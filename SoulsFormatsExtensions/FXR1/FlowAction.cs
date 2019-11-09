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

            public static FlowAction Read(BinaryReaderEx br, FxrEnvironment env)
            {
                var action = new FlowAction();
                action.ActionType = br.ReadFXR1Varint();
                action.ActionAst = env.GetAST(br, br.Position);
                br.Position += AST.GetSize(br.VarintLong);

                return action;
            }
        }
    }
}
