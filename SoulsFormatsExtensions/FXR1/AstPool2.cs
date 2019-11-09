using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
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
                    case 27: data = new AstPool2Type27(br, env); break;
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






    }
}
