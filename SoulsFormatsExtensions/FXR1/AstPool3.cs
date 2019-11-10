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
        [XmlInclude(typeof(ASTPool3Type0))]
        [XmlInclude(typeof(ASTPool3Type1))]
        [XmlInclude(typeof(ASTPool3Type2))]
        [XmlInclude(typeof(ASTPool3Type3))]
        [XmlInclude(typeof(ASTPool3Type4))]
        [XmlInclude(typeof(ASTPool3Type5))]
        [XmlInclude(typeof(ASTPool3Type6))]
        [XmlInclude(typeof(ASTPool3Type7))]
        public abstract class ASTPool3
        {
            public int CommandType;

            public abstract void InnerRead(BinaryReaderEx br, FxrEnvironment env);
            public abstract void InnerWrite(BinaryWriterEx bw, FxrEnvironment env);

            public static ASTPool3 Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int commandType = br.GetInt32(br.Position);
                ASTPool3 data = null;
                switch (commandType)
                {
                    case 0: data = new ASTPool3Type0(); break;
                    case 1: data = new ASTPool3Type1(); break;
                    case 2: data = new ASTPool3Type2(); break;
                    case 3: data = new ASTPool3Type3(); break;
                    case 4: data = new ASTPool3Type4(); break;
                    case 5: data = new ASTPool3Type5(); break;
                    case 6: data = new ASTPool3Type6(); break;
                    case 7: data = new ASTPool3Type7(); break;
                }
                data.CommandType = commandType;
                data.InnerRead(br, env);
                return data;
            }
        }



        public class ASTPool3Type0 : ASTPool3
        {
            public int Unknown;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(0);

                Unknown = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(0);

                bw.WriteInt32(Unknown);
            }
        }

        public class ASTPool3Type1 : ASTPool3
        {
            public int Unk1;
            public int Unk2;
            public int Unk3;
            public float OffsetX;
            public float OffsetY;
            public float Unk6;
            public int Unk7;
            public int Unk8;
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
                Unk8 = br.ReadInt32();
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
                bw.WriteInt32(Unk8);
                bw.WriteSingle(Unk9);
                bw.WriteSingle(Unk10);
                bw.WriteSingle(Unk11);
            }
        }



        public class ASTPool3Type2 : ASTPool3
        {
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

        public class ASTPool3Type3 : ASTPool3
        {
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

        public class ASTPool3Type4 : ASTPool3
        {
            public int Unknown;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(4);

                Unknown = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(4);

                bw.WriteInt32(Unknown);
            }
        }

        public class ASTPool3Type5 : ASTPool3
        {
            public int Unknown;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(5);

                Unknown = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(5);

                bw.WriteInt32(Unknown);
            }
        }

        public class ASTPool3Type6 : ASTPool3
        {
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

        public class ASTPool3Type7 : ASTPool3
        {
            public int Unknown;
            public override void InnerRead(BinaryReaderEx br, FxrEnvironment env)
            {
                br.AssertInt32(7);

                Unknown = br.ReadInt32();
            }

            public override void InnerWrite(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteInt32(7);

                bw.WriteInt32(Unknown);
            }
        }


    }
}
