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
        [XmlInclude(typeof(FXActionRef))]
        public class FXAction : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFXActions;

            [XmlAttribute]
            public int ActionType;
            public FXParamList ParamList;

            public virtual bool ShouldSerializeActionType() => true;
            public virtual bool ShouldSerializeParamList() => true;

            public static int GetSize(bool isLong)
                => (isLong ? 8 : 4) + FXParamList.GetSize(isLong);

            internal override void ToXIDs(FXR1 fxr)
            {
                ParamList = fxr.ReferenceFXParamList(ParamList);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                ParamList = fxr.DereferenceFXParamList(ParamList);
            }

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                ActionType = br.ReadFXR1Varint();
                ParamList = env.GetEffect(br, br.Position);
                br.Position += FXParamList.GetSize(br.VarintLong);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                bw.WriteFXR1Varint(ActionType);
                ParamList.Write(bw, env);
            }
        }

        
        public class FXActionRef : FXAction
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeActionType() => false;
            public override bool ShouldSerializeParamList() => false;

            public FXActionRef(FXAction refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public FXActionRef()
            {

            }
        }
    }
}
