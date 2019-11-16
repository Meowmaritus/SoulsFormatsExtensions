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
        public struct FloatTick
        {
            [XmlAttribute]
            public float Time;
            [XmlAttribute]
            public float Value;
        }

        public struct Vector3Tick
        {
            [XmlAttribute]
            public float Time;
            [XmlAttribute]
            public float X;
            [XmlAttribute]
            public float Y;
            [XmlAttribute]
            public float Z;
        }

        [XmlInclude(typeof(ConstFloatSequence))]
        [XmlInclude(typeof(FloatSequence))]
        [XmlInclude(typeof(RangedFloatSequence))]
        [XmlInclude(typeof(FloatSequenceEx))]
        [XmlInclude(typeof(RangedFloatSequenceEx))]
        [XmlInclude(typeof(Vector3Sequence))]
        [XmlInclude(typeof(RangedVector3Sequence))]
        [XmlInclude(typeof(RepeatingConstFloatSequence))]
        [XmlInclude(typeof(RepeatingFloatSequence))]
        [XmlInclude(typeof(RangedFloatSequenceB))]
        [XmlInclude(typeof(NewVector3Sequence))]
        [XmlInclude(typeof(ConstInt))]
        [XmlInclude(typeof(ParamType25))]
        [XmlInclude(typeof(ParamType26))]
        [XmlInclude(typeof(ParamType27))]
        [XmlInclude(typeof(Empty))]
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
                    case 0: v = new ConstFloatSequence(); break; //Constant
                    case 4: v = new FloatSequence(); break;
                    case 5: v = new RangedFloatSequence(); break;
                    case 6: v = new FloatSequenceEx(); break;
                    case 7: v = new RangedFloatSequenceEx(); break;
                    case 8: v = new Vector3Sequence(); break;
                    case 9: v = new RangedVector3Sequence(); break;
                    case 12: v = new RepeatingConstFloatSequence(); break; //Constant, repeats
                    case 16: v = new RepeatingFloatSequence(); break; //Repeats
                    case 17: v = new RangedFloatSequenceB(); break;
                    case 20: v = new NewVector3Sequence(); break; //DS1R Only
                    case 24: v = new ConstInt(); break;
                    case 25: v = new ParamType25(); break;
                    case 26: v = new ParamType26(); break;
                    case 27: v = new ParamType27(); break;
                    case 28: v = new Empty(); break;
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


            public class ConstFloatSequence : Param
            {
                public override int Type => 0;

                public FloatTick[] Values;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class FloatSequence : Param
            {
                public override int Type => 4;

                public FloatTick[] Values;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class RangedFloatSequence : Param
            {
                public override int Type => 5;

                public FloatTick[] Values;
                [XmlAttribute]
                public float Min;
                [XmlAttribute]
                public float Max;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class FloatSequenceEx : Param
            {
                public override int Type => 6;

                public FloatTick[] Values;
                [XmlAttribute]
                public int PreDataIndex;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class RangedFloatSequenceEx : Param
            {
                public override int Type => 7;

                public FloatTick[] Values;
                [XmlAttribute]
                public float Min;
                [XmlAttribute]
                public float Max;
                [XmlAttribute]
                public int PreDataIndex;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class Vector3Sequence : Param
            {
                public override int Type => 8;

                public Vector3Tick[] Values;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new Vector3Tick[listSize];
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

            public class RangedVector3Sequence : Param
            {
                public override int Type => 9;

                public Vector3Tick[] Values;
                [XmlAttribute]
                public float Min;
                [XmlAttribute]
                public float Max;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new Vector3Tick[listSize];
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

            public class RepeatingConstFloatSequence : Param
            {
                public override int Type => 12;

                public FloatTick[] Values;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class RepeatingFloatSequence : Param
            {
                public override int Type => 16;

                public FloatTick[] Values;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class RangedFloatSequenceB : Param
            {
                public override int Type => 17;

                public FloatTick[] Values;
                [XmlAttribute]
                public float Min;
                [XmlAttribute]
                public float Max;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new FloatTick[listSize];
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

            public class NewVector3Sequence : Param
            {
                public override int Type => 20;

                public Vector3Tick[] Values;

                public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
                {
                    int listSize = br.ReadInt32();
                    Values = new Vector3Tick[listSize];
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

            public class ConstInt : Param
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

            public class Empty : Param
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
}
