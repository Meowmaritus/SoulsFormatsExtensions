﻿using SoulsFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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


        internal void WriteToXml(string xmlPath)
        {
            if (File.Exists(xmlPath))
                File.Delete(xmlPath);
            using (var testStream = File.OpenWrite(xmlPath))
            {
                var test = new XmlSerializer(typeof(FXR1));
                using (var xmlWriter = XmlWriter.Create(testStream, new XmlWriterSettings()
                {
                    Indent = true,
                }))
                {
                    Flatten();
                    test.Serialize(xmlWriter, this);
                    Unflatten();
                } 
            }
        }

        public static FXR1 ReadFromXml(string xmlPath)
        {
            using (var testStream = File.OpenRead(xmlPath))
            {
                var test = new XmlSerializer(typeof(FXR1));
                using (var xmlReader = XmlReader.Create(testStream))
                {
                    var fxr = (FXR1)test.Deserialize(xmlReader);
                    fxr.Unflatten();
                    return fxr;
                }
            }
        }



        [XmlIgnore]
        internal static bool FlattenStates = true;
        [XmlIgnore]
        internal static bool FlattenFXNodePointers = false;
        [XmlIgnore]
        internal static bool FlattenTransitions = true;
        [XmlIgnore]
        internal static bool FlattenFXActions = false;
        [XmlIgnore]
        internal static bool FlattenFXNodes = false;
        [XmlIgnore]
        internal static bool FlattenFXContainers = false;
        [XmlIgnore]
        internal static bool FlattenFXActionDatas = false;
        [XmlIgnore]
        internal static bool FlattenModifiers = false;

        public List<FXState> AllStates { get; set; } = new List<FXState>();
        public List<FXNodePointer> AllFXNodePointers { get; set; } = new List<FXNodePointer>();
        public List<FXTransition> AllTransitions { get; set; } = new List<FXTransition>();
        public List<FXAction> AllFXActions { get; set; } = new List<FXAction>();
        public List<FXNode> AllFXNodes { get; set; } = new List<FXNode>();
        public List<FXContainer> AllFXContainers { get; set; } = new List<FXContainer>();
        public List<FXActionData> AllFXActionDatas { get; set; } = new List<FXActionData>();
        public List<FXModifier> AllModifiers { get; set; } = new List<FXModifier>();



        public bool ShouldSerializeAllStates() => FlattenStates;
        public bool ShouldSerializeAllFXNodePointers() => FlattenFXNodePointers;
        public bool ShouldSerializeAllTransitions() => FlattenTransitions;
        public bool ShouldSerializeAllFXActions() => FlattenFXActions;
        public bool ShouldSerializeAllFXNodes() => FlattenFXNodes;
        public bool ShouldSerializeAllFXContainers() => FlattenFXContainers;
        public bool ShouldSerializeAllFXActionDatas() => FlattenFXActionDatas;
        public bool ShouldSerializeAllModifiers() => FlattenModifiers;


        internal FXState GetState(string xid) => AllStates.FirstOrDefault(x => x.XID == xid);
        internal FXState DereferenceState(FXState v)
        {
            if (!FlattenStates)
                return v;

            if (v is FXStateRef asRef)
                return GetState(asRef.ReferenceXID);
            else
                return v;
        }
        internal FXState ReferenceState(FXState v)
        {
            if (!FlattenStates)
                return v;

            if (v is FXStateRef asRef)
                return v;
            else
                return new FXStateRef(v);
        }


        internal FXNodePointer GetFXNodePointer(string xid) => AllFXNodePointers.FirstOrDefault(x => x.XID == xid);
        internal FXNodePointer DereferenceFXNodePointer(FXNodePointer v)
        {
            if (!FlattenFXNodePointers)
                return v;

            if (v is FXNodePointerRef asRef)
                return GetFXNodePointer(asRef.ReferenceXID);
            else
                return v;
        }
        internal FXNodePointer ReferenceFXNodePointer(FXNodePointer v)
        {
            if (!FlattenFXNodePointers)
                return v;

            if (v is FXNodePointerRef asRef)
                return v;
            else
                return new FXNodePointerRef(v);
        }


        internal FXTransition GetTransition(string xid) => AllTransitions.FirstOrDefault(x => x.XID == xid);
        internal FXTransition DereferenceTransition(FXTransition v)
        {
            if (!FlattenTransitions)
                return v;

            if (v is FXTransitionRef asRef)
                return GetTransition(asRef.ReferenceXID);
            else
                return v;
        }

        internal FXTransition ReferenceTransition(FXTransition v)
        {
            if (!FlattenTransitions)
                return v;

            if (v is FXTransitionRef asRef)
                return v;
            else
                return new FXTransitionRef(v);
        }


        internal FXAction GetFXAction(string xid) => AllFXActions.FirstOrDefault(x => x.XID == xid);
        internal FXAction DereferenceFXAction(FXAction v)
        {
            if (!FlattenFXActions)
                return v;

            if (v is FXActionRef asRef)
                return GetFXAction(asRef.ReferenceXID);
            else
                return v;
        }
        internal FXAction ReferenceFXAction(FXAction v)
        {
            if (!FlattenFXActions)
                return v;

            if (v is FXActionRef asRef)
                return v;
            else
                return new FXActionRef(v);
        }


        internal FXNode GetFXNode(string xid) => AllFXNodes.FirstOrDefault(x => x.XID == xid);
        internal FXNode DereferenceFXNode(FXNode v)
        {
            if (!FlattenFXNodes)
                return v;

            if (v is FXNode.FXNodeRef asRef)
                return GetFXNode(asRef.ReferenceXID);
            else
                return v;
        }
        internal FXNode ReferenceFXNode(FXNode v)
        {
            if (!FlattenFXNodes)
                return v;

            if (v is FXNode.FXNodeRef asRef)
                return v;
            else
                return new FXNode.FXNodeRef(v);
        }


        internal FXContainer GetFXContainer(string xid) => AllFXContainers.FirstOrDefault(x => x.XID == xid);
        internal FXContainer DereferenceFXContainer(FXContainer v)
        {
            if (!FlattenFXContainers)
                return v;

            if (v is FXContainerRef asRef)
                return GetFXContainer(asRef.ReferenceXID);
            else
                return v;
        }
        internal FXContainer ReferenceFXContainer(FXContainer v)
        {
            if (!FlattenFXContainers)
                return v;

            if (v is FXContainerRef asRef)
                return v;
            else
                return new FXContainerRef(v);
        }


        internal FXActionData GetActionData(string xid) => AllFXActionDatas.FirstOrDefault(x => x.XID == xid);
        internal FXActionData DereferenceFXActionData(FXActionData v)
        {
            if (!FlattenFXActionDatas)
                return v;

            if (v is ActionDataRef asRef)
                return GetActionData(asRef.ReferenceXID);
            else
                return v;
        }
        internal FXActionData ReferenceFXActionData(FXActionData v)
        {
            if (!FlattenFXActionDatas)
                return v;

            if (v is ActionDataRef asRef)
                return v;
            else
                return new ActionDataRef(v);
        }


        internal FXModifier GetModifier(string xid) => AllModifiers.FirstOrDefault(x => x.XID == xid);
        internal FXModifier DereferenceModifier(FXModifier v)
        {
            if (!FlattenModifiers)
                return v;

            if (v is FXModifierRef asRef)
                return GetModifier(asRef.ReferenceXID);
            else
                return v;
        }
        internal FXModifier ReferenceModifier(FXModifier v)
        {
            if (!FlattenModifiers)
                return v;

            if (v is FXModifierRef asRef)
                return v;
            else
                return new FXModifierRef(v);
        }

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

            AllFXActionDatas.Clear();
            AllModifiers.Clear();
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
                //switch (kvp.Value)
                //{
                //    case FXState state: Register("FXState", kvp.Key, AllStates, state); break;
                //    case FXTransition transition: Register("FXTransition", kvp.Key, AllTransitions, transition); break;
                //}

                if (kvp.Value is FXActionData asActionData)
                {
                    Register("ActionData", kvp.Key, AllFXActionDatas, asActionData);
                }
                else if (kvp.Value is FXModifier asModifier)
                {
                    Register("Modifier", kvp.Key, AllModifiers, asModifier);
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
                    Register("FXTransition", kvp.Key, AllTransitions, asTransition);
                }
                else if (kvp.Value is FXState asState)
                {
                    Register("FXState", kvp.Key, AllStates, asState);
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
        }

        public void Flatten()
        {
            foreach (var x in AllFXActionDatas)
                x.ToXIDs(this);
            foreach (var x in AllModifiers)
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
            foreach (var x in AllFXActionDatas)
                x.FromXIDs(this);
            foreach (var x in AllModifiers)
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
            
            // Write RootFXNode and everything else.
            env.FinishRecursiveWrite();

            bw.FillFXR1Varint("OffsetToTable", (int)bw.Position);
            env.WritePointerTable("TablePointerCount");
            env.WriteFXNodeTable("TableFXNodeCount");
        }

    }
}
