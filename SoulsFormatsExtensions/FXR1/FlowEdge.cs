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
        [XmlInclude(typeof(FlowEdgeRef))]
        public class FlowEdge : XIDable
        {
            public FlowNode EndNode;
            public Function Func;

            public virtual bool ShouldSerializeEndNode() => true;
            public virtual bool ShouldSerializeFunc() => true;

            internal override void ToXIDs(FXR1 fxr)
            {
                EndNode = fxr.ReferenceFlowNode(EndNode);
                Func = fxr.ReferenceFunction(Func);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                EndNode = fxr.DereferenceFlowNode(EndNode);
                Func = fxr.DereferenceFunction(Func);
            }

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
                env.RegisterPointer(EndNode);
                env.RegisterPointer(Func);
            }
        }

        public class FlowEdgeRef : FlowEdge
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeEndNode() => false;
            public override bool ShouldSerializeFunc() => false;

            public FlowEdgeRef(FlowEdge refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public FlowEdgeRef()
            {

            }
        }
    }
}
