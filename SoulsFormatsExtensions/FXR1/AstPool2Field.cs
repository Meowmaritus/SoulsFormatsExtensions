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
        public class ASTPool2Field
        {
            public long Type;

            public class AstPool2FieldType4 : ASTPool2Field
            {
                public (float Time, float Value)[] Values;
                public AstPool2FieldType4(BinaryReaderEx br, FxrEnvironment env)
                {
                    Type = 4;
                    int listSize = br.ReadInt32();
                    Values = new (float Time, float Value)[listSize];
                    for (int i = 0; i < listSize; i++)
                        Values[i].Time = br.ReadSingle();
                    for (int i = 0; i < listSize; i++)
                        Values[i].Value = br.ReadSingle();
                }
            }

            public class AstPool2FieldType5 : ASTPool2Field
            {
                public (float Time, float Value)[] Values;
                public float Min;
                public float Max;
                public AstPool2FieldType5(BinaryReaderEx br, FxrEnvironment env)
                {
                    Type = 5;
                    int listSize = br.ReadInt32();
                    Values = new (float Time, float Value)[listSize];
                    for (int i = 0; i < listSize; i++)
                        Values[i].Time = br.ReadSingle();
                    for (int i = 0; i < listSize; i++)
                        Values[i].Value = br.ReadSingle();
                    Min = br.ReadSingle();
                    Max = br.ReadSingle();
                }
            }

            public class AstPool2FieldType24 : ASTPool2Field
            {
                public float Value;
                public AstPool2FieldType24(BinaryReaderEx br, FxrEnvironment env)
                {
                    Type = 24;
                    Value = br.ReadSingle();
                }
            }

            public class AstPool2FieldType25 : ASTPool2Field
            {
                public float Base;
                public float Unknown1;
                public float Unknown2;
                public AstPool2FieldType25(BinaryReaderEx br, FxrEnvironment env)
                {
                    Type = 25;
                    Base = br.ReadSingle();
                    Unknown1 = br.ReadSingle();
                    Unknown2 = br.ReadSingle();
                }
            }

            public class AstPool2FieldType28 : ASTPool2Field
            {
                public AstPool2FieldType28(BinaryReaderEx br, FxrEnvironment env)
                {
                    Type = 28;
                }
            }

            public static ASTPool2Field Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long type = br.ReadFXR1Varint();
                long offset = br.ReadFXR1Varint();

                ASTPool2Field v = null;

                br.StepIn(offset);
                switch (type)
                {
                    case 4: v = new AstPool2FieldType4(br, env); break;
                    case 5: v = new AstPool2FieldType5(br, env); break;
                    case 24: v = new AstPool2FieldType24(br, env); break;
                    case 25: v = new AstPool2FieldType25(br, env); break;
                    case 28: v = new AstPool2FieldType28(br, env); break;
                    default: throw new NotImplementedException();
                }
                br.StepOut();

                return v;
            }

            public static ASTPool2Field[] ReadMany(BinaryReaderEx br, FxrEnvironment env, int count)
            {
                ASTPool2Field[] list = new ASTPool2Field[count];
                for (int i = 0; i < count; i++)
                    list[i] = Read(br, env);
                return list;
            }
        }
    }
}
