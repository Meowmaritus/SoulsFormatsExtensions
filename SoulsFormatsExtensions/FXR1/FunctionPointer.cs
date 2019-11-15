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
        [XmlInclude(typeof(FunctionPointerRef))]
        public class FunctionPointer : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFunctionPointers;

            public static int GetSize(bool isLong)
                => isLong ? 8 : 4;

            public Function Func;

            internal override void ToXIDs(FXR1 fxr)
            {
                Func = fxr.ReferenceFunction(Func);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                Func = fxr.DereferenceFunction(Func);
            }

            public virtual bool ShouldSerializeFunc() => true;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long funcOffset = br.ReadFXR1Varint();
                Func = env.GetFunction(br, funcOffset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterPointer(Func);
            }
        }

        public class FunctionPointerRef : FunctionPointer
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeFunc() => false;

            public FunctionPointerRef(FunctionPointer refVal)
            {
                ReferenceXID = refVal.XID;
            }
            public FunctionPointerRef()
            {

            }
        }
    }
}
