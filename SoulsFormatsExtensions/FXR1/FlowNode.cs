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
        public class FlowNode
        {
            public List<FlowEdge> Edges;
            public List<FlowAction> Actions;

            public static int GetSize(bool isLong)
                => isLong ? 24 : 16;

            public static FlowNode Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int edgesOffset = br.ReadFXR1Varint();
                int actionsOffset = br.ReadFXR1Varint();
                int edgeNum = br.ReadInt32();
                int actionNum = br.ReadInt32();

                var node = new FlowNode();

                node.Edges = new List<FlowEdge>(edgeNum);
                node.Actions = new List<FlowAction>(actionNum);

                br.StepIn(edgesOffset);
                for (int i = 0; i < edgeNum; i++)
                {
                    node.Edges.Add(env.GetFlowEdge(br, br.Position));
                    br.Position += FlowEdge.GetSize(br.VarintLong);
                }
                br.StepOut();

                br.StepIn(actionsOffset);
                for (int i = 0; i < actionNum; i++)
                {
                    node.Actions.Add(env.GetFlowAction(br, br.Position));
                    br.Position += FlowAction.GetSize(br.VarintLong);
                }
                br.StepOut();

                return node;
            }
        }
    }
}
