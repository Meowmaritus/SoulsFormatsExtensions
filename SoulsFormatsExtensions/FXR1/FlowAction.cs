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
        [XmlInclude(typeof(FlowActionRef))]
        public class FlowAction : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFlowActions;

            [XmlAttribute]
            public int ActionType;
            public Effect ActionEffect;

            public virtual bool ShouldSerializeActionType() => true;
            public virtual bool ShouldSerializeActionEffect() => true;

            public static int GetSize(bool isLong)
                => (isLong ? 8 : 4) + Effect.GetSize(isLong);

            internal override void ToXIDs(FXR1 fxr)
            {
                ActionEffect = fxr.ReferenceEffect(ActionEffect);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                ActionEffect = fxr.DereferenceEffect(ActionEffect);
            }

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                ActionType = br.ReadFXR1Varint();
                ActionEffect = env.GetEffect(br, br.Position);
                br.Position += Effect.GetSize(br.VarintLong);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteFXR1Varint(ActionType);
                ActionEffect.Write(bw, env);
            }
        }

        
        public class FlowActionRef : FlowAction
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeActionType() => false;
            public override bool ShouldSerializeActionEffect() => false;

            public FlowActionRef(FlowAction refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public FlowActionRef()
            {

            }
        }
    }
}
