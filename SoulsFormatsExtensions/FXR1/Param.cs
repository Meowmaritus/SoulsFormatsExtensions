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
        [XmlInclude(typeof(ParamType0))]
        [XmlInclude(typeof(ParamType4))]
        [XmlInclude(typeof(ParamType5))]
        [XmlInclude(typeof(ParamType6))]
        [XmlInclude(typeof(ParamType7))]
        [XmlInclude(typeof(ParamType8))]
        [XmlInclude(typeof(ParamType9))]
        [XmlInclude(typeof(ParamType12))]
        [XmlInclude(typeof(ParamType16))]
        [XmlInclude(typeof(ParamType17))]
        [XmlInclude(typeof(ParamType20))]
        [XmlInclude(typeof(ParamType24))]
        [XmlInclude(typeof(ParamType25))]
        [XmlInclude(typeof(ParamType26))]
        [XmlInclude(typeof(ParamType27))]
        [XmlInclude(typeof(ParamType28))]
        public abstract class Param
        {
            [XmlIgnore]
            public abstract int Type { get; }

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            internal virtual bool IsEmptyPointer() => false;

            public static Param Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int type = br.ReadFXR1Varint();
                int offset = br.ReadFXR1Varint();

                Param v = null;

                
                switch (type)
                {
                    case 0: v = new ParamType0(); break; //Constant
                    case 4: v = new ParamType4(); break;
                    case 5: v = new ParamType5(); break;
                    case 6: v = new ParamType6(); break;
                    case 7: v = new ParamType7(); break;
                    case 8: v = new ParamType8(); break;
                    case 9: v = new ParamType9(); break;
                    case 12: v = new ParamType12(); break; //Constant, repeats
                    case 16: v = new ParamType16(); break; //Repeats
                    case 17: v = new ParamType17(); break;
                    case 20: v = new ParamType20(); break; //DS1R Only
                    case 24: v = new ParamType24(); break;
                    case 25: v = new ParamType25(); break;
                    case 26: v = new ParamType26(); break;
                    case 27: v = new ParamType27(); break;
                    case 28: v = new ParamType28(); break;
                    default: throw new NotImplementedException();
                }

                br.StepIn(offset);
                v.InnerRead(br, env);
                br.StepOut();

                env.Debug_RegisterReadParam(v);

                return v;
            }

            public static Param[] ReadMany(BinaryReaderEx br, FxrEnvironment env, int count)
            {
                Param[] list = new Param[count];
                for (int i = 0; i < count; i++)
                    list[i] = Read(br, env);
                return list;
            }

            

            
        }

        public class ParamType0 : Param
        {
            public override int Type => 0;

            public Curve1[] Values;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
            }
        }

        public class ParamType4 : Param
        {
            public override int Type => 4;

            public Curve1[] Values;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
            }
        }

        public class ParamType5 : Param
        {
            public override int Type => 5;

            public Curve1[] Values;
            [XmlAttribute]
            public float Min;
            [XmlAttribute]
            public float Max;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
                Min = br.ReadSingle();
                Max = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
                bw.WriteSingle(Min);
                bw.WriteSingle(Max);
            }
        }

        public class ParamType6 : Param
        {
            public override int Type => 6;

            public Curve1[] Values;
            [XmlAttribute]
            public int PreDataIndex;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
                PreDataIndex = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
                bw.WriteInt32(PreDataIndex);
            }
        }

        public class ParamType7 : Param
        {
            public override int Type => 7;

            public Curve1[] Values;
            [XmlAttribute]
            public float Min;
            [XmlAttribute]
            public float Max;
            [XmlAttribute]
            public int PreDataIndex;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
                Min = br.ReadSingle();
                Max = br.ReadSingle();
                PreDataIndex = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
                bw.WriteSingle(Min);
                bw.WriteSingle(Max);
                bw.WriteInt32(PreDataIndex);
            }
        }

        public class ParamType8 : Param
        {
            public override int Type => 8;

            public Curve3[] Values;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve3[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].X = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Y = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Z = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].X);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Y);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Z);
            }
        }

        public class ParamType9 : Param
        {
            public override int Type => 9;

            public Curve3[] Values;
            [XmlAttribute]
            public float Min;
            [XmlAttribute]
            public float Max;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve3[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].X = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Y = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Z = br.ReadSingle();
                Min = br.ReadSingle();
                Max = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].X);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Y);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Z);
                bw.WriteSingle(Min);
                bw.WriteSingle(Max);
            }
        }

        public class ParamType12 : Param
        {
            public override int Type => 12;

            public Curve1[] Values;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
            }
        }

        public class ParamType16 : Param
        {
            public override int Type => 16;

            public Curve1[] Values;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
            }
        }

        public class ParamType17 : Param
        {
            public override int Type => 17;

            public Curve1[] Values;
            [XmlAttribute]
            public float Min;
            [XmlAttribute]
            public float Max;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve1[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Value = br.ReadSingle();
                Min = br.ReadSingle();
                Max = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Value);
                bw.WriteSingle(Min);
                bw.WriteSingle(Max);
            }
        }

        public class ParamType20 : Param
        {
            public override int Type => 20;

            public Curve3[] Values;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                int listSize = br.ReadInt32();
                Values = new Curve3[listSize];
                for (int i = 0; i < listSize; i++)
                    Values[i].Time = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].X = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Y = br.ReadSingle();
                for (int i = 0; i < listSize; i++)
                    Values[i].Z = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(Values.Length);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Time);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].X);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Y);
                for (int i = 0; i < Values.Length; i++)
                    bw.WriteSingle(Values[i].Z);
            }
        }

        public class ParamType24 : Param
        {
            public override int Type => 24;

            [XmlAttribute]
            public float Value;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Value = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteSingle(Value);
            }
        }

        public class ParamType25 : Param
        {
            public override int Type => 25;

            [XmlAttribute]
            public float Base;
            [XmlAttribute]
            public float Min;
            [XmlAttribute]
            public float Max;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Base = br.ReadSingle();
                Min = br.ReadSingle();
                Max = br.ReadSingle();
            }
            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteSingle(Base);
                bw.WriteSingle(Min);
                bw.WriteSingle(Max);
            }
        }

        public class ParamType26 : Param
        {
            public override int Type => 26;

            [XmlAttribute]
            public float Unk1;
            [XmlAttribute]
            public int Unk2;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = br.ReadSingle();
                Unk2 = br.ReadInt32();
            }
            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteSingle(Unk1);
                bw.WriteInt32(Unk2);
            }
        }

        public class ParamType27 : Param
        {
            public override int Type => 27;

            [XmlAttribute]
            public float Unk1;
            [XmlAttribute]
            public float Unk2;
            [XmlAttribute]
            public float Unk3;
            [XmlAttribute]
            public int PreDataIndex;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = br.ReadSingle();
                Unk2 = br.ReadSingle();
                Unk3 = br.ReadSingle();
                PreDataIndex = br.ReadInt32();
            }
            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteSingle(Unk1);
                bw.WriteSingle(Unk2);
                bw.WriteSingle(Unk3);
                bw.WriteInt32(PreDataIndex);
            }
        }

        public class ParamType28 : Param
        {
            public override int Type => 28;

            internal override bool IsEmptyPointer() => true;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {

            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                
            }
        }
    }
}
