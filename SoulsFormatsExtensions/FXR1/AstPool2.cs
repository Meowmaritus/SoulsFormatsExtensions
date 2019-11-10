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
        [XmlInclude(typeof(AstPool2Type27))]
        [XmlInclude(typeof(AstPool2Type28))]
        [XmlInclude(typeof(AstPool2Type29))]
        [XmlInclude(typeof(AstPool2Type30))]
        [XmlInclude(typeof(AstPool2Type31))]
        [XmlInclude(typeof(AstPool2Type32))]
        [XmlInclude(typeof(AstPool2Type40))]
        [XmlInclude(typeof(AstPool2Type43))]
        [XmlInclude(typeof(AstPool2Type55))]
        [XmlInclude(typeof(AstPool2Type59))]
        [XmlInclude(typeof(AstPool2Type61))]
        [XmlInclude(typeof(AstPool2Type66))]
        [XmlInclude(typeof(AstPool2Type70))]
        [XmlInclude(typeof(AstPool2Type71))]
        [XmlInclude(typeof(AstPool2Type84))]
        [XmlInclude(typeof(AstPool2Type105))]
        [XmlInclude(typeof(AstPool2Type107))]
        [XmlInclude(typeof(AstPool2Type108))]
        [XmlInclude(typeof(AstPool2Type117))]
        public abstract class ASTPool2
        {
            public int SubType;
            public AST ParentAst;

            public List<int> PreDataNumbers;
            public List<int> PreDataSubtypes;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            //public byte[] TEMP_DATA;

            public static ASTPool2 Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long startOffset = br.Position;

                int subType = br.ReadInt32();
                int size = br.ReadInt32();
                long preDataCount = br.ReadFXR1Varint();
                long offsetToPreDataNumbers = br.ReadFXR1Varint();
                long offsetToPreDataSubtypes = br.ReadFXR1Varint();

                long offsetToParentAst = br.ReadFXR1Varint();
                //var parentAst = env.GetAST(br, offsetToParentAst);

                ASTPool2 data;

                switch (subType)
                {
                    case 27: data = new AstPool2Type27(); break;
                    case 28: data = new AstPool2Type28(); break;
                    case 29: data = new AstPool2Type29(); break;
                    case 30: data = new AstPool2Type30(); break;
                    case 31: data = new AstPool2Type31(); break;
                    case 32: data = new AstPool2Type32(); break;
                    case 40: data = new AstPool2Type40(); break;
                    case 43: data = new AstPool2Type43(); break;
                    case 55: data = new AstPool2Type55(); break;
                    case 59: data = new AstPool2Type59(); break;
                    case 61: data = new AstPool2Type61(); break;
                    case 66: data = new AstPool2Type66(); break;
                    case 70: data = new AstPool2Type70(); break;
                    case 71: data = new AstPool2Type71(); break;
                    case 84: data = new AstPool2Type84(); break;
                    case 105: data = new AstPool2Type105(); break;
                    case 107: data = new AstPool2Type107(); break;
                    case 108: data = new AstPool2Type108(); break;
                    case 117: data = new AstPool2Type117(); break;
                    default: throw new NotImplementedException();
                }

                //TEMPORARY
                data.InnerRead(br, env);

                data.SubType = subType;

                //data.TEMP_DATA = br.GetBytes(startOffset, size);

                //data.ParentAst = parentAst;

                //pre data nubmers go here during write
                data.PreDataNumbers = new List<int>();
                br.StepIn(offsetToPreDataNumbers);
                for (int i = 0; i < preDataCount; i++)
                {
                    data.PreDataNumbers.Add(br.ReadInt32());
                }
                br.StepOut();

                //pre data subtypes go here during write
                data.PreDataSubtypes = new List<int>();
                br.StepIn(offsetToPreDataNumbers);
                for (int i = 0; i < preDataCount; i++)
                {
                    data.PreDataSubtypes.Add(br.ReadInt32());
                }
                br.StepOut();

                //the packed shit from switch(subType) all goes here during write?

                br.Position = startOffset + size;

                return data;
            }
        }







        public class AstPool2Type27 : ASTPool2
        {
            public float Unk1;
            public float Unk2;
            public float Unk3;
            public float Unk4;
            public int TextureID;
            public int Unk6;
            public ASTPool2Field[] Unk7;
            public int Unk8;
            public int Unk9;
            public int Unk10;
            public float Unk11;
            public ASTPool2Field[] DS1R_Unk5;
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
                Unk7 = ASTPool2Field.ReadMany(br, env, 10);
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();
                Unk11 = br.ReadSingle();
                if (br.VarintLong)
                {
                    DS1R_Unk5 = ASTPool2Field.ReadMany(br, env, 5);
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



        public class AstPool2Type28 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public int Unk2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 3);
                Unk2 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }


        public class AstPool2Type29 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public int Unk2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 5);
                Unk2 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type30 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public float Unk2;
            public int Unk3;
            public int Unk4;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 4);
                Unk2 = br.ReadSingle();
                Unk3 = br.ReadInt32();
                Unk4 = br.ReadFXR1Varint();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }


        public class AstPool2Type31 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public int Unk2;
            public int Unk3;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 4);
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type32 : ASTPool2
        {
            public ASTPool2Field OffsetX;
            public ASTPool2Field OffsetY;
            public ASTPool2Field OffsetZ;
            public ASTPool2Field[] Unk1;
            public int Unk2;
            public int Unk3;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                OffsetX = ASTPool2Field.Read(br, env);
                OffsetY = ASTPool2Field.Read(br, env);
                OffsetZ = ASTPool2Field.Read(br, env);
                Unk1 = ASTPool2Field.ReadMany(br, env, 3);
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type40 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk3;
            public int Unk4;
            public int Unk5;
            public ASTPool2Field[] Unk6;
            public float Unk7;
            public float Unk8;
            public int Unk9;
            public int Unk10;
            public ASTPool2Field[] Unk11;
            public int Unk12;
            public int Unk13;
            public ASTPool2Field Unk14;
            public int Unk15;
            public float Unk16;
            public ASTPool2Field[] Unk17;
            public int Unk18;

            public ASTPool2Field[] DS1R_Unk1;
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
                Unk6 = ASTPool2Field.ReadMany(br, env, 4);
                Unk7 = br.ReadSingle();
                Unk8 = br.ReadSingle();
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();

                br.AssertInt32(0);

                Unk11 = ASTPool2Field.ReadMany(br, env, 4);
                Unk12 = br.ReadInt32();
                Unk13 = br.ReadInt32();

                br.AssertInt32(0);

                Unk14 = ASTPool2Field.Read(br, env);
                Unk15 = br.ReadInt32();
                Unk16 = br.ReadSingle();
                Unk17 = ASTPool2Field.ReadMany(br, env, 2);
                Unk18 = br.ReadInt32();

                if (br.VarintLong)
                {
                    DS1R_Unk1 = ASTPool2Field.ReadMany(br, env, 5);
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

        public class AstPool2Type43 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk2;
            public int Unk3;
            public int Unk4;
            public int Unk5;
            public int Unk6;
            public ASTPool2Field[] Unk7;
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
                Unk7 = ASTPool2Field.ReadMany(br, env, 13);
                Unk8 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type55 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public float Unk2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 3);
                br.AssertInt32(0);
                Unk2 = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type59 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk2;
            public int Unk3;
            public ASTPool2Field[] Unk4;
            public int Unk5;
            public int Unk6;
            public ASTPool2Field[] Unk7;
            public int Unk8;
            public int Unk9;
            public int Unk10;
            public float Unk11;

            public ASTPool2Field[] DS1R_Unk1;
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
                Unk4 = ASTPool2Field.ReadMany(br, env, 5);
                Unk5 = br.ReadInt32();
                Unk6 = br.ReadInt32();
                Unk7 = ASTPool2Field.ReadMany(br, env, 8);
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();

                br.AssertInt32(0);

                Unk10 = br.ReadInt32();
                Unk11 = br.ReadSingle();

                br.AssertInt32(0);

                if (br.VarintLong)
                {
                    DS1R_Unk1 = ASTPool2Field.ReadMany(br, env, 5);
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

        public class AstPool2Type61 : ASTPool2
        {
            public int TextureID;
            public int Unk1;
            public int Unk2;
            public int Unk3;
            public ASTPool2Field[] Unk4;
            public int Unk5;
            public float Unk6;
            public ASTPool2Field Unk7;
            public int Unk8;
            public int Unk9;

            public ASTPool2Field[] DS1R_Unk1;
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

                Unk4 = ASTPool2Field.ReadMany(br, env, 3);

                br.AssertInt32(0);
                br.AssertInt32(0);

                Unk5 = br.ReadInt32();
                Unk6 = br.ReadSingle();

                br.AssertInt32(0);

                Unk7 = ASTPool2Field.Read(br, env);
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();

                br.AssertInt32(0);
                br.AssertInt32(0);

                if (br.VarintLong)
                {
                    DS1R_Unk1 = ASTPool2Field.ReadMany(br, env, 5);
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


        public class AstPool2Type66 : ASTPool2
        {
            public float Unk1;
            public float Unk2;
            public int Unk3;
            public float Unk4;
            public int Unk5;

            public int DS1R_Unk0;

            public ASTPool2Field[] Unk6;

            public ASTPool2Field[] DS1R_Unk1;
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

                Unk6 = ASTPool2Field.ReadMany(br, env, 26);

                if (br.VarintLong)
                {
                    br.AssertInt32(0);
                    DS1R_Unk1 = ASTPool2Field.ReadMany(br, env, 5);
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

        public class AstPool2Type70 : ASTPool2
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
            public ASTPool2Field[] Unk9;
            public int Unk10;
            public int Unk11;
            public int Unk12;
            public int Unk13;
            public float Unk14;
            public int Unk15;
            public float Unk16;
            public int Unk17;

            public ASTPool2Field[] DS1R_Unk1;
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
                Unk9 = ASTPool2Field.ReadMany(br, env, 30);
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
                    DS1R_Unk1 = ASTPool2Field.ReadMany(br, env, 5);
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

        public class AstPool2Type71 : ASTPool2
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
            public ASTPool2Field[] Unk11;
            public int Unk12;
            public int Unk13;
            public ASTPool2Field[] Unk14;

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

            public ASTPool2Field[] DS1R_Unk1;
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
                Unk11 = ASTPool2Field.ReadMany(br, env, 10);
                Unk12 = br.ReadInt32();
                Unk13 = br.ReadInt32();
                Unk14 = ASTPool2Field.ReadMany(br, env, 10);

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
                    DS1R_Unk1 = ASTPool2Field.ReadMany(br, env, 5);
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

        public class AstPool2Type84 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public float Unk2;
            public ASTPool2Field Unk3;
            public int Unk4;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 3);
                br.AssertInt32(0);
                Unk2 = br.ReadSingle();
                Unk3 = ASTPool2Field.Read(br, env);
                Unk4 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type105 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public float Unk2;
            public ASTPool2Field Unk3;
            public int Unk4;
            public ASTPool2Field Unk5;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 3);
                br.AssertInt32(0);
                Unk2 = br.ReadSingle();
                Unk3 = ASTPool2Field.Read(br, env);
                Unk4 = br.ReadFXR1Varint();
                Unk5 = ASTPool2Field.Read(br, env);
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type107 : ASTPool2
        {
            public float Unk1;
            public int TextureID;
            public int Unk2;
            public ASTPool2Field[] Unk3;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = br.ReadSingle();
                br.AssertInt32(0);
                TextureID = br.ReadInt32();
                Unk2 = br.ReadInt32();
                Unk3 = ASTPool2Field.ReadMany(br, env, 7);
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2Type108 : ASTPool2
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
            public ASTPool2Field Scale1X;
            public ASTPool2Field Scale1Y;
            public ASTPool2Field Scale1Z;
            public ASTPool2Field Scale2X;
            public ASTPool2Field Scale2Y;
            public ASTPool2Field Scale2Z;
            public ASTPool2Field RotSpeedX;
            public ASTPool2Field RotSpeedY;
            public ASTPool2Field RotSpeedZ;
            public ASTPool2Field Rot1;
            public ASTPool2Field Rot2;
            public ASTPool2Field Rot3;
            public int Unk9;
            public int Unk10;
            public ASTPool2Field[] Unk11;
            public ASTPool2Field Color1R;
            public ASTPool2Field Color1G;
            public ASTPool2Field Color1B;
            public ASTPool2Field Color1A;
            public ASTPool2Field Color2R;
            public ASTPool2Field Color2G;
            public ASTPool2Field Color2B;
            public ASTPool2Field Color2A;
            public int Unk12;
            public int Unk13;
            public int Unk14;
            public float Unk15;
            public int Unk16;

            public ASTPool2Field[] DS1R_Unk1;
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

                Scale1X = ASTPool2Field.Read(br, env);
                Scale1Y = ASTPool2Field.Read(br, env);
                Scale1Z = ASTPool2Field.Read(br, env);
                Scale2X = ASTPool2Field.Read(br, env);
                Scale2Y = ASTPool2Field.Read(br, env);
                Scale2Z = ASTPool2Field.Read(br, env);
                RotSpeedX = ASTPool2Field.Read(br, env);
                RotSpeedY = ASTPool2Field.Read(br, env);
                RotSpeedZ = ASTPool2Field.Read(br, env);
                Rot1 = ASTPool2Field.Read(br, env);
                Rot2 = ASTPool2Field.Read(br, env);
                Rot3 = ASTPool2Field.Read(br, env);
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();
                Unk11 = ASTPool2Field.ReadMany(br, env, 6);
                Color1R = ASTPool2Field.Read(br, env);
                Color1G = ASTPool2Field.Read(br, env);
                Color1B = ASTPool2Field.Read(br, env);
                Color1A = ASTPool2Field.Read(br, env);
                Color2R = ASTPool2Field.Read(br, env);
                Color2G = ASTPool2Field.Read(br, env);
                Color2B = ASTPool2Field.Read(br, env);
                Color2A = ASTPool2Field.Read(br, env);
                Unk12 = br.ReadInt32();
                Unk13 = br.ReadInt32();
                Unk14 = br.ReadInt32();
                Unk15 = br.ReadSingle();
                Unk16 = br.ReadInt32();

                if (br.VarintLong)
                {
                    DS1R_Unk1 = ASTPool2Field.ReadMany(br, env, 5);
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

        public class AstPool2Type117 : ASTPool2
        {
            public ASTPool2Field[] Unk1;
            public int Unk2;
            public int Unk3;
            public ASTPool2Field Unk4;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = ASTPool2Field.ReadMany(br, env, 6);
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
                Unk4 = ASTPool2Field.Read(br, env);
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

    }
}
