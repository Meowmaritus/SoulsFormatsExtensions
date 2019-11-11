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

            public static ASTFunction Read(BinaryReaderEx br, FxrEnvironment env)
            {
                var ast = new ASTFunction();
                long funcOffset = br.ReadFXR1Varint();
                ast.Func = env.GetFunction(br, funcOffset);
                return ast;
            }
        }
    }
}
