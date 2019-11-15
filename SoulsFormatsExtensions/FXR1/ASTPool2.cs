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
        [XmlInclude(typeof(ASTPool2Type27))]
        [XmlInclude(typeof(ASTPool2Type28))]
        [XmlInclude(typeof(ASTPool2Type29))]
        [XmlInclude(typeof(ASTPool2Type30))]
        [XmlInclude(typeof(ASTPool2Type31))]
        [XmlInclude(typeof(ASTPool2Type32))]
        [XmlInclude(typeof(ASTPool2Type40))]
        [XmlInclude(typeof(ASTPool2Type43))]
        [XmlInclude(typeof(ASTPool2Type55))]
        [XmlInclude(typeof(ASTPool2Type59))]
        [XmlInclude(typeof(ASTPool2Type61))]
        [XmlInclude(typeof(ASTPool2Type66))]
        [XmlInclude(typeof(ASTPool2Type70))]
        [XmlInclude(typeof(ASTPool2Type71))]
        [XmlInclude(typeof(ASTPool2Type84))]
        [XmlInclude(typeof(ASTPool2Type105))]
        [XmlInclude(typeof(ASTPool2Type107))]
        [XmlInclude(typeof(ASTPool2Type108))]
        [XmlInclude(typeof(ASTPool2Type117))]
        [XmlInclude(typeof(ASTPool2Ref))]
        public abstract class ASTPool2 : XIDable
        {
            [XmlAttribute]
            public int SubType;
            public AST ParentAst;
            public List<PreDataEntry> PreDatas;

            [XmlIgnore]
            internal int SizeOnRead = -1;

            public virtual bool ShouldSerializeSubType() => true;
            public virtual bool ShouldSerializeParentAst() => true;
            public virtual bool ShouldSerializePreDatas() => true;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            internal override void ToXIDs(FXR1 fxr)
            {
                ParentAst = fxr.ReferenceAST(ParentAst);
                InnerToXIDs(fxr);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                ParentAst = fxr.DereferenceAST(ParentAst);
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

            protected void WriteParamArray(Param[] p, int expectedLength)
            {
                if (p.Length != expectedLength)
                    throw new InvalidOperationException("Invalid number of params in param array.");

                foreach (var x in p)
                    WriteParam(x);
            }

            protected void WriteParam(Param p)
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

                bw.WriteInt32(SubType);
                bw.ReserveInt32("ASTPool2.Size");
                bw.WriteFXR1Varint(PreDatas.Count);
                bw.ReserveInt32("ASTPool2.PreDatas.Numbers");
                bw.ReserveInt32("ASTPool2.PreDatas.Params");
                env.RegisterPointer(ParentAst, useExistingPointerOnly: true);

                paramWriteLocations.Clear();
                currentWriteEnvironment = env;
                InnerWrite(bw, env);

                if (bw.VarintLong)
                    bw.Pad(8);

                bw.FillInt32("ASTPool2.PreDatas.Numbers", (int)bw.Position);
                for (int i = 0; i < PreDatas.Count; i++)
                {
                    bw.WriteInt32(PreDatas[i].Unk);
                }

                bw.FillInt32("ASTPool2.PreDatas.Params", (int)bw.Position);
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

                if (SizeOnRead != -1 && writtenSize != SizeOnRead)
                    throw new Exception("sdfsgfdsgfds");

                bw.FillInt32("ASTPool2.Size", writtenSize);

                bw.Pad(16); //Might be 16?

                paramWriteLocations.Clear();
                currentWriteEnvironment = null;
            }

            public static ASTPool2 Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long startOffset = br.Position;

                int subType = br.ReadInt32();
                int size = br.ReadInt32();
                int preDataCount = br.ReadFXR1Varint();
                int offsetToPreDataNumbers = br.ReadFXR1Varint();
                int offsetToPreDataParams = br.ReadFXR1Varint();

                int offsetToParentAst = br.ReadFXR1Varint();
                var parentAst = env.GetAST(br, offsetToParentAst);

                ASTPool2 data;

                switch (subType)
                {
                    case 27: data = new ASTPool2Type27(); break;
                    case 28: data = new ASTPool2Type28(); break;
                    case 29: data = new ASTPool2Type29(); break;
                    case 30: data = new ASTPool2Type30(); break;
                    case 31: data = new ASTPool2Type31(); break;
                    case 32: data = new ASTPool2Type32(); break;
                    case 40: data = new ASTPool2Type40(); break;
                    case 43: data = new ASTPool2Type43(); break;
                    case 55: data = new ASTPool2Type55(); break;
                    case 59: data = new ASTPool2Type59(); break;
                    case 61: data = new ASTPool2Type61(); break;
                    case 66: data = new ASTPool2Type66(); break;
                    case 70: data = new ASTPool2Type70(); break;
                    case 71: data = new ASTPool2Type71(); break;
                    case 84: data = new ASTPool2Type84(); break;
                    case 105: data = new ASTPool2Type105(); break;
                    case 107: data = new ASTPool2Type107(); break;
                    case 108: data = new ASTPool2Type108(); break;
                    case 117: data = new ASTPool2Type117(); break;
                    default: throw new NotImplementedException();
                }

                env.RegisterOffset(startOffset, data);

                //TEMPORARY
                data.SizeOnRead = size;

                data.InnerRead(br, env);

                data.SubType = subType;

                //data.TEMP_DATA = br.GetBytes(startOffset, size);

                data.ParentAst = parentAst;

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
        }


        public class ASTPool2Ref : ASTPool2
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeSubType() => false;
            public override bool ShouldSerializeParentAst() => false;
            public override bool ShouldSerializePreDatas() => false;

            public ASTPool2Ref(ASTPool2 refVal)
            {
                ReferenceXID = refVal?.XID;
            }

            public ASTPool2Ref()
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




        public class ASTPool2Type27 : ASTPool2
        {
            public float Unk1;
            public float Unk2;
            public float Unk3;
            public float Unk4;
            public int TextureID;
            public int Unk6;
            public Param[] Unk7;
            public int Unk8;
            public int Unk9;
            public int Unk10;
            public float Unk11;
            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

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

                Unk7 = Param.ReadMany(br, env, 10);
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();
                Unk11 = br.ReadSingle();

                if (br.VarintLong)
                {
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadInt32();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                }
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

                WriteParamArray(Unk7, 10);
                bw.WriteInt32(Unk8);
                bw.WriteInt32(Unk9);
                bw.WriteInt32(Unk10);
                bw.WriteSingle(Unk11);

                if (bw.VarintLong)
                {
                    WriteParamArray(DS1R_Unk1, 5);
                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }



        public class ASTPool2Type28 : ASTPool2
        {
            public Param[] Unk1;
            public int Unk2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 3);
                Unk2 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParamArray(Unk1, 3);

                bw.WriteInt32(Unk2);
            }
        }


        public class ASTPool2Type29 : ASTPool2
        {
            public Param[] Unk1;
            public int Unk2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 5);
                Unk2 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParamArray(Unk1, 5);
                bw.WriteInt32(Unk2);
            }
        }

        public class ASTPool2Type30 : ASTPool2
        {
            public Param[] Unk1;
            public float Unk2;
            public int Unk3;
            public int Unk4;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 4);
                Unk2 = br.ReadSingle();
                Unk3 = br.ReadInt32();
                Unk4 = br.ReadFXR1Varint();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParamArray(Unk1, 4);

                bw.WriteSingle(Unk2);
                bw.WriteInt32(Unk3);
                bw.WriteFXR1Varint(Unk4);
            }
        }


        public class ASTPool2Type31 : ASTPool2
        {
            public Param[] Unk1;
            public int Unk2;
            public int Unk3;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 4);
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParamArray(Unk1, 4);

                bw.WriteInt32(Unk2);
                bw.WriteInt32(Unk3);
            }
        }

        public class ASTPool2Type32 : ASTPool2
        {
            public Param OffsetX;
            public Param OffsetY;
            public Param OffsetZ;
            public Param[] Unk1;
            public int Unk2;
            public int Unk3;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                OffsetX = Param.Read(br, env);
                OffsetY = Param.Read(br, env);
                OffsetZ = Param.Read(br, env);
                Unk1 = Param.ReadMany(br, env, 3);
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParam(OffsetX);
                WriteParam(OffsetY);
                WriteParam(OffsetZ);
                WriteParamArray(Unk1, 3);
                bw.WriteInt32(Unk2);
                bw.WriteInt32(Unk3);
            }
        }

        public class ASTPool2Type40 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk3;
            public int Unk4;
            public int Unk5;
            public Param[] Unk6;
            public float Unk7;
            public float Unk8;
            public int Unk9;
            public int Unk10;
            public Param[] Unk11;
            public int Unk12;
            public int Unk13;
            public Param Unk14;
            public int Unk15;
            public float Unk16;
            public Param[] Unk17;
            public int Unk18;

            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(0);

                Unk1 = br.ReadSingle();
                TextureID = br.ReadInt32();

                br.AssertInt32(0);

                Unk3 = br.ReadInt32();
                Unk4 = br.ReadInt32();
                Unk5 = br.ReadInt32();
                Unk6 = Param.ReadMany(br, env, 4);
                Unk7 = br.ReadSingle();
                Unk8 = br.ReadSingle();
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();

                br.AssertInt32(0);

                Unk11 = Param.ReadMany(br, env, 4);
                Unk12 = br.ReadInt32();
                Unk13 = br.ReadInt32();

                br.AssertInt32(0);

                Unk14 = Param.Read(br, env);
                Unk15 = br.ReadInt32();
                Unk16 = br.ReadSingle();
                Unk17 = Param.ReadMany(br, env, 2);
                Unk18 = br.ReadInt32();

                if (br.VarintLong)
                {
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadSingle();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                }
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
                WriteParamArray(Unk6, 4);
                bw.WriteSingle(Unk7);
                bw.WriteSingle(Unk8);
                bw.WriteInt32(Unk9);
                bw.WriteInt32(Unk10);

                bw.WriteInt32(0);

                WriteParamArray(Unk11, 4);
                bw.WriteInt32(Unk12);
                bw.WriteInt32(Unk13);

                bw.WriteInt32(0);

                WriteParam(Unk14);
                bw.WriteInt32(Unk15);
                bw.WriteSingle(Unk16);
                WriteParamArray(Unk17, 2);
                bw.WriteInt32(Unk18);

                if (bw.VarintLong)
                {
                    WriteParamArray(DS1R_Unk1, 5);

                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }

        public class ASTPool2Type43 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk2;
            public int Unk3;
            public int Unk4;
            public int Unk5;
            public int Unk6;
            public Param[] Unk7;
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
                Unk7 = Param.ReadMany(br, env, 13);
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
                WriteParamArray(Unk7, 13);
                bw.WriteInt32(Unk8);
            }
        }

        public class ASTPool2Type55 : ASTPool2
        {
            public Param[] Unk1;
            public float Unk2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 3);
                br.AssertInt32(0);
                Unk2 = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParamArray(Unk1, 3);

                bw.WriteInt32(0);
                bw.WriteSingle(Unk2);
            }
        }

        public class ASTPool2Type59 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk2;
            public int Unk3;
            public Param[] Unk4;
            public int Unk5;
            public int Unk6;
            public Param[] Unk7;
            public int Unk8;
            public int Unk9;
            public int Unk10;
            public float Unk11;

            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = br.ReadSingle();

                br.AssertInt32(0);

                TextureID = br.ReadInt32();

                br.AssertInt32(0);

                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
                Unk4 = Param.ReadMany(br, env, 5);
                Unk5 = br.ReadInt32();
                Unk6 = br.ReadInt32();
                Unk7 = Param.ReadMany(br, env, 8);
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();

                br.AssertInt32(0);

                Unk10 = br.ReadInt32();
                Unk11 = br.ReadSingle();

                br.AssertInt32(0);

                if (br.VarintLong)
                {
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadSingle();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                }
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteSingle(Unk1);

                bw.WriteInt32(0);

                bw.WriteInt32(TextureID);

                bw.WriteInt32(0);

                bw.WriteInt32(Unk2);
                bw.WriteInt32(Unk3);
                WriteParamArray(Unk4, 5);
                bw.WriteInt32(Unk5);
                bw.WriteInt32(Unk6);
                WriteParamArray(Unk7, 8);
                bw.WriteInt32(Unk8);
                bw.WriteInt32(Unk9);

                bw.WriteInt32(0);

                bw.WriteInt32(Unk10);
                bw.WriteSingle(Unk11);

                bw.WriteInt32(0);

                if (bw.VarintLong)
                {
                    WriteParamArray(DS1R_Unk1, 5);
                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }

        public class ASTPool2Type61 : ASTPool2
        {
            public int TextureID;
            public int Unk1;
            public int Unk2;
            public int Unk3;
            public Param[] Unk4;
            public int Unk5;
            public float Unk6;
            public Param Unk7;
            public int Unk8;
            public int Unk9;
            public Param[] Unk10;
            public int Unk11;
            public int Unk12;

            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(0);
                br.AssertInt32(0);

                TextureID = br.ReadInt32();
                Unk1 = br.ReadInt32();
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();

                br.AssertInt32(0);

                Unk4 = Param.ReadMany(br, env, 3);

                br.AssertInt32(0);
                br.AssertInt32(0);

                Unk5 = br.ReadInt32();
                Unk6 = br.ReadSingle();

                br.AssertInt32(0);

                Unk7 = Param.Read(br, env);
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();
                Unk10 = Param.ReadMany(br, env, 10);
                Unk11 = br.ReadInt32();
                Unk12 = br.ReadInt32();

                br.AssertInt32(0);
                br.AssertInt32(0);

                if (br.VarintLong)
                {
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadSingle();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                }
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(0);
                bw.WriteInt32(0);

                bw.WriteInt32(TextureID);
                bw.WriteInt32(Unk1);
                bw.WriteInt32(Unk2);
                bw.WriteInt32(Unk3);

                bw.WriteInt32(0);

                WriteParamArray(Unk4, 3);

                bw.WriteInt32(0);
                bw.WriteInt32(0);

                bw.WriteInt32(Unk5);
                bw.WriteSingle(Unk6);

                bw.WriteInt32(0);

                WriteParam(Unk7);
                bw.WriteInt32(Unk8);
                bw.WriteInt32(Unk9);
                WriteParamArray(Unk10, 10);
                bw.WriteInt32(Unk11);
                bw.WriteInt32(Unk12);

                bw.WriteInt32(0);
                bw.WriteInt32(0);

                if (bw.VarintLong)
                {
                    WriteParamArray(DS1R_Unk1, 5);
                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }


        public class ASTPool2Type66 : ASTPool2
        {
            public float Unk1;
            public float Unk2;
            public int Unk3;
            public float Unk4;
            public int Unk5;

            public int DS1R_Unk0;

            public Param[] Unk6;

            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

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

                Unk6 = Param.ReadMany(br, env, 26);

                br.AssertInt32(0);

                if (br.VarintLong)
                {
                    br.AssertInt32(0);
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadSingle();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
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

                WriteParamArray(Unk6, 26);

                bw.WriteInt32(0);

                if (bw.VarintLong)
                {
                    bw.WriteInt32(0);
                    WriteParamArray(DS1R_Unk1, 5);
                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }

        public class ASTPool2Type70 : ASTPool2
        {
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
            public Param[] Unk9;
            public int Unk10;
            public int Unk11;
            public int Unk12;
            public int Unk13;
            public float Unk14;
            public int Unk15;
            public float Unk16;
            public int Unk17;

            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

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
                Unk9 = Param.ReadMany(br, env, 30);
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
                {
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadSingle();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                }
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
                WriteParamArray(Unk9, 30);
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
                {
                    WriteParamArray(DS1R_Unk1, 5);
                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }

        public class ASTPool2Type71 : ASTPool2
        {
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
            public Param[] Unk11;
            public int Unk12;
            public int Unk13;
            public Param[] Unk14;

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

            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

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
                Unk11 = Param.ReadMany(br, env, 10);
                Unk12 = br.ReadInt32();
                Unk13 = br.ReadInt32();
                Unk14 = Param.ReadMany(br, env, 10);

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
                {
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadSingle();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                }
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
                WriteParamArray(Unk11, 10);
                bw.WriteInt32(Unk12);
                bw.WriteInt32(Unk13);
                WriteParamArray(Unk14, 10);
                
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
                {
                    WriteParamArray(DS1R_Unk1, 5);
                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }

        public class ASTPool2Type84 : ASTPool2
        {
            public Param[] Unk1;
            public float Unk2;
            public Param Unk3;
            public int Unk4;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 3);
                br.AssertInt32(0);
                Unk2 = br.ReadSingle();
                Unk3 = Param.Read(br, env);
                Unk4 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParamArray(Unk1, 3);
                bw.WriteInt32(0);
                bw.WriteSingle(Unk2);
                WriteParam(Unk3);
                bw.WriteInt32(Unk4);
            }
        }

        public class ASTPool2Type105 : ASTPool2
        {
            public Param[] Unk1;
            public float Unk2;
            public Param Unk3;
            public int Unk4;
            public Param Unk5;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 3);
                br.AssertInt32(0);
                Unk2 = br.ReadSingle();
                Unk3 = Param.Read(br, env);
                Unk4 = br.ReadFXR1Varint();
                Unk5 = Param.Read(br, env);
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                WriteParamArray(Unk1, 3);
                bw.WriteInt32(0);
                bw.WriteSingle(Unk2);
                WriteParam(Unk3);
                bw.WriteFXR1Varint(Unk4);
                WriteParam(Unk5);
            }
        }

        public class ASTPool2Type107 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk2;
            public Param[] Unk3;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = br.ReadSingle();
                br.AssertInt32(0);
                TextureID = br.ReadInt32();
                Unk2 = br.ReadInt32();
                Unk3 = Param.ReadMany(br, env, 7);
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteSingle(Unk1);
                bw.WriteInt32(0);
                bw.WriteInt32(TextureID);
                bw.WriteInt32(Unk2);
                WriteParamArray(Unk3, 7);
            }
        }

        public class ASTPool2Type108 : ASTPool2
        {
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
            public Param Rot1;
            public Param Rot2;
            public Param Rot3;
            public int Unk9;
            public int Unk10;
            public Param[] Unk11;
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

            public Param[] DS1R_Unk1;
            public float DS1R_Unk2;
            public int DS1R_Unk3;
            public int DS1R_Unk4;
            public int DS1R_Unk5;
            public int DS1R_Unk6;
            public int DS1R_Unk7;

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
                Rot1 = Param.Read(br, env);
                Rot2 = Param.Read(br, env);
                Rot3 = Param.Read(br, env);
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();
                Unk11 = Param.ReadMany(br, env, 6);
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
                {
                    DS1R_Unk1 = Param.ReadMany(br, env, 5);
                    DS1R_Unk2 = br.ReadSingle();
                    DS1R_Unk3 = br.ReadInt32();
                    DS1R_Unk4 = br.ReadInt32();
                    DS1R_Unk5 = br.ReadInt32();
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                }
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
                WriteParam(Rot1);
                WriteParam(Rot2);
                WriteParam(Rot3);
                bw.WriteInt32(Unk9);
                bw.WriteInt32(Unk10);
                WriteParamArray(Unk11, 6);
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
                {
                    WriteParamArray(DS1R_Unk1, 5);
                    bw.WriteSingle(DS1R_Unk2);
                    bw.WriteInt32(DS1R_Unk3);
                    bw.WriteInt32(DS1R_Unk4);
                    bw.WriteInt32(DS1R_Unk5);
                    bw.WriteInt32(DS1R_Unk6);
                    bw.WriteInt32(DS1R_Unk7);
                }
            }
        }

        public class ASTPool2Type117 : ASTPool2
        {
            public Param[] Unk1;
            public int Unk2;
            public int Unk3;
            public Param Unk4;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.ReadMany(br, env, 6);
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
                Unk4 = Param.Read(br, env);
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

    }
}
