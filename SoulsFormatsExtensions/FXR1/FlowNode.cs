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
        public class FlowNode
        {
            [XmlIgnore]
            internal List<FlowEdge> Edges;

            public List<int> FlowEdgeIndices = new List<int>();

            internal void CalculateIndices(FxrEnvironment env)
            {
                FlowEdgeIndices = new List<int>(Edges.Count);
                for (int i = 0; i < Edges.Count; i++)
                    FlowEdgeIndices.Add(env.GetFlowEdgeIndex(Edges[i]));
                FlowActionIndices = new List<int>(Actions.Count);
                for (int i = 0; i < Actions.Count; i++)
                    FlowActionIndices.Add(env.GetFlowActionIndex(Actions[i]));
                //Actions = null;
                //Edges = null;
            }

            [XmlIgnore]
            internal List<FlowAction> Actions;

            public List<int> FlowActionIndices = new List<int>();

            public static int GetSize(bool isLong)
                => isLong ? 24 : 16;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int edgesOffset = br.ReadFXR1Varint();
                int actionsOffset = br.ReadFXR1Varint();
                int edgeNum = br.ReadInt32();
                int actionNum = br.ReadInt32();

                Edges = new List<FlowEdge>(edgeNum);
                Actions = new List<FlowAction>(actionNum);

                br.StepIn(edgesOffset);
                for (int i = 0; i < edgeNum; i++)
                {
                    Edges.Add(env.GetFlowEdge(br, br.Position));
                    br.Position += FlowEdge.GetSize(br.VarintLong);
                }
                br.StepOut();

                br.StepIn(actionsOffset);
                for (int i = 0; i < actionNum; i++)
                {
                    Actions.Add(env.GetFlowAction(br, br.Position));
                    br.Position += FlowAction.GetSize(br.VarintLong);
                }
                br.StepOut();
            }
        }
    }
}
