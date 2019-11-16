﻿using SoulsFormats;
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
        [XmlInclude(typeof(TemplateType0))]
        [XmlInclude(typeof(TemplateType1))]
        [XmlInclude(typeof(TemplateType2))]
        [XmlInclude(typeof(TemplateType3))]
        [XmlInclude(typeof(TemplateType4))]
        [XmlInclude(typeof(TemplateType5))]
        [XmlInclude(typeof(TemplateType6))]
        [XmlInclude(typeof(TemplateType7))]
        [XmlInclude(typeof(TemplateRef))]
        public abstract class Template : XIDable
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

            public static Template GetProperType(BinaryReaderEx br, FxrEnvironment env)
            {
                int commandType = br.GetInt32(br.Position);
                Template data = null;
                switch (commandType)
                {
                    case 0: data = new TemplateType0(); break;
                    case 1: data = new TemplateType1(); break;
                    case 2: data = new TemplateType2(); break;
                    case 3: data = new TemplateType3(); break;
                    case 4: data = new TemplateType4(); break;
                    case 5: data = new TemplateType5(); break;
                    case 6: data = new TemplateType6(); break;
                    case 7: data = new TemplateType7(); break;
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


        public class TemplateRef : Template
        {
            public override int Type => -1;

            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeCommandType() => false;

            public TemplateRef(Template refVal)
            {
                ReferenceXID = refVal?.XID;
            }

            public TemplateRef()
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


        public class TemplateType0 : Template
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

        public class TemplateType1 : Template
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



        public class TemplateType2 : Template
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

        public class TemplateType3 : Template
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

        public class TemplateType4 : Template
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

        public class TemplateType5 : Template
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

        public class TemplateType6 : Template
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

        public class TemplateType7 : Template
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