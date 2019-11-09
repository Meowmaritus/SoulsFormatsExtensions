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
        public class ASTPool1
        {
            public Function Func;

            public static ASTPool1 Read(BinaryReaderEx br, FxrEnvironment env)
            {
                var ast = new ASTPool1();
                long funcOffset = br.ReadFXR1Varint();
                ast.Func = env.GetFunction(br, funcOffset);
                return ast;
            }
        }
    }
}
