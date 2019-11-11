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
        public abstract class ASTPool2
        {
            [XmlAttribute]
            public int SubType;

            [XmlIgnore]
            public AST ParentAst;

            public List<PreDataEntry> PreDatas;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            //public byte[] TEMP_DATA;

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

                //TEMPORARY
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
                        Unknown = br.ReadInt32()
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
            public Param[] DS1R_Unk5;
            public float DS1R_Unk6;
            public int DS1R_Unk7;
            public int DS1R_Unk8;
            public int DS1R_Unk9;
            public int DS1R_Unk10;
            public int DS1R_Unk11;

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
                    DS1R_Unk5 = Param.ReadMany(br, env, 5);
                    DS1R_Unk6 = br.ReadInt32();
                    DS1R_Unk7 = br.ReadInt32();
                    DS1R_Unk8 = br.ReadInt32();
                    DS1R_Unk9 = br.ReadInt32();
                    DS1R_Unk10 = br.ReadInt32();
                    DS1R_Unk11 = br.ReadInt32();
                }
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
            public int Unk10;

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

                Unk10 = br.ReadInt32();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                var DEBUG_POS = br.Position;
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
