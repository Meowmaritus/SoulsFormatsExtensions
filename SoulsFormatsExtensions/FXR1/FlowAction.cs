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
        public class FlowAction
        {
            public int ActionType;
            public AST ActionAst;

            public static int GetSize(bool isLong)
                => (isLong ? 8 : 4) + AST.GetSize(isLong);

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                ActionType = br.ReadFXR1Varint();
                ActionAst = env.GetAST(br, br.Position);
                br.Position += AST.GetSize(br.VarintLong);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteFXR1Varint(ActionType);
                ActionAst.Write(bw, env);
            }
        }
    }
}
