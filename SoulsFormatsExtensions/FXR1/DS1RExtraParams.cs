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
        public class DS1RExtraNodes
        {
            public FXField Unk1;
            public FXField Unk2;
            public FXField Unk3;
            public FXField Unk4;
            public FXField Unk5;
            public float Unk6;
            public int Unk7;
            public int Unk8;
            public int Unk9;
            public int Unk10;
            public int Unk11;

            private void ReadInner(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = FXField.Read(br, env);
                Unk2 = FXField.Read(br, env);
                Unk3 = FXField.Read(br, env);
                Unk4 = FXField.Read(br, env);
                Unk5 = FXField.Read(br, env);
                Unk6 = br.ReadSingle();
                Unk7 = br.ReadInt32();
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();
                Unk11 = br.ReadInt32();
            }

            public void Write(BinaryWriterEx bw, FXBehavior beh)
            {
                beh.WriteNode(Unk1);
                beh.WriteNode(Unk2);
                beh.WriteNode(Unk3);
                beh.WriteNode(Unk4);
                beh.WriteNode(Unk5);
                bw.WriteSingle(Unk6);
                bw.WriteInt32(Unk7);
                bw.WriteInt32(Unk8);
                bw.WriteInt32(Unk9);
                bw.WriteInt32(Unk10);
                bw.WriteInt32(Unk11);
            }

            public static DS1RExtraNodes Read(BinaryReaderEx br, FxrEnvironment env)
            {
                var p = new DS1RExtraNodes();
                p.ReadInner(br, env);
                return p;
            }
        }
    }
    
}
