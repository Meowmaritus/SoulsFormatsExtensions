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

        public Function RootFunction { get; set; }


        [XmlIgnore]
        public static bool FlattenFlowNodes = true;
        [XmlIgnore]
        public static bool FlattenFunctionPointers = false;
        [XmlIgnore]
        public static bool FlattenFlowEdges = true;
        [XmlIgnore]
        public static bool FlattenFlowActions = true;
        [XmlIgnore]
        public static bool FlattenFunctions = false;
        [XmlIgnore]
        public static bool FlattenEffects = false;
        [XmlIgnore]
        public static bool FlattenBehaviors = false;
        [XmlIgnore]
        public static bool FlattenTemplates = false;


        public bool ShouldSerializeAllFlowNodes() => FlattenFlowNodes;
        public bool ShouldSerializeAllFunctionPointers() => FlattenFunctionPointers;
        public bool ShouldSerializeAllFlowEdges() => FlattenFlowEdges;
        public bool ShouldSerializeAllFlowActions() => FlattenFlowActions;
        public bool ShouldSerializeAllFunctions() => FlattenFunctions;
        public bool ShouldSerializeAllEffects() => FlattenEffects;
        public bool ShouldSerializeAllBehaviors() => FlattenBehaviors;
        public bool ShouldSerializeAllTemplates() => FlattenBehaviors;

        public List<FlowNode> AllFlowNodes { get; set; } = new List<FlowNode>();
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

        public List<FunctionPointer> AllFunctionPointers { get; set; } = new List<FunctionPointer>();
        public FunctionPointer GetFunctionPointer(string xid) => AllFunctionPointers.FirstOrDefault(x => x.XID == xid);
        public FunctionPointer DereferenceFunctionPointer(FunctionPointer v)
        {
            if (!FlattenFunctionPointers)
                return v;

            if (v is FunctionPointerRef asRef)
                return GetFunctionPointer(asRef.ReferenceXID);
            else
                return v;
        }
        public FunctionPointer ReferenceFunctionPointer(FunctionPointer v)
        {
            if (!FlattenFunctionPointers)
                return v;

            if (v is FunctionPointerRef asRef)
                return v;
            else
                return new FunctionPointerRef(v);
        }

        public List<FlowEdge> AllFlowEdges { get; set; } = new List<FlowEdge>();
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

        public List<FlowAction> AllFlowActions { get; set; } = new List<FlowAction>();
        public FlowAction GetFlowAction(string xid) => AllFlowActions.FirstOrDefault(x => x.XID == xid);
        public FlowAction DereferenceFlowAction(FlowAction v)
        {
            if (!FlattenFlowActions)
                return v;

            if (v is FlowActionRef asRef)
                return GetFlowAction(asRef.ReferenceXID);
            else
                return v;
        }
        public FlowAction ReferenceFlowAction(FlowAction v)
        {
            if (!FlattenFlowActions)
                return v;

            if (v is FlowActionRef asRef)
                return v;
            else
                return new FlowActionRef(v);
        }

        public List<Function> AllFunctions { get; set; } = new List<Function>();
        public Function GetFunction(string xid) => AllFunctions.FirstOrDefault(x => x.XID == xid);
        public Function DereferenceFunction(Function v)
        {
            if (!FlattenFunctions)
                return v;

            if (v is Function.FunctionRef asRef)
                return GetFunction(asRef.ReferenceXID);
            else
                return v;
        }
        public Function ReferenceFunction(Function v)
        {
            if (!FlattenFunctions)
                return v;

            if (v is Function.FunctionRef asRef)
                return v;
            else
                return new Function.FunctionRef(v);
        }

        public List<Effect> AllEffects { get; set; } = new List<Effect>();
        public Effect GetEffect(string xid) => AllEffects.FirstOrDefault(x => x.XID == xid);
        public Effect DereferenceEffect(Effect v)
        {
            if (!FlattenEffects)
                return v;

            if (v is EffectRef asRef)
                return GetEffect(asRef.ReferenceXID);
            else
                return v;
        }
        public Effect ReferenceEffect(Effect v)
        {
            if (!FlattenEffects)
                return v;

            if (v is EffectRef asRef)
                return v;
            else
                return new EffectRef(v);
        }

        public List<Behavior> AllBehaviors { get; set; } = new List<Behavior>();
        public Behavior GetBehavior(string xid) => AllBehaviors.FirstOrDefault(x => x.XID == xid);
        public Behavior DereferenceBehavior(Behavior v)
        {
            if (!FlattenBehaviors)
                return v;

            if (v is BehaviorRef asRef)
                return GetBehavior(asRef.ReferenceXID);
            else
                return v;
        }
        public Behavior ReferenceBehavior(Behavior v)
        {
            if (!FlattenBehaviors)
                return v;

            if (v is BehaviorRef asRef)
                return v;
            else
                return new BehaviorRef(v);
        }

        public List<Template> AllTemplates { get; set; } = new List<Template>();
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
        public List<Param> Debug_AllLoadedParams;

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

            AllBehaviors.Clear();
            AllTemplates.Clear();
            AllEffects.Clear();
            AllFlowActions.Clear();
            AllFlowEdges.Clear();
            AllFlowNodes.Clear();
            AllFunctions.Clear();
            AllFunctionPointers.Clear();

            env.fxr = this;

            //br.StepIn(metadataTableOffset);
            //env.ReadPointerTable();
            //br.StepOut();

            br.Pad(16);

            RootFunction = env.GetFunction(br, br.Position);

            void Register<T>(string type, long offset, List<T> list, T thing)
                where T : XIDable
            {
                thing.XID = $"{type}_0x{offset:X}";

                if (!list.Contains(thing))
                    list.Add(thing);
            }

            foreach (var kvp in env.ObjectsByOffset)
            {
                if (kvp.Value is Behavior asBehavior)
                {
                    Register("Behavior", kvp.Key, AllBehaviors, asBehavior);
                }
                else if (kvp.Value is Template asTemplate)
                {
                    Register("Template", kvp.Key, AllTemplates, asTemplate);
                }
                else if (kvp.Value is Effect asEffect)
                {
                    Register("Effect", kvp.Key, AllEffects, asEffect);
                }
                else if (kvp.Value is FlowAction asFlowAction)
                {
                    Register("FlowAction", kvp.Key, AllFlowActions, asFlowAction);
                }
                else if (kvp.Value is FlowEdge asFlowEdge)
                {
                    Register("FlowEdge", kvp.Key, AllFlowEdges, asFlowEdge);
                }
                else if (kvp.Value is FlowNode asFlowNode)
                {
                    Register("FlowNode", kvp.Key, AllFlowNodes, asFlowNode);
                }
                else if (kvp.Value is Function asFunction)
                {
                    Register("Function", kvp.Key, AllFunctions, asFunction);
                }
                else if (kvp.Value is FunctionPointer asFunctionPointer)
                {
                    Register("FunctionPointer", kvp.Key, AllFunctionPointers, asFunctionPointer);
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
            foreach (var x in AllBehaviors)
                x.ToXIDs(this);
            foreach (var x in AllTemplates)
                x.ToXIDs(this);
            foreach (var x in AllEffects)
                x.ToXIDs(this);
            foreach (var x in AllFlowActions)
                x.ToXIDs(this);
            foreach (var x in AllFlowEdges)
                x.ToXIDs(this);
            foreach (var x in AllFlowNodes)
                x.ToXIDs(this);
            foreach (var x in AllFunctions)
                x.ToXIDs(this);
            foreach (var x in AllFunctionPointers)
                x.ToXIDs(this);

            RootFunction.ToXIDs(this);
            //RootFunction = ReferenceFunction(RootFunction);
        }

        public void Unflatten()
        {
            foreach (var x in AllBehaviors)
                x.FromXIDs(this);
            foreach (var x in AllTemplates)
                x.FromXIDs(this);
            foreach (var x in AllEffects)
                x.FromXIDs(this);
            foreach (var x in AllFlowActions)
                x.FromXIDs(this);
            foreach (var x in AllFlowEdges)
                x.FromXIDs(this);
            foreach (var x in AllFlowNodes)
                x.FromXIDs(this);
            foreach (var x in AllFunctions)
                x.FromXIDs(this);
            foreach (var x in AllFunctionPointers)
                x.FromXIDs(this);

            RootFunction.FromXIDs(this);
            //RootFunction = DereferenceFunction(RootFunction);
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

            env.RegisterPointer(RootFunction);

            bw.ReserveFXR1Varint("OffsetToTable");

            bw.ReserveInt32("TablePointerCount");
            bw.ReserveInt32("TableFunctionCount");

            bw.WriteInt32(Unk1);
            bw.WriteInt32(Unk2);

            bw.Pad(16);
            
            // Write RootFunction and everything else :fatcat:
            env.FinishRecursiveWrite();

            bw.FillFXR1Varint("OffsetToTable", (int)bw.Position);
            env.WritePointerTable("TablePointerCount");
            env.WriteFunctionTable("TableFunctionCount");

        }

    }
}
