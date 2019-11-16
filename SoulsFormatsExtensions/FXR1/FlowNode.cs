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
        [XmlInclude(typeof(FlowNodeRef))]
        public class FlowNode : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFlowNodes;

            public List<FlowEdge> Edges;
            public List<FXAction> Actions;

            public virtual bool ShouldSerializeEdges() => true;
            public virtual bool ShouldSerializeActions() => true;

            internal override void ToXIDs(FXR1 fxr)
            {
                for (int i = 0; i < Edges.Count; i++)
                    Edges[i] = fxr.ReferenceFlowEdge(Edges[i]);
                for (int i = 0; i < Actions.Count; i++)
                    Actions[i] = fxr.ReferenceFXAction(Actions[i]);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                for (int i = 0; i < Edges.Count; i++)
                    Edges[i] = fxr.DereferenceFlowEdge(Edges[i]);
                for (int i = 0; i < Actions.Count; i++)
                    Actions[i] = fxr.DereferenceFXAction(Actions[i]);
            }

            public static int GetSize(bool isLong)
                => isLong ? 24 : 16;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int edgesOffset = br.ReadFXR1Varint();
                int actionsOffset = br.ReadFXR1Varint();
                int edgeNum = br.ReadInt32();
                int actionNum = br.ReadInt32();

                Edges = new List<FlowEdge>(edgeNum);
                Actions = new List<FXAction>(actionNum);

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
                    Actions.Add(env.GetFXAction(br, br.Position));
                    br.Position += FXAction.GetSize(br.VarintLong);
                }
                br.StepOut();
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterPointer(Edges);
                env.RegisterPointer(Actions);
                bw.WriteInt32(Edges.Count);
                bw.WriteInt32(Actions.Count);
            }
        }

        public class FlowNodeRef : FlowNode
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeEdges() => false;
            public override bool ShouldSerializeActions() => false;

            public FlowNodeRef(FlowNode refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public FlowNodeRef()
            {

            }
        }
    }
}
