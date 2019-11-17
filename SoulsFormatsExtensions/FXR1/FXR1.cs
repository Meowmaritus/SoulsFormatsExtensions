using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoulsFormatsExtensions
{
    public partial class FXR1 : SoulsFile<FXR1>
    {
        public bool BigEndian { get; set; } = false;
        public bool Wide { get; set; } = false;
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }

        public FXNode RootFXNode { get; set; }


        [XmlIgnore]
        public static bool FlattenStates = true;
        [XmlIgnore]
        public static bool FlattenFXNodePointers = false;
        [XmlIgnore]
        public static bool FlattenTransitions = true;
        [XmlIgnore]
        public static bool FlattenFXActions = false;
        [XmlIgnore]
        public static bool FlattenFXNodes = false;
        [XmlIgnore]
        public static bool FlattenFXContainers = false;
        [XmlIgnore]
        public static bool FlattenFXBehaviors = false;
        [XmlIgnore]
        public static bool FlattenTemplates = false;

        public List<FXState> AllStates { get; set; } = new List<FXState>();
        public List<FXNodePointer> AllFXNodePointers { get; set; } = new List<FXNodePointer>();
        public List<FXTransition> AllTransitions { get; set; } = new List<FXTransition>();
        public List<FXAction> AllFXActions { get; set; } = new List<FXAction>();
        public List<FXNode> AllFXNodes { get; set; } = new List<FXNode>();
        public List<FXContainer> AllFXContainers { get; set; } = new List<FXContainer>();
        public List<FXBehavior> AllFXBehaviors { get; set; } = new List<FXBehavior>();
        public List<FXTemplate> AllTemplates { get; set; } = new List<FXTemplate>();



        public bool ShouldSerializeAllStates() => FlattenStates;
        public bool ShouldSerializeAllFXNodePointers() => FlattenFXNodePointers;
        public bool ShouldSerializeAllTransitions() => FlattenTransitions;
        public bool ShouldSerializeAllFXActions() => FlattenFXActions;
        public bool ShouldSerializeAllFXNodes() => FlattenFXNodes;
        public bool ShouldSerializeAllFXContainers() => FlattenFXContainers;
        public bool ShouldSerializeAllFXBehaviors() => FlattenFXBehaviors;
        public bool ShouldSerializeAllTemplates() => FlattenTemplates;

        
        public FXState GetState(string xid) => AllStates.FirstOrDefault(x => x.XID == xid);
        public FXState DereferenceState(FXState v)
        {
            if (!FlattenStates)
                return v;

            if (v is StateRef asRef)
                return GetState(asRef.ReferenceXID);
            else
                return v;
        }
        public FXState ReferenceState(FXState v)
        {
            if (!FlattenStates)
                return v;

            if (v is StateRef asRef)
                return v;
            else
                return new StateRef(v);
        }

        
        public FXNodePointer GetFXNodePointer(string xid) => AllFXNodePointers.FirstOrDefault(x => x.XID == xid);
        public FXNodePointer DereferenceFXNodePointer(FXNodePointer v)
        {
            if (!FlattenFXNodePointers)
                return v;

            if (v is FXNodePointerRef asRef)
                return GetFXNodePointer(asRef.ReferenceXID);
            else
                return v;
        }
        public FXNodePointer ReferenceFXNodePointer(FXNodePointer v)
        {
            if (!FlattenFXNodePointers)
                return v;

            if (v is FXNodePointerRef asRef)
                return v;
            else
                return new FXNodePointerRef(v);
        }

        
        public FXTransition GetTransition(string xid) => AllTransitions.FirstOrDefault(x => x.XID == xid);
        public FXTransition DereferenceTransition(FXTransition v)
        {
            if (!FlattenTransitions)
                return v;

            if (v is TransitionRef asRef)
                return GetTransition(asRef.ReferenceXID);
            else
                return v;
        }

        public FXTransition ReferenceTransition(FXTransition v)
        {
            if (!FlattenTransitions)
                return v;

            if (v is TransitionRef asRef)
                return v;
            else
                return new TransitionRef(v);
        }

        
        public FXAction GetFXAction(string xid) => AllFXActions.FirstOrDefault(x => x.XID == xid);
        public FXAction DereferenceFXAction(FXAction v)
        {
            if (!FlattenFXActions)
                return v;

            if (v is FXActionRef asRef)
                return GetFXAction(asRef.ReferenceXID);
            else
                return v;
        }
        public FXAction ReferenceFXAction(FXAction v)
        {
            if (!FlattenFXActions)
                return v;

            if (v is FXActionRef asRef)
                return v;
            else
                return new FXActionRef(v);
        }

        
        public FXNode GetFXNode(string xid) => AllFXNodes.FirstOrDefault(x => x.XID == xid);
        public FXNode DereferenceFXNode(FXNode v)
        {
            if (!FlattenFXNodes)
                return v;

            if (v is FXNode.FXNodeRef asRef)
                return GetFXNode(asRef.ReferenceXID);
            else
                return v;
        }
        public FXNode ReferenceFXNode(FXNode v)
        {
            if (!FlattenFXNodes)
                return v;

            if (v is FXNode.FXNodeRef asRef)
                return v;
            else
                return new FXNode.FXNodeRef(v);
        }

        
        public FXContainer GetFXContainer(string xid) => AllFXContainers.FirstOrDefault(x => x.XID == xid);
        public FXContainer DereferenceFXContainer(FXContainer v)
        {
            if (!FlattenFXContainers)
                return v;

            if (v is FXContainerRef asRef)
                return GetFXContainer(asRef.ReferenceXID);
            else
                return v;
        }
        public FXContainer ReferenceFXContainer(FXContainer v)
        {
            if (!FlattenFXContainers)
                return v;

            if (v is FXContainerRef asRef)
                return v;
            else
                return new FXContainerRef(v);
        }

        
        public FXBehavior GetBehavior(string xid) => AllFXBehaviors.FirstOrDefault(x => x.XID == xid);
        public FXBehavior DereferenceFXBehavior(FXBehavior v)
        {
            if (!FlattenFXBehaviors)
                return v;

            if (v is BehaviorRef asRef)
                return GetBehavior(asRef.ReferenceXID);
            else
                return v;
        }
        public FXBehavior ReferenceFXBehavior(FXBehavior v)
        {
            if (!FlattenFXBehaviors)
                return v;

            if (v is BehaviorRef asRef)
                return v;
            else
                return new BehaviorRef(v);
        }

        
        public FXTemplate GetTemplate(string xid) => AllTemplates.FirstOrDefault(x => x.XID == xid);
        public FXTemplate DereferenceTemplate(FXTemplate v)
        {
            if (!FlattenTemplates)
                return v;

            if (v is FXTemplateRef asRef)
                return GetTemplate(asRef.ReferenceXID);
            else
                return v;
        }
        public FXTemplate ReferenceTemplate(FXTemplate v)
        {
            if (!FlattenTemplates)
                return v;

            if (v is FXTemplateRef asRef)
                return v;
            else
                return new FXTemplateRef(v);
        }


        [XmlIgnore]
        public List<FXField> Debug_AllLoadedNodes;

        protected override void Read(BinaryReaderEx br)
        {
            br.AssertASCII("FXR\0");
            int endianCheck = br.AssertInt32(0x10000, 0x100);
            br.BigEndian = BigEndian = (endianCheck == 0x100);

            uint longCheck = br.GetUInt32(0xC);
            br.VarintLong = Wide = (longCheck == 0 || longCheck == 0xCDCDCDCD);

            long mainDataOffset = br.ReadFXR1Varint();
            int metadataTableOffset = br.ReadInt32();

            int pointerTableCount = br.ReadInt32();
            int functionTableCount = br.ReadInt32();
            Unk1 = br.ReadInt32();
            Unk2 = br.ReadInt32();

            var env = new FxrEnvironment();

            AllFXBehaviors.Clear();
            AllTemplates.Clear();
            AllFXContainers.Clear();
            AllFXActions.Clear();
            AllTransitions.Clear();
            AllStates.Clear();
            AllFXNodes.Clear();
            AllFXNodePointers.Clear();

            env.fxr = this;

            //br.StepIn(metadataTableOffset);
            //env.ReadPointerTable();
            //br.StepOut();

            br.Pad(16);

            RootFXNode = env.GetFXNode(br, br.Position);

            void Register<T>(string type, long offset, List<T> list, T thing)
                where T : XIDable
            {
                thing.XID = $"{type}_0x{offset:X}";

                if (!list.Contains(thing))
                    list.Add(thing);
            }

            foreach (var kvp in env.ObjectsByOffset)
            {
                if (kvp.Value is FXBehavior asBehavior)
                {
                    Register("Behavior", kvp.Key, AllFXBehaviors, asBehavior);
                }
                else if (kvp.Value is FXTemplate asTemplate)
                {
                    Register("Template", kvp.Key, AllTemplates, asTemplate);
                }
                else if (kvp.Value is FXContainer asFXContainer)
                {
                    Register("FXContainer", kvp.Key, AllFXContainers, asFXContainer);
                }
                else if (kvp.Value is FXAction asFXAction)
                {
                    Register("FXAction", kvp.Key, AllFXActions, asFXAction);
                }
                else if (kvp.Value is FXTransition asTransition)
                {
                    Register("Transition", kvp.Key, AllTransitions, asTransition);
                }
                else if (kvp.Value is FXState asState)
                {
                    Register("State", kvp.Key, AllStates, asState);
                }
                else if (kvp.Value is FXNode asFXNode)
                {
                    Register("FXNode", kvp.Key, AllFXNodes, asFXNode);
                }
                else if (kvp.Value is FXNodePointer asFXNodePointer)
                {
                    Register("FXNodePointer", kvp.Key, AllFXNodePointers, asFXNodePointer);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            Debug_AllLoadedNodes = env.Debug_AllReadNodes;
        }

        public void Flatten()
        {
            foreach (var x in AllFXBehaviors)
                x.ToXIDs(this);
            foreach (var x in AllTemplates)
                x.ToXIDs(this);
            foreach (var x in AllFXContainers)
                x.ToXIDs(this);
            foreach (var x in AllFXActions)
                x.ToXIDs(this);
            foreach (var x in AllTransitions)
                x.ToXIDs(this);
            foreach (var x in AllStates)
                x.ToXIDs(this);
            foreach (var x in AllFXNodes)
                x.ToXIDs(this);
            foreach (var x in AllFXNodePointers)
                x.ToXIDs(this);

            RootFXNode.ToXIDs(this);
            //RootFXNode = ReferenceFXNode(RootFXNode);
        }

        public void Unflatten()
        {
            foreach (var x in AllFXBehaviors)
                x.FromXIDs(this);
            foreach (var x in AllTemplates)
                x.FromXIDs(this);
            foreach (var x in AllFXContainers)
                x.FromXIDs(this);
            foreach (var x in AllFXActions)
                x.FromXIDs(this);
            foreach (var x in AllTransitions)
                x.FromXIDs(this);
            foreach (var x in AllStates)
                x.FromXIDs(this);
            foreach (var x in AllFXNodes)
                x.FromXIDs(this);
            foreach (var x in AllFXNodePointers)
                x.FromXIDs(this);

            RootFXNode.FromXIDs(this);
            //RootFXNode = DereferenceFXNode(RootFXNode);
        }

        protected override void Write(BinaryWriterEx bw)
        {
            bw.WriteASCII("FXR\0");
            bw.BigEndian = BigEndian;
            bw.WriteUInt32(0x10000);
            bw.VarintLong = Wide;
            var env = new FxrEnvironment();
            env.bw = bw;
            env.fxr = this;

            env.RegisterPointer(RootFXNode);

            bw.ReserveFXR1Varint("OffsetToTable");

            bw.ReserveInt32("TablePointerCount");
            bw.ReserveInt32("TableFXNodeCount");

            bw.WriteInt32(Unk1);
            bw.WriteInt32(Unk2);

            bw.Pad(16);
            
            // Write RootFXNode and everything else :fatcat:
            env.FinishRecursiveWrite();

            bw.FillFXR1Varint("OffsetToTable", (int)bw.Position);
            env.WritePointerTable("TablePointerCount");
            env.WriteFXNodeTable("TableFXNodeCount");

        }

    }
}
