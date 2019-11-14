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
        public class FlowEdge
        {
            public int EndFlowNodeIndex;

            [XmlIgnore]
            internal FlowNode EndNode;

            internal void CalculateIndices(FxrEnvironment env)
            {
                EndFlowNodeIndex = env.GetFlowNodeIndex(EndNode);
                //EndNode = null;
            }

            public Function Func;

            public static int GetSize(bool isLong)
                => isLong ? 16 : 8;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int endNodeOffset = br.ReadFXR1Varint();
                int functionOffset = br.ReadFXR1Varint();

                //TESTING
                EndNode = env.GetFlowNode(br, endNodeOffset);
                Func = env.GetFunction(br, functionOffset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterPointer(env.fxr.FlowNodes[EndFlowNodeIndex]);
                env.RegisterPointer(Func);
            }

        }
    }
}
