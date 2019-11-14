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
        public class ASTFunction
        {
            public static int GetSize(bool isLong)
                => isLong ? 8 : 4;

            public Function Func;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long funcOffset = br.ReadFXR1Varint();
                Func = env.GetFunction(br, funcOffset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterPointer(Func);
            }
        }
    }
}
