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
        [XmlInclude(typeof(AstPool2FieldType4))]
        [XmlInclude(typeof(AstPool2FieldType5))]
        [XmlInclude(typeof(AstPool2FieldType24))]
        [XmlInclude(typeof(AstPool2FieldType25))]
        [XmlInclude(typeof(AstPool2FieldType28))]
        public abstract class ASTPool2Field
        {
            public int Type;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            public static ASTPool2Field Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int type = br.ReadFXR1Varint();
                int offset = br.ReadFXR1Varint();

                ASTPool2Field v = null;

                
                switch (type)
                {
                    case 4: v = new AstPool2FieldType4(); break;
                    case 5: v = new AstPool2FieldType5(); break;
                    case 24: v = new AstPool2FieldType24(); break;
                    case 25: v = new AstPool2FieldType25(); break;
                    case 28: v = new AstPool2FieldType28(); break;
                    default: throw new NotImplementedException();
                }

                v.Type = type;

                br.StepIn(offset);
                v.InnerRead(br, env);
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

        public class AstPool2FieldType4 : ASTPool2Field
        {
            public (float Time, float Value)[] Values;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new (float Time, float Value)[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2FieldType5 : ASTPool2Field
        {
            public (float Time, float Value)[] Values;
            public float Min;
            public float Max;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new (float Time, float Value)[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
                Min = br.ReadSingle();
                Max = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2FieldType24 : ASTPool2Field
        {
            public float Value;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Value = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2FieldType25 : ASTPool2Field
        {
            public float Base;
            public float Unknown1;
            public float Unknown2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Base = br.ReadSingle();
                Unknown1 = br.ReadSingle();
                Unknown2 = br.ReadSingle();
            }
            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }

        public class AstPool2FieldType28 : ASTPool2Field
        {
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {

            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                throw new NotImplementedException();
            }
        }
    }
}
