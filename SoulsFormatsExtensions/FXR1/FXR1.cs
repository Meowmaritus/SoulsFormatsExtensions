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

        public FXParam RootFXParam { get; set; }


        [XmlIgnore]
        public static bool FlattenFlowNodes = true;
        [XmlIgnore]
        public static bool FlattenFXParamPointers = false;
        [XmlIgnore]
        public static bool FlattenFlowEdges = true;
        [XmlIgnore]
        public static bool FlattenFXActions = false;
        [XmlIgnore]
        public static bool FlattenFXParams = false;
        [XmlIgnore]
        public static bool FlattenFXParamLists = false;
        [XmlIgnore]
        public static bool FlattenFXBehaviors = false;
        [XmlIgnore]
        public static bool FlattenTemplates = false;

        public List<FlowNode> AllFlowNodes { get; set; } = new List<FlowNode>();
        public List<FXParamPointer> AllFXParamPointers { get; set; } = new List<FXParamPointer>();
        public List<FlowEdge> AllFlowEdges { get; set; } = new List<FlowEdge>();
        public List<FXAction> AllFXActions { get; set; } = new List<FXAction>();
        public List<FXParam> AllFXParams { get; set; } = new List<FXParam>();
        public List<FXParamList> AllFXParamLists { get; set; } = new List<FXParamList>();
        public List<FXBehavior> AllFXBehaviors { get; set; } = new List<FXBehavior>();
        public List<Template> AllTemplates { get; set; } = new List<Template>();



        public bool ShouldSerializeAllFlowNodes() => FlattenFlowNodes;
        public bool ShouldSerializeAllFXParamPointers() => FlattenFXParamPointers;
        public bool ShouldSerializeAllFlowEdges() => FlattenFlowEdges;
        public bool ShouldSerializeAllFXActions() => FlattenFXActions;
        public bool ShouldSerializeAllFXParams() => FlattenFXParams;
        public bool ShouldSerializeAllFXParamLists() => FlattenFXParamLists;
        public bool ShouldSerializeAllFXBehaviors() => FlattenFXBehaviors;
        public bool ShouldSerializeAllTemplates() => FlattenTemplates;

        
        public FlowNode GetFlowNode(string xid) => AllFlowNodes.FirstOrDefault(x => x.XID == xid);
        public FlowNode DereferenceFlowNode(FlowNode v)
        {
            if (!FlattenFlowNodes)
                return v;

            if (v is FlowNodeRef asRef)
                return GetFlowNode(asRef.ReferenceXID);
            else
                return v;
        }
        public FlowNode ReferenceFlowNode(FlowNode v)
        {
            if (!FlattenFlowNodes)
                return v;

            if (v is FlowNodeRef asRef)
                return v;
            else
                return new FlowNodeRef(v);
        }

        
        public FXParamPointer GetFXParamPointer(string xid) => AllFXParamPointers.FirstOrDefault(x => x.XID == xid);
        public FXParamPointer DereferenceFXParamPointer(FXParamPointer v)
        {
            if (!FlattenFXParamPointers)
                return v;

            if (v is FXParamPointerRef asRef)
                return GetFXParamPointer(asRef.ReferenceXID);
            else
                return v;
        }
        public FXParamPointer ReferenceFXParamPointer(FXParamPointer v)
        {
            if (!FlattenFXParamPointers)
                return v;

            if (v is FXParamPointerRef asRef)
                return v;
            else
                return new FXParamPointerRef(v);
        }

        
        public FlowEdge GetFlowEdge(string xid) => AllFlowEdges.FirstOrDefault(x => x.XID == xid);
        public FlowEdge DereferenceFlowEdge(FlowEdge v)
        {
            if (!FlattenFlowEdges)
                return v;

            if (v is FlowEdgeRef asRef)
                return GetFlowEdge(asRef.ReferenceXID);
            else
                return v;
        }

        public FlowEdge ReferenceFlowEdge(FlowEdge v)
        {
            if (!FlattenFlowEdges)
                return v;

            if (v is FlowEdgeRef asRef)
                return v;
            else
                return new FlowEdgeRef(v);
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

        
        public FXParam GetFXParam(string xid) => AllFXParams.FirstOrDefault(x => x.XID == xid);
        public FXParam DereferenceFXParam(FXParam v)
        {
            if (!FlattenFXParams)
                return v;

            if (v is FXParam.FXParamRef asRef)
                return GetFXParam(asRef.ReferenceXID);
            else
                return v;
        }
        public FXParam ReferenceFXParam(FXParam v)
        {
            if (!FlattenFXParams)
                return v;

            if (v is FXParam.FXParamRef asRef)
                return v;
            else
                return new FXParam.FXParamRef(v);
        }

        
        public FXParamList GetFXParamList(string xid) => AllFXParamLists.FirstOrDefault(x => x.XID == xid);
        public FXParamList DereferenceFXParamList(FXParamList v)
        {
            if (!FlattenFXParamLists)
                return v;

            if (v is FXParamListRef asRef)
                return GetFXParamList(asRef.ReferenceXID);
            else
                return v;
        }
        public FXParamList ReferenceFXParamList(FXParamList v)
        {
            if (!FlattenFXParamLists)
                return v;

            if (v is FXParamListRef asRef)
                return v;
            else
                return new FXParamListRef(v);
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

        
        public Template GetTemplate(string xid) => AllTemplates.FirstOrDefault(x => x.XID == xid);
        public Template DereferenceTemplate(Template v)
        {
            if (!FlattenTemplates)
                return v;

            if (v is TemplateRef asRef)
                return GetTemplate(asRef.ReferenceXID);
            else
                return v;
        }
        public Template ReferenceTemplate(Template v)
        {
            if (!FlattenTemplates)
                return v;

            if (v is TemplateRef asRef)
                return v;
            else
                return new TemplateRef(v);
        }


        [XmlIgnore]
        public List<FXField> Debug_AllLoadedParams;

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
            AllFXParamLists.Clear();
            AllFXActions.Clear();
            AllFlowEdges.Clear();
            AllFlowNodes.Clear();
            AllFXParams.Clear();
            AllFXParamPointers.Clear();

            env.fxr = this;

            //br.StepIn(metadataTableOffset);
            //env.ReadPointerTable();
            //br.StepOut();

            br.Pad(16);

            RootFXParam = env.GetFXParam(br, br.Position);

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
                else if (kvp.Value is Template asTemplate)
                {
                    Register("Template", kvp.Key, AllTemplates, asTemplate);
                }
                else if (kvp.Value is FXParamList asFXParamList)
                {
                    Register("FXParamList", kvp.Key, AllFXParamLists, asFXParamList);
                }
                else if (kvp.Value is FXAction asFXAction)
                {
                    Register("FXAction", kvp.Key, AllFXActions, asFXAction);
                }
                else if (kvp.Value is FlowEdge asFlowEdge)
                {
                    Register("FlowEdge", kvp.Key, AllFlowEdges, asFlowEdge);
                }
                else if (kvp.Value is FlowNode asFlowNode)
                {
                    Register("FlowNode", kvp.Key, AllFlowNodes, asFlowNode);
                }
                else if (kvp.Value is FXParam asFXParam)
                {
                    Register("FXParam", kvp.Key, AllFXParams, asFXParam);
                }
                else if (kvp.Value is FXParamPointer asFXParamPointer)
                {
                    Register("FXParamPointer", kvp.Key, AllFXParamPointers, asFXParamPointer);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            Debug_AllLoadedParams = env.Debug_AllReadParams;
        }

        public void Flatten()
        {
            foreach (var x in AllFXBehaviors)
                x.ToXIDs(this);
            foreach (var x in AllTemplates)
                x.ToXIDs(this);
            foreach (var x in AllFXParamLists)
                x.ToXIDs(this);
            foreach (var x in AllFXActions)
                x.ToXIDs(this);
            foreach (var x in AllFlowEdges)
                x.ToXIDs(this);
            foreach (var x in AllFlowNodes)
                x.ToXIDs(this);
            foreach (var x in AllFXParams)
                x.ToXIDs(this);
            foreach (var x in AllFXParamPointers)
                x.ToXIDs(this);

            RootFXParam.ToXIDs(this);
            //RootFXParam = ReferenceFXParam(RootFXParam);
        }

        public void Unflatten()
        {
            foreach (var x in AllFXBehaviors)
                x.FromXIDs(this);
            foreach (var x in AllTemplates)
                x.FromXIDs(this);
            foreach (var x in AllFXParamLists)
                x.FromXIDs(this);
            foreach (var x in AllFXActions)
                x.FromXIDs(this);
            foreach (var x in AllFlowEdges)
                x.FromXIDs(this);
            foreach (var x in AllFlowNodes)
                x.FromXIDs(this);
            foreach (var x in AllFXParams)
                x.FromXIDs(this);
            foreach (var x in AllFXParamPointers)
                x.FromXIDs(this);

            RootFXParam.FromXIDs(this);
            //RootFXParam = DereferenceFXParam(RootFXParam);
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

            env.RegisterPointer(RootFXParam);

            bw.ReserveFXR1Varint("OffsetToTable");

            bw.ReserveInt32("TablePointerCount");
            bw.ReserveInt32("TableFXParamCount");

            bw.WriteInt32(Unk1);
            bw.WriteInt32(Unk2);

            bw.Pad(16);
            
            // Write RootFXParam and everything else :fatcat:
            env.FinishRecursiveWrite();

            bw.FillFXR1Varint("OffsetToTable", (int)bw.Position);
            env.WritePointerTable("TablePointerCount");
            env.WriteFXParamTable("TableFXParamCount");

        }

    }
}
