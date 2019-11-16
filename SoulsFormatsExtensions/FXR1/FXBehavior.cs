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
        [XmlInclude(typeof(FXBehaviorType27))]
        [XmlInclude(typeof(FXBehaviorType28))]
        [XmlInclude(typeof(FXBehaviorType29))]
        [XmlInclude(typeof(FXBehaviorType30))]
        [XmlInclude(typeof(FXBehaviorType31))]
        [XmlInclude(typeof(FXBehaviorType32))]
        [XmlInclude(typeof(FXBehaviorType40))]
        [XmlInclude(typeof(FXBehaviorType43))]
        [XmlInclude(typeof(FXBehaviorType55))]
        [XmlInclude(typeof(FXBehaviorType59))]
        [XmlInclude(typeof(FXBehaviorType61))]
        [XmlInclude(typeof(FXBehaviorType66))]
        [XmlInclude(typeof(FXBehaviorType70))]
        [XmlInclude(typeof(FXBehaviorType71))]
        [XmlInclude(typeof(FXBehaviorType84))]
        [XmlInclude(typeof(FXBehaviorType105))]
        [XmlInclude(typeof(FXBehaviorType107))]
        [XmlInclude(typeof(FXBehaviorType108))]
        [XmlInclude(typeof(FXBehaviorType117))]
        [XmlInclude(typeof(BehaviorRef))]
        public abstract class FXBehavior : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFXBehaviors;

            [XmlIgnore]
            public abstract int Type { get; }

            [XmlIgnore] // Set automatically during parent Effect's Write()
            public FXParamList ContainingParamList;

            public List<PreDataEntry> PreDatas;

            [XmlIgnore]
            internal int DEBUG_SizeOnRead = -1;

            public virtual bool ShouldSerializePreDatas() => true;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            internal override void ToXIDs(FXR1 fxr)
            {
                ContainingParamList = fxr.ReferenceFXParamList(ContainingParamList);
                InnerToXIDs(fxr);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                ContainingParamList = fxr.DereferenceFXParamList(ContainingParamList);
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
            private Dictionary<FXField, List<long>> paramWriteLocations = new Dictionary<FXField, List<long>>();

            //protected void WriteParamArray(Param[] p, int expectedLength)
            //{
            //    if (p.Length != expectedLength)
            //        throw new InvalidOperationException("Invalid number of params in param array.");

            //    foreach (var x in p)
            //        WriteParam(x);
            //}

            internal void WriteParam(FXField p)
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
                env.RegisterPointer(ContainingParamList, useExistingPointerOnly: true);

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

            public static FXBehavior Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long startOffset = br.Position;

                int subType = br.ReadInt32();
                int size = br.ReadInt32();
                int preDataCount = br.ReadFXR1Varint();
                int offsetToPreDataNumbers = br.ReadFXR1Varint();
                int offsetToPreDataParams = br.ReadFXR1Varint();

                int offsetToParentEffect = br.ReadFXR1Varint();
                var parentEffect = env.GetEffect(br, offsetToParentEffect);

                FXBehavior data;

                switch (subType)
                {
                    case 27: data = new FXBehaviorType27(); break;
                    case 28: data = new FXBehaviorType28(); break;
                    case 29: data = new FXBehaviorType29(); break;
                    case 30: data = new FXBehaviorType30(); break;
                    case 31: data = new FXBehaviorType31(); break;
                    case 32: data = new FXBehaviorType32(); break;
                    case 40: data = new FXBehaviorType40(); break;
                    case 43: data = new FXBehaviorType43(); break;
                    case 55: data = new FXBehaviorType55(); break;
                    case 59: data = new FXBehaviorType59(); break;
                    case 61: data = new FXBehaviorType61(); break;
                    case 66: data = new FXBehaviorType66(); break;
                    case 70: data = new FXBehaviorType70(); break;
                    case 71: data = new FXBehaviorType71(); break;
                    case 84: data = new FXBehaviorType84(); break;
                    case 105: data = new FXBehaviorType105(); break;
                    case 107: data = new FXBehaviorType107(); break;
                    case 108: data = new FXBehaviorType108(); break;
                    case 117: data = new FXBehaviorType117(); break;
                    default: throw new NotImplementedException();
                }

                env.RegisterOffset(startOffset, data);

                //TEMPORARY
                data.DEBUG_SizeOnRead = size;

                data.InnerRead(br, env);

                //data.TEMP_DATA = br.GetBytes(startOffset, size);

                data.ContainingParamList = parentEffect;

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
                    data.PreDatas[i].Data = FXField.Read(br, env);
                }
                br.StepOut();

                //the packed shit from switch(subType) all goes here during write?

                br.Position = startOffset + size;

                return data;
            }

            public class FXBehaviorType27 : FXBehavior
            {
                public override int Type => 27;

                public float Unk1;
                public float Unk2;
                public float Unk3;
                public float Unk4;
                public int TextureID;
                public int Unk6;
                public FXField Unk7_1;
                public FXField Unk7_2;
                public FXField Unk7_3;
                public FXField Unk7_4;
                public FXField Unk7_5;
                public FXField Unk7_6;
                public FXField Unk7_7;
                public FXField Unk7_8;
                public FXField Unk7_9;
                public FXField Unk7_10;
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

                    Unk7_1 = FXField.Read(br, env);
                    Unk7_2 = FXField.Read(br, env);
                    Unk7_3 = FXField.Read(br, env);
                    Unk7_4 = FXField.Read(br, env);
                    Unk7_5 = FXField.Read(br, env);
                    Unk7_6 = FXField.Read(br, env);
                    Unk7_7 = FXField.Read(br, env);
                    Unk7_8 = FXField.Read(br, env);
                    Unk7_9 = FXField.Read(br, env);
                    Unk7_10 = FXField.Read(br, env);

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



            public class FXBehaviorType28 : FXBehavior
            {
                public override int Type => 28;

                public FXField Unk1;
                public FXField Unk2;
                public FXField Unk3;
                public int Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = FXField.Read(br, env);
                    Unk2 = FXField.Read(br, env);
                    Unk3 = FXField.Read(br, env);
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


            public class FXBehaviorType29 : FXBehavior
            {
                public override int Type => 29;

                public FXField Unk1;
                public FXField Unk2;
                public FXField Unk3;
                public FXField Unk4;
                public FXField Unk5;
                public int Unk6;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = FXField.Read(br, env);
                    Unk2 = FXField.Read(br, env);
                    Unk3 = FXField.Read(br, env);
                    Unk4 = FXField.Read(br, env);
                    Unk5 = FXField.Read(br, env);
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

            public class FXBehaviorType30 : FXBehavior
            {
                public override int Type => 30;

                public FXField Unk1_1;
                public FXField Unk1_2;
                public FXField Unk1_3;
                public FXField Unk1_4;
                public float Unk2;
                public int Unk3;
                public int Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = FXField.Read(br, env);
                    Unk1_2 = FXField.Read(br, env);
                    Unk1_3 = FXField.Read(br, env);
                    Unk1_4 = FXField.Read(br, env);
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


            public class FXBehaviorType31 : FXBehavior
            {
                public override int Type => 31;

                public FXField Unk1_1;
                public FXField Unk1_2;
                public FXField Unk1_3;
                public FXField Unk1_4;
                public int Unk2;
                public int Unk3;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = FXField.Read(br, env);
                    Unk1_2 = FXField.Read(br, env);
                    Unk1_3 = FXField.Read(br, env);
                    Unk1_4 = FXField.Read(br, env);
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

            public class FXBehaviorType32 : FXBehavior
            {
                public override int Type => 32;

                public FXField OffsetX;
                public FXField OffsetY;
                public FXField OffsetZ;
                public FXField Unk1_1;
                public FXField Unk1_2;
                public FXField Unk1_3;
                public int Unk2;
                public int Unk3;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    OffsetX = FXField.Read(br, env);
                    OffsetY = FXField.Read(br, env);
                    OffsetZ = FXField.Read(br, env);
                    Unk1_1 = FXField.Read(br, env);
                    Unk1_2 = FXField.Read(br, env);
                    Unk1_3 = FXField.Read(br, env);
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

            public class FXBehaviorType40 : FXBehavior
            {
                public override int Type => 40;

                public float Unk1;
                public int TextureID;
                public int Unk3;
                public int Unk4;
                public int Unk5;
                public FXField Unk6_1;
                public FXField Unk6_2;
                public FXField Unk6_3;
                public FXField Unk6_4;
                public float Unk7;
                public float Unk8;
                public int Unk9;
                public int Unk10;
                public FXField Unk11_1;
                public FXField Unk11_2;
                public FXField Unk11_3;
                public FXField Unk11_4;
                public int Unk12;
                public int Unk13;
                public FXField Unk14;
                public int Unk15;
                public float Unk16;
                public FXField Unk17_1;
                public FXField Unk17_2;
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
                    Unk6_1 = FXField.Read(br, env);
                    Unk6_2 = FXField.Read(br, env);
                    Unk6_3 = FXField.Read(br, env);
                    Unk6_4 = FXField.Read(br, env);
                    Unk7 = br.ReadSingle();
                    Unk8 = br.ReadSingle();
                    Unk9 = br.ReadInt32();
                    Unk10 = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk11_1 = FXField.Read(br, env);
                    Unk11_2 = FXField.Read(br, env);
                    Unk11_3 = FXField.Read(br, env);
                    Unk11_4 = FXField.Read(br, env);
                    Unk12 = br.ReadInt32();
                    Unk13 = br.ReadInt32();

                    br.AssertInt32(0);

                    Unk14 = FXField.Read(br, env);
                    Unk15 = br.ReadInt32();
                    Unk16 = br.ReadSingle();
                    Unk17_1 = FXField.Read(br, env);
                    Unk17_2 = FXField.Read(br, env);
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

            public class FXBehaviorType43 : FXBehavior
            {
                public override int Type => 43;

                public float Unk1;
                public int TextureID;
                public int Unk2;
                public int Unk3;
                public int Unk4;
                public int Unk5;
                public int Unk6;
                public FXField Unk7_1;
                public FXField Unk7_2;
                public FXField Unk7_3;
                public FXField Unk7_4;
                public FXField Unk7_5;
                public FXField Unk7_6;
                public FXField Unk7_7;
                public FXField Unk7_8;
                public FXField Unk7_9;
                public FXField Unk7_10;
                public FXField Unk7_11;
                public FXField Unk7_12;
                public FXField Unk7_13;
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
                    Unk7_1 = FXField.Read(br, env);
                    Unk7_2 = FXField.Read(br, env);
                    Unk7_3 = FXField.Read(br, env);
                    Unk7_4 = FXField.Read(br, env);
                    Unk7_5 = FXField.Read(br, env);
                    Unk7_6 = FXField.Read(br, env);
                    Unk7_7 = FXField.Read(br, env);
                    Unk7_8 = FXField.Read(br, env);
                    Unk7_9 = FXField.Read(br, env);
                    Unk7_10 = FXField.Read(br, env);
                    Unk7_11 = FXField.Read(br, env);
                    Unk7_12 = FXField.Read(br, env);
                    Unk7_13 = FXField.Read(br, env);
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

            public class FXBehaviorType55 : FXBehavior
            {
                public override int Type => 55;

                public FXField Unk1;
                public FXField Unk2;
                public FXField Unk3;
                public float Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = FXField.Read(br, env);
                    Unk2 = FXField.Read(br, env);
                    Unk3 = FXField.Read(br, env);

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

            public class FXBehaviorType59 : FXBehavior
            {
                public override int Type => 59;

                public float Unk1;
                public int TextureID;
                public int Unk2;
                public int Unk3;
                public FXField Unk4_1;
                public FXField Unk4_2;
                public FXField Unk4_3;
                public FXField Unk4_4;
                public FXField Unk4_5;
                public int Unk5;
                public int Unk6;
                public FXField Unk7_1;
                public FXField Unk7_2;
                public FXField Unk7_3;
                public FXField Unk7_4;
                public FXField Unk7_5;
                public FXField Unk7_6;
                public FXField Unk7_7;
                public FXField Unk7_8;
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
                    Unk4_1 = FXField.Read(br, env);
                    Unk4_2 = FXField.Read(br, env);
                    Unk4_3 = FXField.Read(br, env);
                    Unk4_4 = FXField.Read(br, env);
                    Unk4_5 = FXField.Read(br, env);
                    Unk5 = br.ReadInt32();
                    Unk6 = br.ReadInt32();
                    Unk7_1 = FXField.Read(br, env);
                    Unk7_2 = FXField.Read(br, env);
                    Unk7_3 = FXField.Read(br, env);
                    Unk7_4 = FXField.Read(br, env);
                    Unk7_5 = FXField.Read(br, env);
                    Unk7_6 = FXField.Read(br, env);
                    Unk7_7 = FXField.Read(br, env);
                    Unk7_8 = FXField.Read(br, env);
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

            public class FXBehaviorType61 : FXBehavior
            {
                public override int Type => 61;

                public int TextureID;
                public int Unk1;
                public int Unk2;
                public int Unk3_1;
                public int Unk3_2;
                public FXField Unk4_1;
                public FXField Unk4_2;
                public FXField Unk4_3;
                public int Unk5;
                public float Unk6;
                public FXField Unk7;
                public int Unk8;
                public int Unk9;
                public FXField Unk10_1;
                public FXField Unk10_2;
                public FXField Unk10_3;
                public FXField Unk10_4;
                public FXField Unk10_5;
                public FXField Unk10_6;
                public FXField Unk10_7;
                public FXField Unk10_8;
                public FXField Unk10_9;
                public FXField Unk10_10;
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
                    Unk4_1 = FXField.Read(br, env);
                    Unk4_2 = FXField.Read(br, env);
                    Unk4_3 = FXField.Read(br, env);

                    br.AssertInt32(0);
                    br.AssertInt32(0);

                    Unk5 = br.ReadInt32();
                    Unk6 = br.ReadSingle();

                    br.AssertInt32(0);

                    Unk7 = FXField.Read(br, env);
                    Unk8 = br.ReadInt32();
                    Unk9 = br.ReadInt32();
                    Unk10_1 = FXField.Read(br, env);
                    Unk10_2 = FXField.Read(br, env);
                    Unk10_3 = FXField.Read(br, env);
                    Unk10_4 = FXField.Read(br, env);
                    Unk10_5 = FXField.Read(br, env);
                    Unk10_6 = FXField.Read(br, env);
                    Unk10_7 = FXField.Read(br, env);
                    Unk10_8 = FXField.Read(br, env);
                    Unk10_9 = FXField.Read(br, env);
                    Unk10_10 = FXField.Read(br, env);
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


            public class FXBehaviorType66 : FXBehavior
            {
                public override int Type => 66;

                public float Unk1;
                public float Unk2;
                public int Unk3;
                public float Unk4;
                public int Unk5;

                public int DS1R_Unk0;

                public FXField Unk6_1;
                public FXField Unk6_2;
                public FXField Unk6_3;
                public FXField Unk6_4;
                public FXField Unk6_5;
                public FXField Unk6_6;
                public FXField Unk6_7;
                public FXField Unk6_8;
                public FXField Unk6_9;
                public FXField Unk6_10;
                public FXField Unk6_11;
                public FXField Unk6_12;
                public FXField Unk6_13;
                public FXField Unk6_14;
                public FXField Unk6_15;
                public FXField Unk6_16;
                public FXField Unk6_17;
                public FXField Unk6_18;
                public FXField Unk6_19;
                public FXField Unk6_20;
                public FXField Unk6_21;
                public FXField Unk6_22;
                public FXField Unk6_23;
                public FXField Unk6_24;
                public FXField Unk6_25;
                public FXField Unk6_26;

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

                    Unk6_1 = FXField.Read(br, env);
                    Unk6_2 = FXField.Read(br, env);
                    Unk6_3 = FXField.Read(br, env);
                    Unk6_4 = FXField.Read(br, env);
                    Unk6_5 = FXField.Read(br, env);
                    Unk6_6 = FXField.Read(br, env);
                    Unk6_7 = FXField.Read(br, env);
                    Unk6_8 = FXField.Read(br, env);
                    Unk6_9 = FXField.Read(br, env);
                    Unk6_10 = FXField.Read(br, env);
                    Unk6_11 = FXField.Read(br, env);
                    Unk6_12 = FXField.Read(br, env);
                    Unk6_13 = FXField.Read(br, env);
                    Unk6_14 = FXField.Read(br, env);
                    Unk6_15 = FXField.Read(br, env);
                    Unk6_16 = FXField.Read(br, env);
                    Unk6_17 = FXField.Read(br, env);
                    Unk6_18 = FXField.Read(br, env);
                    Unk6_19 = FXField.Read(br, env);
                    Unk6_20 = FXField.Read(br, env);
                    Unk6_21 = FXField.Read(br, env);
                    Unk6_22 = FXField.Read(br, env);
                    Unk6_23 = FXField.Read(br, env);
                    Unk6_24 = FXField.Read(br, env);
                    Unk6_25 = FXField.Read(br, env);
                    Unk6_26 = FXField.Read(br, env);

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

            public class FXBehaviorType70 : FXBehavior
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
                public FXField Unk9_1;
                public FXField Unk9_2;
                public FXField Unk9_3;
                public FXField Unk9_4;
                public FXField Unk9_5;
                public FXField Unk9_6;
                public FXField Unk9_7;
                public FXField Unk9_8;
                public FXField Unk9_9;
                public FXField Unk9_10;
                public FXField Unk9_11;
                public FXField Unk9_12;
                public FXField Unk9_13;
                public FXField Unk9_14;
                public FXField Unk9_15;
                public FXField Unk9_16;
                public FXField Unk9_17;
                public FXField Unk9_18;
                public FXField Unk9_19;
                public FXField Unk9_20;
                public FXField Unk9_21;
                public FXField Unk9_22;
                public FXField Unk9_23;
                public FXField Unk9_24;
                public FXField Unk9_25;
                public FXField Unk9_26;
                public FXField Unk9_27;
                public FXField Unk9_28;
                public FXField Unk9_29;
                public FXField Unk9_30;
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
                    Unk9_1 = FXField.Read(br, env);
                    Unk9_2 = FXField.Read(br, env);
                    Unk9_3 = FXField.Read(br, env);
                    Unk9_4 = FXField.Read(br, env);
                    Unk9_5 = FXField.Read(br, env);
                    Unk9_6 = FXField.Read(br, env);
                    Unk9_7 = FXField.Read(br, env);
                    Unk9_8 = FXField.Read(br, env);
                    Unk9_9 = FXField.Read(br, env);
                    Unk9_10 = FXField.Read(br, env);
                    Unk9_11 = FXField.Read(br, env);
                    Unk9_12 = FXField.Read(br, env);
                    Unk9_13 = FXField.Read(br, env);
                    Unk9_14 = FXField.Read(br, env);
                    Unk9_15 = FXField.Read(br, env);
                    Unk9_16 = FXField.Read(br, env);
                    Unk9_17 = FXField.Read(br, env);
                    Unk9_18 = FXField.Read(br, env);
                    Unk9_19 = FXField.Read(br, env);
                    Unk9_20 = FXField.Read(br, env);
                    Unk9_21 = FXField.Read(br, env);
                    Unk9_22 = FXField.Read(br, env);
                    Unk9_23 = FXField.Read(br, env);
                    Unk9_24 = FXField.Read(br, env);
                    Unk9_25 = FXField.Read(br, env);
                    Unk9_26 = FXField.Read(br, env);
                    Unk9_27 = FXField.Read(br, env);
                    Unk9_28 = FXField.Read(br, env);
                    Unk9_29 = FXField.Read(br, env);
                    Unk9_30 = FXField.Read(br, env);
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

            public class FXBehaviorType71 : FXBehavior
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
                public FXField Unk11_1;
                public FXField Unk11_2;
                public FXField Unk11_3;
                public FXField Unk11_4;
                public FXField Unk11_5;
                public FXField Unk11_6;
                public FXField Unk11_7;
                public FXField Unk11_8;
                public FXField Unk11_9;
                public FXField Unk11_10;
                public int Unk12;
                public int Unk13;
                public FXField Unk14_1;
                public FXField Unk14_2;
                public FXField Unk14_3;
                public FXField Unk14_4;
                public FXField Unk14_5;
                public FXField Unk14_6;
                public FXField Unk14_7;
                public FXField Unk14_8;
                public FXField Unk14_9;
                public FXField Unk14_10;

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
                    Unk11_1 = FXField.Read(br, env);
                    Unk11_2 = FXField.Read(br, env);
                    Unk11_3 = FXField.Read(br, env);
                    Unk11_4 = FXField.Read(br, env);
                    Unk11_5 = FXField.Read(br, env);
                    Unk11_6 = FXField.Read(br, env);
                    Unk11_7 = FXField.Read(br, env);
                    Unk11_8 = FXField.Read(br, env);
                    Unk11_9 = FXField.Read(br, env);
                    Unk11_10 = FXField.Read(br, env);
                    Unk12 = br.ReadInt32();
                    Unk13 = br.ReadInt32();
                    Unk14_1 = FXField.Read(br, env);
                    Unk14_2 = FXField.Read(br, env);
                    Unk14_3 = FXField.Read(br, env);
                    Unk14_4 = FXField.Read(br, env);
                    Unk14_5 = FXField.Read(br, env);
                    Unk14_6 = FXField.Read(br, env);
                    Unk14_7 = FXField.Read(br, env);
                    Unk14_8 = FXField.Read(br, env);
                    Unk14_9 = FXField.Read(br, env);
                    Unk14_10 = FXField.Read(br, env);

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

            public class FXBehaviorType84 : FXBehavior
            {
                public override int Type => 84;

                public FXField Unk1_1;
                public FXField Unk1_2;
                public FXField Unk1_3;
                public float Unk2;
                public FXField Unk3;
                public int Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = FXField.Read(br, env);
                    Unk1_2 = FXField.Read(br, env);
                    Unk1_3 = FXField.Read(br, env);
                    br.AssertInt32(0);
                    Unk2 = br.ReadSingle();
                    Unk3 = FXField.Read(br, env);
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

            public class FXBehaviorType105 : FXBehavior
            {
                public override int Type => 105;

                public FXField Unk1_1;
                public FXField Unk1_2;
                public FXField Unk1_3;
                public float Unk2;
                public FXField Unk3;
                public int Unk4;
                public FXField Unk5;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = FXField.Read(br, env);
                    Unk1_2 = FXField.Read(br, env);
                    Unk1_3 = FXField.Read(br, env);
                    br.AssertInt32(0);
                    Unk2 = br.ReadSingle();
                    Unk3 = FXField.Read(br, env);
                    Unk4 = br.ReadFXR1Varint();
                    Unk5 = FXField.Read(br, env);
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

            public class FXBehaviorType107 : FXBehavior
            {
                public override int Type => 107;

                public float Unk1;
                public int TextureID;
                public int Unk2;
                public FXField Unk3;
                public FXField Unk4;
                public FXField Unk5;
                public FXField Unk6;
                public FXField Unk7;
                public FXField Unk8;
                public FXField Unk9;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1 = br.ReadSingle();
                    br.AssertInt32(0);
                    TextureID = br.ReadInt32();
                    Unk2 = br.ReadInt32();
                    Unk3 = FXField.Read(br, env);
                    Unk4 = FXField.Read(br, env);
                    Unk5 = FXField.Read(br, env);
                    Unk6 = FXField.Read(br, env);
                    Unk7 = FXField.Read(br, env);
                    Unk8 = FXField.Read(br, env);
                    Unk9 = FXField.Read(br, env);
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

            public class FXBehaviorType108 : FXBehavior
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
                public FXField Scale1X;
                public FXField Scale1Y;
                public FXField Scale1Z;
                public FXField Scale2X;
                public FXField Scale2Y;
                public FXField Scale2Z;
                public FXField RotSpeedX;
                public FXField RotSpeedY;
                public FXField RotSpeedZ;
                public FXField RotVal2X;
                public FXField RotVal2Y;
                public FXField RotVal2Z;
                public int Unk9;
                public int Unk10;
                public FXField Unk11_1;
                public FXField Unk11_2;
                public FXField Unk11_3;
                public FXField Unk11_4;
                public FXField Unk11_5;
                public FXField Unk11_6;
                public FXField Color1R;
                public FXField Color1G;
                public FXField Color1B;
                public FXField Color1A;
                public FXField Color2R;
                public FXField Color2G;
                public FXField Color2B;
                public FXField Color2A;
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

                    Scale1X = FXField.Read(br, env);
                    Scale1Y = FXField.Read(br, env);
                    Scale1Z = FXField.Read(br, env);
                    Scale2X = FXField.Read(br, env);
                    Scale2Y = FXField.Read(br, env);
                    Scale2Z = FXField.Read(br, env);
                    RotSpeedX = FXField.Read(br, env);
                    RotSpeedY = FXField.Read(br, env);
                    RotSpeedZ = FXField.Read(br, env);
                    RotVal2X = FXField.Read(br, env);
                    RotVal2Y = FXField.Read(br, env);
                    RotVal2Z = FXField.Read(br, env);
                    Unk9 = br.ReadInt32();
                    Unk10 = br.ReadInt32();
                    Unk11_1 = FXField.Read(br, env);
                    Unk11_2 = FXField.Read(br, env);
                    Unk11_3 = FXField.Read(br, env);
                    Unk11_4 = FXField.Read(br, env);
                    Unk11_5 = FXField.Read(br, env);
                    Unk11_6 = FXField.Read(br, env);
                    Color1R = FXField.Read(br, env);
                    Color1G = FXField.Read(br, env);
                    Color1B = FXField.Read(br, env);
                    Color1A = FXField.Read(br, env);
                    Color2R = FXField.Read(br, env);
                    Color2G = FXField.Read(br, env);
                    Color2B = FXField.Read(br, env);
                    Color2A = FXField.Read(br, env);
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

            public class FXBehaviorType117 : FXBehavior
            {
                public override int Type => 117;

                public FXField Unk1_1;
                public FXField Unk1_2;
                public FXField Unk1_3;
                public FXField Unk1_4;
                public FXField Unk1_5;
                public FXField Unk1_6;
                public int Unk2;
                public int Unk3;
                public FXField Unk4;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    Unk1_1 = FXField.Read(br, env);
                    Unk1_2 = FXField.Read(br, env);
                    Unk1_3 = FXField.Read(br, env);
                    Unk1_4 = FXField.Read(br, env);
                    Unk1_5 = FXField.Read(br, env);
                    Unk1_6 = FXField.Read(br, env);
                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                    Unk4 = FXField.Read(br, env);
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


        public class BehaviorRef : FXBehavior
        {
            public override int Type => -1;

            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializePreDatas() => false;

            public BehaviorRef(FXBehavior refVal)
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
