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
        public class FlowEdge
        {
            public FlowNode EndNode;
            public Function Func;

            public static int GetSize(bool isLong)
                => isLong ? 16 : 8;

            public static FlowEdge Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int endNodeOffset = br.ReadFXR1Varint();
                int functionOffset = br.ReadFXR1Varint();

                var edge = new FlowEdge();

                edge.EndNode = env.GetFlowNode(br, endNodeOffset);
                edge.Func = env.GetFunction(br, functionOffset);

                return edge;
            }
        }
    }
}
