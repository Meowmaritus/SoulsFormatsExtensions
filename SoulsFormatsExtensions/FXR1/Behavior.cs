using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
        [XmlInclude(typeof(BehaviorType27))]
        [XmlInclude(typeof(BehaviorType28))]
        [XmlInclude(typeof(BehaviorType29))]
        [XmlInclude(typeof(BehaviorType30))]
        [XmlInclude(typeof(BehaviorType31))]
        [XmlInclude(typeof(BehaviorType32))]
        [XmlInclude(typeof(BehaviorType40))]
        [XmlInclude(typeof(BehaviorType43))]
        [XmlInclude(typeof(BehaviorType55))]
        [XmlInclude(typeof(BehaviorType59))]
        [XmlInclude(typeof(BehaviorType61))]
        [XmlInclude(typeof(BehaviorType66))]
        [XmlInclude(typeof(BehaviorType70))]
        [XmlInclude(typeof(BehaviorType71))]
        [XmlInclude(typeof(BehaviorType84))]
        [XmlInclude(typeof(BehaviorType105))]
        [XmlInclude(typeof(BehaviorType107))]
        [XmlInclude(typeof(BehaviorType108))]
        [XmlInclude(typeof(BehaviorType117))]
        [XmlInclude(typeof(BehaviorRef))]
        public abstract class Behavior : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenBehaviors;

            [XmlIgnore]
            public abstract int Type { get; }

            [XmlIgnore] // Set automatically during parent Effect's Write()
            public Effect ParentEffect;

            public List<PreDataEntry> PreDatas;

            [XmlIgnore]
            internal int DEBUG_SizeOnRead = -1;

            public virtual bool ShouldSerializeParentEffect() => true;
            public virtual bool ShouldSerializePreDatas() => true;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            internal override void ToXIDs(FXR1 fxr)
            {
                ParentEffect = fxr.ReferenceEffect(ParentEffect);
                InnerToXIDs(fxr);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                ParentEffect = fxr.DereferenceEffect(ParentEffect);
                InnerFromXIDs(fxr);
            }

            internal virtual void InnerToXIDs(FXR1 fxr)
            {

            }

            internal virtual void InnerFromXIDs(FXR1 fxr)
            {

            }

            //public byte[] TEMP_DATA;

            private FxrEnvironment currentWriteEnvironment = null;
            private Dictionary<Param, List<long>> paramWriteLocations = new Dictionary<Param, List<long>>();

            //protected void WriteParamArray(Param[] p, int expectedLength)
            //{
            //    if (p.Length != expectedLength)
            //        throw new InvalidOperationException("Invalid number of params in param array.");

            //    foreach (var x in p)
            //        WriteParam(x);
            //}

            internal void WriteParam(Param p)
            {
                currentWriteEnvironment.bw.WriteFXR1Varint(p.Type);

                if (p.IsEmptyPointer())
                {
                    currentWriteEnvironment.bw.WriteUInt32(0);
                }
                else
                {
                    if (!paramWriteLocations.ContainsKey(p))
                        paramWriteLocations.Add(p, new List<long>());

                    if (!paramWriteLocations[p].Contains(currentWriteEnvironment.bw.Position))
                        paramWriteLocations[p].Add(currentWriteEnvironment.bw.Position);

                    currentWriteEnvironment.RegisterPointerOffset(currentWriteEnvironment.bw.Position);

                    currentWriteEnvironment.bw.WriteUInt32(0xEEEEEEEE);
                }

                // garbage on end of offset to packed data
                currentWriteEnvironment.bw.WriteFXR1Garbage();
            }

            internal void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                long startPos = bw.Position;

                bw.WriteInt32(Type);
                bw.ReserveInt32("Behavior.Size");
                bw.WriteFXR1Varint(PreDatas.Count);
                bw.ReserveInt32("Behavior.PreDatas.Numbers");
                bw.ReserveInt32("Behavior.PreDatas.Params");
                env.RegisterPointer(ParentEffect, useExistingPointerOnly: true);

                paramWriteLocations.Clear();
                currentWriteEnvironment = env;
                InnerWrite(bw, env);

                if (bw.VarintLong)
                    bw.Pad(8);

                bw.FillInt32("Behavior.PreDatas.Numbers", (int)bw.Position);
                for (int i = 0; i < PreDatas.Count; i++)
                {
                    bw.WriteInt32(PreDatas[i].Unk);
                }

                bw.FillInt32("Behavior.PreDatas.Params", (int)bw.Position);
                for (int i = 0; i < PreDatas.Count; i++)
                {
                    WriteParam(PreDatas[i].Data);
                }

                foreach (var kvp in paramWriteLocations)
                {
                    long offsetOfThisParam = bw.Position;

                    foreach (var location in kvp.Value)
                    {
                        bw.StepIn(location);
                        bw.WriteInt32((int)offsetOfThisParam);
                        bw.StepOut();
                    }

                    kvp.Key.InnerWrite(bw, env); 
                }

                int writtenSize = (int)(bw.Position - startPos);

                //if (DEBUG_SizeOnRead != -1 && writtenSize != DEBUG_SizeOnRead)
                //    throw new Exception("sdfsgfdsgfds");

                bw.FillInt32("Behavior.Size", writtenSize);

                bw.Pad(16); //Might be 16?

                paramWriteLocations.Clear();
                currentWriteEnvironment = null;
            }

            public static Behavior Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long startOffset = br.Position;

                int subType = br.ReadInt32();
                int size = br.ReadInt32();
                int preDataCount = br.ReadFXR1Varint();
                int offsetToPreDataNumbers = br.ReadFXR1Varint();
                int offsetToPreDataParams = br.ReadFXR1Varint();

                int offsetToParentEffect = br.ReadFXR1Varint();
                var parentEffect = env.GetEffect(br, offsetToParentEffect);

                Behavior data;

                switch (subType)
                {
                    case 27: data = new BehaviorType27(); break;
                    case 28: data = new BehaviorType28(); break;
                    case 29: data = new BehaviorType29(); break;
                    case 30: data = new BehaviorType30(); break;
                    case 31: data = new BehaviorType31(); break;
                    case 32: data = new BehaviorType32(); break;
                    case 40: data = new BehaviorType40(); break;
                    case 43: data = new BehaviorType43(); break;
                    case 55: data = new BehaviorType55(); break;
                    case 59: data = new BehaviorType59(); break;
                    case 61: data = new BehaviorType61(); break;
                    case 66: data = new BehaviorType66(); break;
                    case 70: data = new BehaviorType70(); break;
                    case 71: data = new BehaviorType71(); break;
                    case 84: data = new BehaviorType84(); break;
                    case 105: data = new BehaviorType105(); break;
                    case 107: data = new BehaviorType107(); break;
                    case 108: data = new BehaviorType108(); break;
                    case 117: data = new BehaviorType117(); break;
                    default: throw new NotImplementedException();
                }

                env.RegisterOffset(startOffset, data);

                //TEMPORARY
                data.DEBUG_SizeOnRead = size;

                data.InnerRead(br, env);

                //data.TEMP_DATA = br.GetBytes(startOffset, size);

                data.ParentEffect = parentEffect;

                data.PreDatas = new List<PreDataEntry>(preDataCount);

                //pre data nubmers go here during write
                br.StepIn(offsetToPreDataNumbers);
                for (int i = 0; i < preDataCount; i++)
                {
                    data.PreDatas.Add(new PreDataEntry()
                    {
                        Unk = br.ReadInt32()
                    });
                }
                br.StepOut();

                //pre data subtypes go here during write
                br.StepIn(offsetToPreDataParams);
                for (int i = 0; i < preDataCount; i++)
                {
                    data.PreDatas[i].Data = Param.Read(br, env);
                }
                br.StepOut();

                //the packed shit from switch(subType) all goes here during write?

                br.Position = startOffset + size;

                return data;
            }

            public class BehaviorType27 : Behavior
            {
                public override int Type => 27;

                public float Unk1;
                public float Unk2;
                public float Unk3;
                public float Unk4;
                public int TextureID;
                public int Unk6;
                public Param Unk7_1;
                public Param Unk7_2;
                public Param Unk7_3;
                public Param Unk7_4;
                public Param Unk7_5;
                public Param Unk7_6;
                public Param Unk7_7;
                public Param Unk7_8;
                public Param Unk7_9;
                public Param Unk7_10;
                public int Unk8;
                public int Unk9;
                public int Unk10;
                public float Unk11;
                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    Unk2 = br.ReadSingle();
                    Unk3 = br.ReadSingle();

                    br.AssertInt32(0);

                    Unk4 = br.ReadSingle();

                    if (br.VarintLong)
                        br.AssertInt32(0);

                    TextureID = br.ReadInt32();
                    Unk6 = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk7_1 = Param.Read(br, env);
                    Unk7_2 = Param.Read(br, env);
                    Unk7_3 = Param.Read(br, env);
                    Unk7_4 = Param.Read(br, env);
                    Unk7_5 = Param.Read(br, env);
                    Unk7_6 = Param.Read(br, env);
                    Unk7_7 = Param.Read(br, env);
                    Unk7_8 = Param.Read(br, env);
                    Unk7_9 = Param.Read(br, env);
                    Unk7_10 = Param.Read(br, env);

                    Unk8 = br.ReadInt32();
                    Unk9 = br.ReadInt32();
                    Unk10 = br.ReadInt32();
                    Unk11 = br.ReadSingle();

                    if (br.VarintLong)
                        DS1RData = DS1RExtraParams.Read(br, env);
                    
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);
                    bw.WriteSingle(Unk2);
                    bw.WriteSingle(Unk3);

                    bw.WriteInt32(0);

                    bw.WriteSingle(Unk4);

                    if (bw.VarintLong)
                        bw.WriteInt32(0);

                    bw.WriteInt32(TextureID);
                    bw.WriteInt32(Unk6);

                    bw.WriteInt32(0);

                    WriteParam(Unk7_1);
                    WriteParam(Unk7_2);
                    WriteParam(Unk7_3);
                    WriteParam(Unk7_4);
                    WriteParam(Unk7_5);
                    WriteParam(Unk7_6);
                    WriteParam(Unk7_7);
                    WriteParam(Unk7_8);
                    WriteParam(Unk7_9);
                    WriteParam(Unk7_10);
                    bw.WriteInt32(Unk8);
                    bw.WriteInt32(Unk9);
                    bw.WriteInt32(Unk10);
                    bw.WriteSingle(Unk11);

                    if (bw.VarintLong)
                        DS1RData.Write(bw, this);
                }
            }



            public class BehaviorType28 : Behavior
            {
                public override int Type => 28;

                public Param Unk1;
                public Param Unk2;
                public Param Unk3;
                public int Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = Param.Read(br, env);
                    Unk2 = Param.Read(br, env);
                    Unk3 = Param.Read(br, env);
                    Unk4 = br.ReadInt32();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1);
                    WriteParam(Unk2);
                    WriteParam(Unk3);
                    bw.WriteInt32(Unk4);
                }
            }


            public class BehaviorType29 : Behavior
            {
                public override int Type => 29;

                public Param Unk1;
                public Param Unk2;
                public Param Unk3;
                public Param Unk4;
                public Param Unk5;
                public int Unk6;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = Param.Read(br, env);
                    Unk2 = Param.Read(br, env);
                    Unk3 = Param.Read(br, env);
                    Unk4 = Param.Read(br, env);
                    Unk5 = Param.Read(br, env);
                    Unk6 = br.ReadInt32();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1);
                    WriteParam(Unk2);
                    WriteParam(Unk3);
                    WriteParam(Unk4);
                    WriteParam(Unk5);
                    bw.WriteInt32(Unk6);
                }
            }

            public class BehaviorType30 : Behavior
            {
                public override int Type => 30;

                public Param Unk1_1;
                public Param Unk1_2;
                public Param Unk1_3;
                public Param Unk1_4;
                public float Unk2;
                public int Unk3;
                public int Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = Param.Read(br, env);
                    Unk1_2 = Param.Read(br, env);
                    Unk1_3 = Param.Read(br, env);
                    Unk1_4 = Param.Read(br, env);
                    Unk2 = br.ReadSingle();
                    Unk3 = br.ReadInt32();
                    Unk4 = br.ReadFXR1Varint();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1_1);
                    WriteParam(Unk1_2);
                    WriteParam(Unk1_3);
                    WriteParam(Unk1_4);
                    bw.WriteSingle(Unk2);
                    bw.WriteInt32(Unk3);
                    bw.WriteFXR1Varint(Unk4);
                }
            }


            public class BehaviorType31 : Behavior
            {
                public override int Type => 31;

                public Param Unk1_1;
                public Param Unk1_2;
                public Param Unk1_3;
                public Param Unk1_4;
                public int Unk2;
                public int Unk3;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = Param.Read(br, env);
                    Unk1_2 = Param.Read(br, env);
                    Unk1_3 = Param.Read(br, env);
                    Unk1_4 = Param.Read(br, env);
                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1_1);
                    WriteParam(Unk1_2);
                    WriteParam(Unk1_3);
                    WriteParam(Unk1_4);
                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3);
                }
            }

            public class BehaviorType32 : Behavior
            {
                public override int Type => 32;

                public Param OffsetX;
                public Param OffsetY;
                public Param OffsetZ;
                public Param Unk1_1;
                public Param Unk1_2;
                public Param Unk1_3;
                public int Unk2;
                public int Unk3;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    OffsetX = Param.Read(br, env);
                    OffsetY = Param.Read(br, env);
                    OffsetZ = Param.Read(br, env);
                    Unk1_1 = Param.Read(br, env);
                    Unk1_2 = Param.Read(br, env);
                    Unk1_3 = Param.Read(br, env);
                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(OffsetX);
                    WriteParam(OffsetY);
                    WriteParam(OffsetZ);
                    WriteParam(Unk1_1);
                    WriteParam(Unk1_2);
                    WriteParam(Unk1_3);
                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3);
                }
            }

            public class BehaviorType40 : Behavior
            {
                public override int Type => 40;

                public float Unk1;
                public int TextureID;
                public int Unk3;
                public int Unk4;
                public int Unk5;
                public Param Unk6_1;
                public Param Unk6_2;
                public Param Unk6_3;
                public Param Unk6_4;
                public float Unk7;
                public float Unk8;
                public int Unk9;
                public int Unk10;
                public Param Unk11_1;
                public Param Unk11_2;
                public Param Unk11_3;
                public Param Unk11_4;
                public int Unk12;
                public int Unk13;
                public Param Unk14;
                public int Unk15;
                public float Unk16;
                public Param Unk17_1;
                public Param Unk17_2;
                public int Unk18;

                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertInt32(0);

                    Unk1 = br.ReadSingle();
                    TextureID = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk3 = br.ReadInt32();
                    Unk4 = br.ReadInt32();
                    Unk5 = br.ReadInt32();
                    Unk6_1 = Param.Read(br, env);
                    Unk6_2 = Param.Read(br, env);
                    Unk6_3 = Param.Read(br, env);
                    Unk6_4 = Param.Read(br, env);
                    Unk7 = br.ReadSingle();
                    Unk8 = br.ReadSingle();
                    Unk9 = br.ReadInt32();
                    Unk10 = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk11_1 = Param.Read(br, env);
                    Unk11_2 = Param.Read(br, env);
                    Unk11_3 = Param.Read(br, env);
                    Unk11_4 = Param.Read(br, env);
                    Unk12 = br.ReadInt32();
                    Unk13 = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk14 = Param.Read(br, env);
                    Unk15 = br.ReadInt32();
                    Unk16 = br.ReadSingle();
                    Unk17_1 = Param.Read(br, env);
                    Unk17_2 = Param.Read(br, env);
                    Unk18 = br.ReadInt32();

                    if (br.VarintLong)
                        DS1RData = DS1RExtraParams.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteInt32(0);

                    bw.WriteSingle(Unk1);
                    bw.WriteInt32(TextureID);

                    bw.WriteInt32(0);

                    bw.WriteInt32(Unk3);
                    bw.WriteInt32(Unk4);
                    bw.WriteInt32(Unk5);
                    WriteParam(Unk6_1);
                    WriteParam(Unk6_2);
                    WriteParam(Unk6_3);
                    WriteParam(Unk6_4);
                    bw.WriteSingle(Unk7);
                    bw.WriteSingle(Unk8);
                    bw.WriteInt32(Unk9);
                    bw.WriteInt32(Unk10);

                    bw.WriteInt32(0);

                    WriteParam(Unk11_1);
                    WriteParam(Unk11_2);
                    WriteParam(Unk11_3);
                    WriteParam(Unk11_4);
                    bw.WriteInt32(Unk12);
                    bw.WriteInt32(Unk13);

                    bw.WriteInt32(0);

                    WriteParam(Unk14);
                    bw.WriteInt32(Unk15);
                    bw.WriteSingle(Unk16);
                    WriteParam(Unk17_1);
                    WriteParam(Unk17_2);
                    bw.WriteInt32(Unk18);

                    if (bw.VarintLong)
                        DS1RData.Write(bw, this);
                }
            }

            public class BehaviorType43 : Behavior
            {
                public override int Type => 43;

                public float Unk1;
                public int TextureID;
                public int Unk2;
                public int Unk3;
                public int Unk4;
                public int Unk5;
                public int Unk6;
                public Param Unk7_1;
                public Param Unk7_2;
                public Param Unk7_3;
                public Param Unk7_4;
                public Param Unk7_5;
                public Param Unk7_6;
                public Param Unk7_7;
                public Param Unk7_8;
                public Param Unk7_9;
                public Param Unk7_10;
                public Param Unk7_11;
                public Param Unk7_12;
                public Param Unk7_13;
                public int Unk8;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    TextureID = br.ReadInt32();
                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                    Unk4 = br.ReadInt32();
                    Unk5 = br.ReadInt32();
                    Unk6 = br.ReadInt32();
                    Unk7_1 = Param.Read(br, env);
                    Unk7_2 = Param.Read(br, env);
                    Unk7_3 = Param.Read(br, env);
                    Unk7_4 = Param.Read(br, env);
                    Unk7_5 = Param.Read(br, env);
                    Unk7_6 = Param.Read(br, env);
                    Unk7_7 = Param.Read(br, env);
                    Unk7_8 = Param.Read(br, env);
                    Unk7_9 = Param.Read(br, env);
                    Unk7_10 = Param.Read(br, env);
                    Unk7_11 = Param.Read(br, env);
                    Unk7_12 = Param.Read(br, env);
                    Unk7_13 = Param.Read(br, env);
                    Unk8 = br.ReadInt32();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(TextureID);
                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3);
                    bw.WriteInt32(Unk4);
                    bw.WriteInt32(Unk5);
                    bw.WriteInt32(Unk6);
                    WriteParam(Unk7_1);
                    WriteParam(Unk7_2);
                    WriteParam(Unk7_3);
                    WriteParam(Unk7_4);
                    WriteParam(Unk7_5);
                    WriteParam(Unk7_6);
                    WriteParam(Unk7_7);
                    WriteParam(Unk7_8);
                    WriteParam(Unk7_9);
                    WriteParam(Unk7_10);
                    WriteParam(Unk7_11);
                    WriteParam(Unk7_12);
                    WriteParam(Unk7_13);
                    bw.WriteInt32(Unk8);
                }
            }

            public class BehaviorType55 : Behavior
            {
                public override int Type => 55;

                public Param Unk1;
                public Param Unk2;
                public Param Unk3;
                public float Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = Param.Read(br, env);
                    Unk2 = Param.Read(br, env);
                    Unk3 = Param.Read(br, env);

                    br.AssertInt32(0);

                    Unk4 = br.ReadSingle();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1);
                    WriteParam(Unk2);
                    WriteParam(Unk3);

                    bw.WriteInt32(0);

                    bw.WriteSingle(Unk4);
                }
            }

            public class BehaviorType59 : Behavior
            {
                public override int Type => 59;

                public float Unk1;
                public int TextureID;
                public int Unk2;
                public int Unk3;
                public Param Unk4_1;
                public Param Unk4_2;
                public Param Unk4_3;
                public Param Unk4_4;
                public Param Unk4_5;
                public int Unk5;
                public int Unk6;
                public Param Unk7_1;
                public Param Unk7_2;
                public Param Unk7_3;
                public Param Unk7_4;
                public Param Unk7_5;
                public Param Unk7_6;
                public Param Unk7_7;
                public Param Unk7_8;
                public int Unk8;
                public int Unk9;
                public int Unk10;
                public float Unk11;

                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();

                    br.AssertInt32(0);

                    TextureID = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                    Unk4_1 = Param.Read(br, env);
                    Unk4_2 = Param.Read(br, env);
                    Unk4_3 = Param.Read(br, env);
                    Unk4_4 = Param.Read(br, env);
                    Unk4_5 = Param.Read(br, env);
                    Unk5 = br.ReadInt32();
                    Unk6 = br.ReadInt32();
                    Unk7_1 = Param.Read(br, env);
                    Unk7_2 = Param.Read(br, env);
                    Unk7_3 = Param.Read(br, env);
                    Unk7_4 = Param.Read(br, env);
                    Unk7_5 = Param.Read(br, env);
                    Unk7_6 = Param.Read(br, env);
                    Unk7_7 = Param.Read(br, env);
                    Unk7_8 = Param.Read(br, env);
                    Unk8 = br.ReadInt32();
                    Unk9 = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk10 = br.ReadInt32();
                    Unk11 = br.ReadSingle();

                    br.AssertInt32(0);

                    if (br.VarintLong)
                        DS1RData = DS1RExtraParams.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);

                    bw.WriteInt32(0);

                    bw.WriteInt32(TextureID);

                    bw.WriteInt32(0);

                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3);
                    WriteParam(Unk4_1);
                    WriteParam(Unk4_2);
                    WriteParam(Unk4_3);
                    WriteParam(Unk4_4);
                    WriteParam(Unk4_5);
                    bw.WriteInt32(Unk5);
                    bw.WriteInt32(Unk6);
                    WriteParam(Unk7_1);
                    WriteParam(Unk7_2);
                    WriteParam(Unk7_3);
                    WriteParam(Unk7_4);
                    WriteParam(Unk7_5);
                    WriteParam(Unk7_6);
                    WriteParam(Unk7_7);
                    WriteParam(Unk7_8);
                    bw.WriteInt32(Unk8);
                    bw.WriteInt32(Unk9);

                    bw.WriteInt32(0);

                    bw.WriteInt32(Unk10);
                    bw.WriteSingle(Unk11);

                    bw.WriteInt32(0);

                    if (bw.VarintLong)
                        DS1RData.Write(bw, this);
                }
            }

            public class BehaviorType61 : Behavior
            {
                public override int Type => 61;

                public int TextureID;
                public int Unk1;
                public int Unk2;
                public int Unk3_1;
                public int Unk3_2;
                public Param Unk4_1;
                public Param Unk4_2;
                public Param Unk4_3;
                public int Unk5;
                public float Unk6;
                public Param Unk7;
                public int Unk8;
                public int Unk9;
                public Param Unk10_1;
                public Param Unk10_2;
                public Param Unk10_3;
                public Param Unk10_4;
                public Param Unk10_5;
                public Param Unk10_6;
                public Param Unk10_7;
                public Param Unk10_8;
                public Param Unk10_9;
                public Param Unk10_10;
                public int Unk11;
                public int Unk12;

                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);

                    TextureID = br.ReadInt32();
                    Unk1 = br.ReadInt32();
                    Unk2 = br.ReadInt32();
                    Unk3_1 = br.ReadInt32();
                    Unk3_2 = br.ReadInt32();
                    Unk4_1 = Param.Read(br, env);
                    Unk4_2 = Param.Read(br, env);
                    Unk4_3 = Param.Read(br, env);

                    br.AssertInt32(0);
                    br.AssertInt32(0);

                    Unk5 = br.ReadInt32();
                    Unk6 = br.ReadSingle();

                    br.AssertInt32(0);

                    Unk7 = Param.Read(br, env);
                    Unk8 = br.ReadInt32();
                    Unk9 = br.ReadInt32();
                    Unk10_1 = Param.Read(br, env);
                    Unk10_2 = Param.Read(br, env);
                    Unk10_3 = Param.Read(br, env);
                    Unk10_4 = Param.Read(br, env);
                    Unk10_5 = Param.Read(br, env);
                    Unk10_6 = Param.Read(br, env);
                    Unk10_7 = Param.Read(br, env);
                    Unk10_8 = Param.Read(br, env);
                    Unk10_9 = Param.Read(br, env);
                    Unk10_10 = Param.Read(br, env);
                    Unk11 = br.ReadInt32();
                    Unk12 = br.ReadInt32();

                    br.AssertInt32(0);
                    br.AssertInt32(0);

                    if (br.VarintLong)
                        DS1RData = DS1RExtraParams.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);

                    bw.WriteInt32(TextureID);
                    bw.WriteInt32(Unk1);
                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3_1);
                    bw.WriteInt32(Unk3_2);
                    WriteParam(Unk4_1);
                    WriteParam(Unk4_2);
                    WriteParam(Unk4_3);

                    bw.WriteInt32(0);
                    bw.WriteInt32(0);

                    bw.WriteInt32(Unk5);
                    bw.WriteSingle(Unk6);

                    bw.WriteInt32(0);

                    WriteParam(Unk7);
                    bw.WriteInt32(Unk8);
                    bw.WriteInt32(Unk9);
                    WriteParam(Unk10_1);
                    WriteParam(Unk10_2);
                    WriteParam(Unk10_3);
                    WriteParam(Unk10_4);
                    WriteParam(Unk10_5);
                    WriteParam(Unk10_6);
                    WriteParam(Unk10_7);
                    WriteParam(Unk10_8);
                    WriteParam(Unk10_9);
                    WriteParam(Unk10_10);
                    bw.WriteInt32(Unk11);
                    bw.WriteInt32(Unk12);

                    bw.WriteInt32(0);
                    bw.WriteInt32(0);

                    if (bw.VarintLong)
                        DS1RData.Write(bw, this);
                }
            }


            public class BehaviorType66 : Behavior
            {
                public override int Type => 66;

                public float Unk1;
                public float Unk2;
                public int Unk3;
                public float Unk4;
                public int Unk5;

                public int DS1R_Unk0;

                public Param Unk6_1;
                public Param Unk6_2;
                public Param Unk6_3;
                public Param Unk6_4;
                public Param Unk6_5;
                public Param Unk6_6;
                public Param Unk6_7;
                public Param Unk6_8;
                public Param Unk6_9;
                public Param Unk6_10;
                public Param Unk6_11;
                public Param Unk6_12;
                public Param Unk6_13;
                public Param Unk6_14;
                public Param Unk6_15;
                public Param Unk6_16;
                public Param Unk6_17;
                public Param Unk6_18;
                public Param Unk6_19;
                public Param Unk6_20;
                public Param Unk6_21;
                public Param Unk6_22;
                public Param Unk6_23;
                public Param Unk6_24;
                public Param Unk6_25;
                public Param Unk6_26;

                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    Unk2 = br.ReadSingle();

                    br.AssertInt32(0);

                    Unk3 = br.ReadInt32();
                    Unk4 = br.ReadSingle();
                    Unk5 = br.ReadInt32();

                    if (br.VarintLong)
                        DS1R_Unk0 = br.ReadFXR1Varint();

                    Unk6_1 = Param.Read(br, env);
                    Unk6_2 = Param.Read(br, env);
                    Unk6_3 = Param.Read(br, env);
                    Unk6_4 = Param.Read(br, env);
                    Unk6_5 = Param.Read(br, env);
                    Unk6_6 = Param.Read(br, env);
                    Unk6_7 = Param.Read(br, env);
                    Unk6_8 = Param.Read(br, env);
                    Unk6_9 = Param.Read(br, env);
                    Unk6_10 = Param.Read(br, env);
                    Unk6_11 = Param.Read(br, env);
                    Unk6_12 = Param.Read(br, env);
                    Unk6_13 = Param.Read(br, env);
                    Unk6_14 = Param.Read(br, env);
                    Unk6_15 = Param.Read(br, env);
                    Unk6_16 = Param.Read(br, env);
                    Unk6_17 = Param.Read(br, env);
                    Unk6_18 = Param.Read(br, env);
                    Unk6_19 = Param.Read(br, env);
                    Unk6_20 = Param.Read(br, env);
                    Unk6_21 = Param.Read(br, env);
                    Unk6_22 = Param.Read(br, env);
                    Unk6_23 = Param.Read(br, env);
                    Unk6_24 = Param.Read(br, env);
                    Unk6_25 = Param.Read(br, env);
                    Unk6_26 = Param.Read(br, env);

                    br.AssertInt32(0);

                    if (br.VarintLong)
                    {
                        br.AssertInt32(0);
                        DS1RData = DS1RExtraParams.Read(br, env);
                    }
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);
                    bw.WriteSingle(Unk2);

                    bw.WriteInt32(0);

                    bw.WriteInt32(Unk3);
                    bw.WriteSingle(Unk4);
                    bw.WriteInt32(Unk5);

                    if (bw.VarintLong)
                        bw.WriteFXR1Varint(DS1R_Unk0);

                    WriteParam(Unk6_1);
                    WriteParam(Unk6_2);
                    WriteParam(Unk6_3);
                    WriteParam(Unk6_4);
                    WriteParam(Unk6_5);
                    WriteParam(Unk6_6);
                    WriteParam(Unk6_7);
                    WriteParam(Unk6_8);
                    WriteParam(Unk6_9);
                    WriteParam(Unk6_10);
                    WriteParam(Unk6_11);
                    WriteParam(Unk6_12);
                    WriteParam(Unk6_13);
                    WriteParam(Unk6_14);
                    WriteParam(Unk6_15);
                    WriteParam(Unk6_16);
                    WriteParam(Unk6_17);
                    WriteParam(Unk6_18);
                    WriteParam(Unk6_19);
                    WriteParam(Unk6_20);
                    WriteParam(Unk6_21);
                    WriteParam(Unk6_22);
                    WriteParam(Unk6_23);
                    WriteParam(Unk6_24);
                    WriteParam(Unk6_25);
                    WriteParam(Unk6_26);

                    bw.WriteInt32(0);

                    if (bw.VarintLong)
                    {
                        bw.WriteInt32(0);
                        DS1RData.Write(bw, this);
                    }
                }
            }

            public class BehaviorType70 : Behavior
            {
                public override int Type => 70;

                public float Unk1;
                public float Unk2;
                public float Unk3;
                public int Unk4;
                public float Unk5;
                public int TextureID1;
                public int TextureID2;
                public int TextureID3;
                public int Unk6;
                public int Unk7;
                public int Unk8;
                public Param Unk9_1;
                public Param Unk9_2;
                public Param Unk9_3;
                public Param Unk9_4;
                public Param Unk9_5;
                public Param Unk9_6;
                public Param Unk9_7;
                public Param Unk9_8;
                public Param Unk9_9;
                public Param Unk9_10;
                public Param Unk9_11;
                public Param Unk9_12;
                public Param Unk9_13;
                public Param Unk9_14;
                public Param Unk9_15;
                public Param Unk9_16;
                public Param Unk9_17;
                public Param Unk9_18;
                public Param Unk9_19;
                public Param Unk9_20;
                public Param Unk9_21;
                public Param Unk9_22;
                public Param Unk9_23;
                public Param Unk9_24;
                public Param Unk9_25;
                public Param Unk9_26;
                public Param Unk9_27;
                public Param Unk9_28;
                public Param Unk9_29;
                public Param Unk9_30;
                public int Unk10;
                public int Unk11;
                public int Unk12;
                public int Unk13;
                public float Unk14;
                public int Unk15;
                public float Unk16;
                public int Unk17;

                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    Unk2 = br.ReadSingle();
                    Unk3 = br.ReadSingle();
                    Unk4 = br.ReadInt32();
                    Unk5 = br.ReadSingle();

                    if (br.VarintLong)
                        br.AssertInt32(0);

                    TextureID1 = br.ReadInt32();
                    TextureID2 = br.ReadInt32();
                    TextureID3 = br.ReadInt32();
                    Unk6 = br.ReadInt32();
                    Unk7 = br.ReadInt32();
                    Unk8 = br.ReadInt32();
                    Unk9_1 = Param.Read(br, env);
                    Unk9_2 = Param.Read(br, env);
                    Unk9_3 = Param.Read(br, env);
                    Unk9_4 = Param.Read(br, env);
                    Unk9_5 = Param.Read(br, env);
                    Unk9_6 = Param.Read(br, env);
                    Unk9_7 = Param.Read(br, env);
                    Unk9_8 = Param.Read(br, env);
                    Unk9_9 = Param.Read(br, env);
                    Unk9_10 = Param.Read(br, env);
                    Unk9_11 = Param.Read(br, env);
                    Unk9_12 = Param.Read(br, env);
                    Unk9_13 = Param.Read(br, env);
                    Unk9_14 = Param.Read(br, env);
                    Unk9_15 = Param.Read(br, env);
                    Unk9_16 = Param.Read(br, env);
                    Unk9_17 = Param.Read(br, env);
                    Unk9_18 = Param.Read(br, env);
                    Unk9_19 = Param.Read(br, env);
                    Unk9_20 = Param.Read(br, env);
                    Unk9_21 = Param.Read(br, env);
                    Unk9_22 = Param.Read(br, env);
                    Unk9_23 = Param.Read(br, env);
                    Unk9_24 = Param.Read(br, env);
                    Unk9_25 = Param.Read(br, env);
                    Unk9_26 = Param.Read(br, env);
                    Unk9_27 = Param.Read(br, env);
                    Unk9_28 = Param.Read(br, env);
                    Unk9_29 = Param.Read(br, env);
                    Unk9_30 = Param.Read(br, env);
                    Unk10 = br.ReadInt32();

                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);

                    if (br.VarintLong)
                    {
                        br.AssertInt32(0);
                        br.AssertInt32(0);
                        br.AssertInt32(0);
                        br.AssertInt32(0);
                    }

                    Unk11 = br.ReadInt32();
                    Unk12 = br.ReadInt32();
                    Unk13 = br.ReadInt32();
                    Unk14 = br.ReadSingle();
                    Unk15 = br.ReadInt32();
                    Unk16 = br.ReadSingle();
                    Unk17 = br.ReadInt32();

                    if (br.VarintLong)
                        DS1RData = DS1RExtraParams.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);
                    bw.WriteSingle(Unk2);
                    bw.WriteSingle(Unk3);
                    bw.WriteInt32(Unk4);
                    bw.WriteSingle(Unk5);

                    if (bw.VarintLong)
                        bw.WriteInt32(0);

                    bw.WriteInt32(TextureID1);
                    bw.WriteInt32(TextureID2);
                    bw.WriteInt32(TextureID3);
                    bw.WriteInt32(Unk6);
                    bw.WriteInt32(Unk7);
                    bw.WriteInt32(Unk8);
                    WriteParam(Unk9_1);
                    WriteParam(Unk9_2);
                    WriteParam(Unk9_3);
                    WriteParam(Unk9_4);
                    WriteParam(Unk9_5);
                    WriteParam(Unk9_6);
                    WriteParam(Unk9_7);
                    WriteParam(Unk9_8);
                    WriteParam(Unk9_9);
                    WriteParam(Unk9_10);
                    WriteParam(Unk9_11);
                    WriteParam(Unk9_12);
                    WriteParam(Unk9_13);
                    WriteParam(Unk9_14);
                    WriteParam(Unk9_15);
                    WriteParam(Unk9_16);
                    WriteParam(Unk9_17);
                    WriteParam(Unk9_18);
                    WriteParam(Unk9_19);
                    WriteParam(Unk9_20);
                    WriteParam(Unk9_21);
                    WriteParam(Unk9_22);
                    WriteParam(Unk9_23);
                    WriteParam(Unk9_24);
                    WriteParam(Unk9_25);
                    WriteParam(Unk9_26);
                    WriteParam(Unk9_27);
                    WriteParam(Unk9_28);
                    WriteParam(Unk9_29);
                    WriteParam(Unk9_30);
                    bw.WriteInt32(Unk10);

                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);

                    if (bw.VarintLong)
                    {
                        bw.WriteInt32(0);
                        bw.WriteInt32(0);
                        bw.WriteInt32(0);
                        bw.WriteInt32(0);
                    }

                    bw.WriteInt32(Unk11);
                    bw.WriteInt32(Unk12);
                    bw.WriteInt32(Unk13);
                    bw.WriteSingle(Unk14);
                    bw.WriteInt32(Unk15);
                    bw.WriteSingle(Unk16);
                    bw.WriteInt32(Unk17);

                    if (bw.VarintLong)
                        DS1RData.Write(bw, this);
                }
            }

            public class BehaviorType71 : Behavior
            {
                public override int Type => 71;

                public float Unk1;
                public float Unk2;
                public float Unk3;
                public int Unk4;
                public float Unk5;
                public int TextureID;
                public int Unk6;
                public int Unk7;
                public int Unk8;
                public int Unk9;
                public int Unk10;
                public Param Unk11_1;
                public Param Unk11_2;
                public Param Unk11_3;
                public Param Unk11_4;
                public Param Unk11_5;
                public Param Unk11_6;
                public Param Unk11_7;
                public Param Unk11_8;
                public Param Unk11_9;
                public Param Unk11_10;
                public int Unk12;
                public int Unk13;
                public Param Unk14_1;
                public Param Unk14_2;
                public Param Unk14_3;
                public Param Unk14_4;
                public Param Unk14_5;
                public Param Unk14_6;
                public Param Unk14_7;
                public Param Unk14_8;
                public Param Unk14_9;
                public Param Unk14_10;

                public int DS1R_UnkA1;
                public int DS1R_UnkA2;
                public int DS1R_UnkA3;
                public int DS1R_UnkA4;

                public int Unk15;
                public int Unk16;
                public int Unk17;
                public int Unk18;
                public float Unk19;
                public int Unk20;
                public float Unk21;
                public int Unk22;

                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    Unk2 = br.ReadSingle();
                    Unk3 = br.ReadSingle();
                    Unk4 = br.ReadInt32();
                    Unk5 = br.ReadSingle();
                    TextureID = br.ReadInt32();
                    Unk7 = br.ReadInt32();
                    Unk8 = br.ReadInt32();
                    Unk9 = br.ReadInt32();
                    Unk10 = br.ReadInt32();
                    Unk11_1 = Param.Read(br, env);
                    Unk11_2 = Param.Read(br, env);
                    Unk11_3 = Param.Read(br, env);
                    Unk11_4 = Param.Read(br, env);
                    Unk11_5 = Param.Read(br, env);
                    Unk11_6 = Param.Read(br, env);
                    Unk11_7 = Param.Read(br, env);
                    Unk11_8 = Param.Read(br, env);
                    Unk11_9 = Param.Read(br, env);
                    Unk11_10 = Param.Read(br, env);
                    Unk12 = br.ReadInt32();
                    Unk13 = br.ReadInt32();
                    Unk14_1 = Param.Read(br, env);
                    Unk14_2 = Param.Read(br, env);
                    Unk14_3 = Param.Read(br, env);
                    Unk14_4 = Param.Read(br, env);
                    Unk14_5 = Param.Read(br, env);
                    Unk14_6 = Param.Read(br, env);
                    Unk14_7 = Param.Read(br, env);
                    Unk14_8 = Param.Read(br, env);
                    Unk14_9 = Param.Read(br, env);
                    Unk14_10 = Param.Read(br, env);

                    if (br.VarintLong)
                    {
                        DS1R_UnkA1 = br.ReadInt32();
                        DS1R_UnkA2 = br.ReadInt32();
                        DS1R_UnkA3 = br.ReadInt32();
                        DS1R_UnkA4 = br.ReadInt32();
                    }

                    Unk15 = br.ReadInt32();

                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);

                    Unk16 = br.ReadInt32();
                    Unk17 = br.ReadInt32();
                    Unk18 = br.ReadInt32();
                    Unk19 = br.ReadSingle();
                    Unk20 = br.ReadInt32();
                    Unk21 = br.ReadSingle();
                    Unk22 = br.ReadInt32();

                    if (br.VarintLong)
                        DS1RData = DS1RExtraParams.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);
                    bw.WriteSingle(Unk2);
                    bw.WriteSingle(Unk3);
                    bw.WriteInt32(Unk4);
                    bw.WriteSingle(Unk5);
                    bw.WriteInt32(TextureID);
                    bw.WriteInt32(Unk7);
                    bw.WriteInt32(Unk8);
                    bw.WriteInt32(Unk9);
                    bw.WriteInt32(Unk10);
                    WriteParam(Unk11_1);
                    WriteParam(Unk11_2);
                    WriteParam(Unk11_3);
                    WriteParam(Unk11_4);
                    WriteParam(Unk11_5);
                    WriteParam(Unk11_6);
                    WriteParam(Unk11_7);
                    WriteParam(Unk11_8);
                    WriteParam(Unk11_9);
                    WriteParam(Unk11_10);
                    bw.WriteInt32(Unk12);
                    bw.WriteInt32(Unk13);
                    WriteParam(Unk14_1);
                    WriteParam(Unk14_2);
                    WriteParam(Unk14_3);
                    WriteParam(Unk14_4);
                    WriteParam(Unk14_5);
                    WriteParam(Unk14_6);
                    WriteParam(Unk14_7);
                    WriteParam(Unk14_8);
                    WriteParam(Unk14_9);
                    WriteParam(Unk14_10);

                    if (bw.VarintLong)
                    {
                        bw.WriteInt32(DS1R_UnkA1);
                        bw.WriteInt32(DS1R_UnkA2);
                        bw.WriteInt32(DS1R_UnkA3);
                        bw.WriteInt32(DS1R_UnkA4);
                    }

                    bw.WriteInt32(Unk15);

                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);

                    bw.WriteInt32(Unk16);
                    bw.WriteInt32(Unk17);
                    bw.WriteInt32(Unk18);
                    bw.WriteSingle(Unk19);
                    bw.WriteInt32(Unk20);
                    bw.WriteSingle(Unk21);
                    bw.WriteInt32(Unk22);

                    if (bw.VarintLong)
                        DS1RData.Write(bw, this);
                }
            }

            public class BehaviorType84 : Behavior
            {
                public override int Type => 84;

                public Param Unk1_1;
                public Param Unk1_2;
                public Param Unk1_3;
                public float Unk2;
                public Param Unk3;
                public int Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = Param.Read(br, env);
                    Unk1_2 = Param.Read(br, env);
                    Unk1_3 = Param.Read(br, env);
                    br.AssertInt32(0);
                    Unk2 = br.ReadSingle();
                    Unk3 = Param.Read(br, env);
                    Unk4 = br.ReadInt32();
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1_1);
                    WriteParam(Unk1_2);
                    WriteParam(Unk1_3);
                    bw.WriteInt32(0);
                    bw.WriteSingle(Unk2);
                    WriteParam(Unk3);
                    bw.WriteInt32(Unk4);
                }
            }

            public class BehaviorType105 : Behavior
            {
                public override int Type => 105;

                public Param Unk1_1;
                public Param Unk1_2;
                public Param Unk1_3;
                public float Unk2;
                public Param Unk3;
                public int Unk4;
                public Param Unk5;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = Param.Read(br, env);
                    Unk1_2 = Param.Read(br, env);
                    Unk1_3 = Param.Read(br, env);
                    br.AssertInt32(0);
                    Unk2 = br.ReadSingle();
                    Unk3 = Param.Read(br, env);
                    Unk4 = br.ReadFXR1Varint();
                    Unk5 = Param.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1_1);
                    WriteParam(Unk1_2);
                    WriteParam(Unk1_3);
                    bw.WriteInt32(0);
                    bw.WriteSingle(Unk2);
                    WriteParam(Unk3);
                    bw.WriteFXR1Varint(Unk4);
                    WriteParam(Unk5);
                }
            }

            public class BehaviorType107 : Behavior
            {
                public override int Type => 107;

                public float Unk1;
                public int TextureID;
                public int Unk2;
                public Param Unk3;
                public Param Unk4;
                public Param Unk5;
                public Param Unk6;
                public Param Unk7;
                public Param Unk8;
                public Param Unk9;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    br.AssertInt32(0);
                    TextureID = br.ReadInt32();
                    Unk2 = br.ReadInt32();
                    Unk3 = Param.Read(br, env);
                    Unk4 = Param.Read(br, env);
                    Unk5 = Param.Read(br, env);
                    Unk6 = Param.Read(br, env);
                    Unk7 = Param.Read(br, env);
                    Unk8 = Param.Read(br, env);
                    Unk9 = Param.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);
                    bw.WriteInt32(0);
                    bw.WriteInt32(TextureID);
                    bw.WriteInt32(Unk2);
                    WriteParam(Unk3);
                    WriteParam(Unk4);
                    WriteParam(Unk5);
                    WriteParam(Unk6);
                    WriteParam(Unk7);
                    WriteParam(Unk8);
                    WriteParam(Unk9);
                }
            }

            public class BehaviorType108 : Behavior
            {
                public override int Type => 108;

                public float Unk1;
                public float Unk2;
                public float Unk3;
                public int Unk4;
                public float Unk5;
                public int ModelID;
                public int Unk6;
                public int Unk7;
                public int Unk8;
                public Param Scale1X;
                public Param Scale1Y;
                public Param Scale1Z;
                public Param Scale2X;
                public Param Scale2Y;
                public Param Scale2Z;
                public Param RotSpeedX;
                public Param RotSpeedY;
                public Param RotSpeedZ;
                public Param RotVal2X;
                public Param RotVal2Y;
                public Param RotVal2Z;
                public int Unk9;
                public int Unk10;
                public Param Unk11_1;
                public Param Unk11_2;
                public Param Unk11_3;
                public Param Unk11_4;
                public Param Unk11_5;
                public Param Unk11_6;
                public Param Color1R;
                public Param Color1G;
                public Param Color1B;
                public Param Color1A;
                public Param Color2R;
                public Param Color2G;
                public Param Color2B;
                public Param Color2A;
                public int Unk12;
                public int Unk13;
                public int Unk14;
                public float Unk15;
                public int Unk16;

                public DS1RExtraParams DS1RData;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    Unk2 = br.ReadSingle();
                    Unk3 = br.ReadSingle();
                    Unk4 = br.ReadInt32();
                    Unk5 = br.ReadSingle();

                    if (br.VarintLong)
                        br.AssertInt32(0);

                    ModelID = br.ReadInt32();
                    Unk6 = br.ReadInt32();
                    Unk7 = br.ReadInt32();
                    Unk8 = br.ReadInt32();

                    br.AssertInt32(0);

                    Scale1X = Param.Read(br, env);
                    Scale1Y = Param.Read(br, env);
                    Scale1Z = Param.Read(br, env);
                    Scale2X = Param.Read(br, env);
                    Scale2Y = Param.Read(br, env);
                    Scale2Z = Param.Read(br, env);
                    RotSpeedX = Param.Read(br, env);
                    RotSpeedY = Param.Read(br, env);
                    RotSpeedZ = Param.Read(br, env);
                    RotVal2X = Param.Read(br, env);
                    RotVal2Y = Param.Read(br, env);
                    RotVal2Z = Param.Read(br, env);
                    Unk9 = br.ReadInt32();
                    Unk10 = br.ReadInt32();
                    Unk11_1 = Param.Read(br, env);
                    Unk11_2 = Param.Read(br, env);
                    Unk11_3 = Param.Read(br, env);
                    Unk11_4 = Param.Read(br, env);
                    Unk11_5 = Param.Read(br, env);
                    Unk11_6 = Param.Read(br, env);
                    Color1R = Param.Read(br, env);
                    Color1G = Param.Read(br, env);
                    Color1B = Param.Read(br, env);
                    Color1A = Param.Read(br, env);
                    Color2R = Param.Read(br, env);
                    Color2G = Param.Read(br, env);
                    Color2B = Param.Read(br, env);
                    Color2A = Param.Read(br, env);
                    Unk12 = br.ReadInt32();
                    Unk13 = br.ReadInt32();
                    Unk14 = br.ReadInt32();
                    Unk15 = br.ReadSingle();
                    Unk16 = br.ReadInt32();

                    if (br.VarintLong)
                        DS1RData = DS1RExtraParams.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteSingle(Unk1);
                    bw.WriteSingle(Unk2);
                    bw.WriteSingle(Unk3);
                    bw.WriteInt32(Unk4);
                    bw.WriteSingle(Unk5);

                    if (bw.VarintLong)
                        bw.WriteInt32(0);

                    bw.WriteInt32(ModelID);
                    bw.WriteInt32(Unk6);
                    bw.WriteInt32(Unk7);
                    bw.WriteInt32(Unk8);

                    bw.WriteInt32(0);

                    WriteParam(Scale1X);
                    WriteParam(Scale1Y);
                    WriteParam(Scale1Z);
                    WriteParam(Scale2X);
                    WriteParam(Scale2Y);
                    WriteParam(Scale2Z);
                    WriteParam(RotSpeedX);
                    WriteParam(RotSpeedY);
                    WriteParam(RotSpeedZ);
                    WriteParam(RotVal2X);
                    WriteParam(RotVal2Y);
                    WriteParam(RotVal2Z);
                    bw.WriteInt32(Unk9);
                    bw.WriteInt32(Unk10);
                    WriteParam(Unk11_1);
                    WriteParam(Unk11_2);
                    WriteParam(Unk11_3);
                    WriteParam(Unk11_4);
                    WriteParam(Unk11_5);
                    WriteParam(Unk11_6);
                    WriteParam(Color1R);
                    WriteParam(Color1G);
                    WriteParam(Color1B);
                    WriteParam(Color1A);
                    WriteParam(Color2R);
                    WriteParam(Color2G);
                    WriteParam(Color2B);
                    WriteParam(Color2A);
                    bw.WriteInt32(Unk12);
                    bw.WriteInt32(Unk13);
                    bw.WriteInt32(Unk14);
                    bw.WriteSingle(Unk15);
                    bw.WriteInt32(Unk16);

                    if (bw.VarintLong)
                        DS1RData.Write(bw, this);
                }
            }

            public class BehaviorType117 : Behavior
            {
                public override int Type => 117;

                public Param Unk1_1;
                public Param Unk1_2;
                public Param Unk1_3;
                public Param Unk1_4;
                public Param Unk1_5;
                public Param Unk1_6;
                public int Unk2;
                public int Unk3;
                public Param Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = Param.Read(br, env);
                    Unk1_2 = Param.Read(br, env);
                    Unk1_3 = Param.Read(br, env);
                    Unk1_4 = Param.Read(br, env);
                    Unk1_5 = Param.Read(br, env);
                    Unk1_6 = Param.Read(br, env);
                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                    Unk4 = Param.Read(br, env);
                }

                public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
                {
                    WriteParam(Unk1_1);
                    WriteParam(Unk1_2);
                    WriteParam(Unk1_3);
                    WriteParam(Unk1_4);
                    WriteParam(Unk1_5);
                    WriteParam(Unk1_6);
                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3);
                    WriteParam(Unk4);
                }
            }
        }


        public class BehaviorRef : Behavior
        {
            public override int Type => -1;

            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeParentEffect() => false;
            public override bool ShouldSerializePreDatas() => false;

            public BehaviorRef(Behavior refVal)
            {
                ReferenceXID = refVal?.XID;
            }

            public BehaviorRef()
            {

            }

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                throw new InvalidOperationException("Cannot actually serialize a reference class.");
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new InvalidOperationException("Cannot actually deserialize a reference class.");
            }
        }

    }
}
