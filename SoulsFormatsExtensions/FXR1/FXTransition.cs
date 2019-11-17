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
        [XmlInclude(typeof(TransitionRef))]
        public class FXTransition : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenTransitions;

            public FXState TargetState;
            public FXNode Evaluator;

            public virtual bool ShouldSerializeTargetState() => true;
            public virtual bool ShouldSerializeNode() => true;

            internal override void ToXIDs(FXR1 fxr)
            {
                TargetState = fxr.ReferenceState(TargetState);
                Evaluator = fxr.ReferenceFXNode(Evaluator);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                TargetState = fxr.DereferenceState(TargetState);
                Evaluator = fxr.DereferenceFXNode(Evaluator);
            }

            public static int GetSize(bool isLong)
                => isLong ? 16 : 8;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int endNodeOffset = br.ReadFXR1Varint();
                int functionOffset = br.ReadFXR1Varint();

                //TESTING
                TargetState = env.GetState(br, endNodeOffset);
                Evaluator = env.GetFXNode(br, functionOffset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterPointer(TargetState);
                env.RegisterPointer(Evaluator);
            }
        }

        public class TransitionRef : FXTransition
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeTargetState() => false;
            public override bool ShouldSerializeNode() => false;

            public TransitionRef(FXTransition refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public TransitionRef()
            {

            }
        }
    }
}
