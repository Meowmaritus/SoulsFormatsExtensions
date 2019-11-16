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
        [XmlInclude(typeof(FXParamPointerRef))]
        public class FXParamPointer : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFXParamPointers;

            public static int GetSize(bool isLong)
                => isLong ? 8 : 4;

            public FXParam Param;

            internal override void ToXIDs(FXR1 fxr)
            {
                Param = fxr.ReferenceFXParam(Param);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                Param = fxr.DereferenceFXParam(Param);
            }

            public virtual bool ShouldSerializeParam() => true;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long funcOffset = br.ReadFXR1Varint();
                Param = env.GetFXParam(br, funcOffset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterPointer(Param);
            }
        }

        public class FXParamPointerRef : FXParamPointer
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeParam() => false;

            public FXParamPointerRef(FXParamPointer refVal)
            {
                ReferenceXID = refVal.XID;
            }
            public FXParamPointerRef()
            {

            }
        }
    }
}
