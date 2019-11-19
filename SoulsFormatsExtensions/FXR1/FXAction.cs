﻿using SoulsFormats;
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
            public FXContainer Container;

            public virtual bool ShouldSerializeActionType() => true;
            public virtual bool ShouldSerializeContainer() => true;

            public static int GetSize(bool isLong)
                => (isLong ? 8 : 4) + FXContainer.GetSize(isLong);

            internal override void ToXIDs(FXR1 fxr)
            {
                Container = fxr.ReferenceFXContainer(Container);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                Container = fxr.DereferenceFXContainer(Container);
            }

            internal void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                ActionType = br.ReadFXR1Varint();
                Container = env.GetFXContainer(br, br.Position);
                br.Position += FXContainer.GetSize(br.VarintLong);
            }

            internal void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                //bw.Pad(8);
                env.RegisterOffset(bw.Position, this);
                bw.WriteFXR1Varint(ActionType);
                Container.Write(bw, env);
            }
        }

        
        public class FXActionRef : FXAction
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeActionType() => false;
            public override bool ShouldSerializeContainer() => false;

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
