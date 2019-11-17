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
        [XmlInclude(typeof(FXTemplate0))]
        [XmlInclude(typeof(FXTemplate1))]
        [XmlInclude(typeof(FXTemplate2))]
        [XmlInclude(typeof(FXTemplate3))]
        [XmlInclude(typeof(FXTemplate4))]
        [XmlInclude(typeof(FXTemplate5))]
        [XmlInclude(typeof(FXTemplate6))]
        [XmlInclude(typeof(FXTemplate7))]
        [XmlInclude(typeof(FXTemplateRef))]
        public abstract class FXTemplate : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenTemplates;

            [XmlIgnore]
            public abstract int Type { get; }

            public virtual bool ShouldSerializeCommandType() => true;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            internal override void ToXIDs(FXR1 fxr)
            {
                InnerToXIDs(fxr);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                InnerFromXIDs(fxr);
            }

            internal virtual void InnerToXIDs(FXR1 fxr)
            {

            }

            internal virtual void InnerFromXIDs(FXR1 fxr)
            {

            }

            public static FXTemplate GetProperType(BinaryReaderEx br, FxrEnvironment env)
            {
                int commandType = br.GetInt32(br.Position);
                FXTemplate data = null;
                switch (commandType)
                {
                    case 0: data = new FXTemplate0(); break;
                    case 1: data = new FXTemplate1(); break;
                    case 2: data = new FXTemplate2(); break;
                    case 3: data = new FXTemplate3(); break;
                    case 4: data = new FXTemplate4(); break;
                    case 5: data = new FXTemplate5(); break;
                    case 6: data = new FXTemplate6(); break;
                    case 7: data = new FXTemplate7(); break;
                }
                return data;
            }

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                InnerRead(br, env);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                InnerWrite(bw, env);
            }
        }


        public class FXTemplateRef : FXTemplate
        {
            public override int Type => -1;

            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeCommandType() => false;

            public FXTemplateRef(FXTemplate refVal)
            {
                ReferenceXID = refVal?.XID;
            }

            public FXTemplateRef()
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


        public class FXTemplate0 : FXTemplate
        {
            public override int Type => 0;

            public int Unk;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(0);

                Unk = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(0);

                bw.WriteInt32(Unk);
            }
        }

        public class FXTemplate1 : FXTemplate
        {
            public override int Type => 1;

            public int Unk1;
            public int Unk2;
            public int Unk3;
            public float OffsetX;
            public float OffsetY;
            public float Unk6;
            public int Unk7;
            public float Unk8;
            public float Unk9;
            public float Unk10;
            public float Unk11;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(1);

                Unk1 = br.ReadInt32();
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadInt32();
                OffsetX = br.ReadSingle();
                OffsetY = br.ReadSingle();
                Unk6 = br.ReadSingle();
                Unk7 = br.ReadInt32();
                Unk8 = br.ReadSingle();
                Unk9 = br.ReadSingle();
                Unk10 = br.ReadSingle();
                Unk11 = br.ReadSingle();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(1);

                bw.WriteInt32(Unk1);
                bw.WriteInt32(Unk2);
                bw.WriteInt32(Unk3);
                bw.WriteSingle(OffsetX);
                bw.WriteSingle(OffsetY);
                bw.WriteSingle(Unk6);
                bw.WriteInt32(Unk7);
                bw.WriteSingle(Unk8);
                bw.WriteSingle(Unk9);
                bw.WriteSingle(Unk10);
                bw.WriteSingle(Unk11);
            }
        }



        public class FXTemplate2 : FXTemplate
        {
            public override int Type => 2;

            public float Lifetime;
            public float Unk2;
            public int Unk3;
            public float Unk4;
            public byte Unk5;
            public byte Unk6;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(2);

                Lifetime = br.ReadSingle();
                Unk2 = br.ReadSingle();
                Unk3 = br.ReadInt32();
                Unk4 = br.ReadSingle();
                Unk5 = br.ReadByte();
                Unk6 = br.ReadByte();

                br.AssertInt16(0);
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(2);

                bw.WriteSingle(Lifetime);
                bw.WriteSingle(Unk2);
                bw.WriteInt32(Unk3);
                bw.WriteSingle(Unk4);
                bw.WriteByte(Unk5);
                bw.WriteByte(Unk6);

                bw.WriteInt16(0);
            }
        }

        public class FXTemplate3 : FXTemplate
        {
            public override int Type => 3;

            public float Unk1;
            public int Unk2;
            public float Unk3;
            public int Unk4;

            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(3);

                Unk1 = br.ReadSingle();
                Unk2 = br.ReadInt32();
                Unk3 = br.ReadSingle();
                Unk4 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(3);

                bw.WriteSingle(Unk1);
                bw.WriteInt32(Unk2);
                bw.WriteSingle(Unk3);
                bw.WriteInt32(Unk4);
            }
        }

        public class FXTemplate4 : FXTemplate
        {
            public override int Type => 4;

            public int Unk;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(4);

                Unk = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(4);

                bw.WriteInt32(Unk);
            }
        }

        public class FXTemplate5 : FXTemplate
        {
            public override int Type => 5;

            public int Unk;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(5);

                Unk = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(5);

                bw.WriteInt32(Unk);
            }
        }

        public class FXTemplate6 : FXTemplate
        {
            public override int Type => 6;

            public float Unk1;
            public int Unk2;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(6);

                Unk1 = br.ReadSingle();
                Unk2 = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(6);

                bw.WriteSingle(Unk1);
                bw.WriteInt32(Unk2);
            }
        }

        public class FXTemplate7 : FXTemplate
        {
            public override int Type => 7;

            public int Unk;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(7);

                Unk = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(7);

                bw.WriteInt32(Unk);
            }
        }


    }
}
