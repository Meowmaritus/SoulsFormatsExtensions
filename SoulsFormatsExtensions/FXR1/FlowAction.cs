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
            [XmlAttribute]
            public int ActionType;
            public AST ActionAst;

            public virtual bool ShouldSerializeActionType() => true;
            public virtual bool ShouldSerializeActionAst() => true;

            public static int GetSize(bool isLong)
                => (isLong ? 8 : 4) + AST.GetSize(isLong);

            internal override void ToXIDs(FXR1 fxr)
            {
                ActionAst = fxr.ReferenceAST(ActionAst);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                ActionAst = fxr.DereferenceAST(ActionAst);
            }

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

        
        public class FlowActionRef : FlowAction
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeActionType() => false;
            public override bool ShouldSerializeActionAst() => false;

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
