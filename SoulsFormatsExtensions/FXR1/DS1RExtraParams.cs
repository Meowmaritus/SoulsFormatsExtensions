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
        public class DS1RExtraParams
        {
            public Param Unk1;
            public Param Unk2;
            public Param Unk3;
            public Param Unk4;
            public Param Unk5;
            public float Unk6;
            public int Unk7;
            public int Unk8;
            public int Unk9;
            public int Unk10;
            public int Unk11;

            private void ReadInner(BinaryReaderEx br, FxrEnvironment env)
            {
                Unk1 = Param.Read(br, env);
                Unk2 = Param.Read(br, env);
                Unk3 = Param.Read(br, env);
                Unk4 = Param.Read(br, env);
                Unk5 = Param.Read(br, env);
                Unk6 = br.ReadSingle();
                Unk7 = br.ReadInt32();
                Unk8 = br.ReadInt32();
                Unk9 = br.ReadInt32();
                Unk10 = br.ReadInt32();
                Unk11 = br.ReadInt32();
            }

            public void Write(BinaryWriterEx bw, Behavior beh)
            {
                beh.WriteParam(Unk1);
                beh.WriteParam(Unk2);
                beh.WriteParam(Unk3);
                beh.WriteParam(Unk4);
                beh.WriteParam(Unk5);
                bw.WriteSingle(Unk6);
                bw.WriteInt32(Unk7);
                bw.WriteInt32(Unk8);
                bw.WriteInt32(Unk9);
                bw.WriteInt32(Unk10);
                bw.WriteInt32(Unk11);
            }

            public static DS1RExtraParams Read(BinaryReaderEx br, FxrEnvironment env)
            {
                var p = new DS1RExtraParams();
                p.ReadInner(br, env);
                return p;
            }
        }
    }
    
}
