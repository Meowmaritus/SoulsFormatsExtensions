using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
        public class FxrEnvironment
        {
            private Dictionary<long, object> stuff = new Dictionary<long, object>();

            internal List<FlowEdge> masterFlowEdgeList = new List<FlowEdge>();
            internal List<FlowNode> masterFlowNodeList = new List<FlowNode>();
            internal List<FlowAction> masterFlowActionList = new List<FlowAction>();

            public void CalculateAllIndices()
            {
                foreach (var edge in masterFlowEdgeList)
                    edge.CalculateIndices(this);

                foreach (var node in masterFlowNodeList)
                    node.CalculateIndices(this);
            }

            public int GetFlowEdgeIndex(FlowEdge edge)
            {
                return masterFlowEdgeList.IndexOf(edge);
            }

            public int GetFlowNodeIndex(FlowNode node)
            {
                return masterFlowNodeList.IndexOf(node);
            }

            public int GetFlowActionIndex(FlowAction action)
            {
                return masterFlowActionList.IndexOf(action);
            }

            private void RegisterFlowNode(FlowNode node)
            {
                if (!masterFlowNodeList.Contains(node))
                    masterFlowNodeList.Add(node);
            }

            private void RegisterFlowEdge(FlowEdge edge)
            {
                if (!masterFlowEdgeList.Contains(edge))
                    masterFlowEdgeList.Add(edge);
            }

            private void RegisterFlowAction(FlowAction action)
            {
                if (!masterFlowActionList.Contains(action))
                    masterFlowActionList.Add(action);
            }

            public ASTFunction GetASTFunction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is ASTFunction v)
                        return v;
                    else 
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTFunction.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
                    return newVal;
                }
            }

            public ASTAction GetASTAction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is ASTAction v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTAction.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
                    return newVal;
                }
            }

            public ASTPool3 GetASTPool3(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is ASTPool3 v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTPool3.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
                    return newVal;
                }
            }

            public Function GetFunction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is Function v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = Function.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
                    return newVal;
                }
            }

            public AST GetAST(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is AST v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new AST();
                    stuff.Add(offset, newVal);
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();
                    
                    return newVal;
                }
            }

            public FlowNode GetFlowNode(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is FlowNode v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new FlowNode();
                    RegisterFlowNode(newVal);
                    stuff.Add(offset, newVal);
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();
                    
                    return newVal;
                }
            }

            public FlowEdge GetFlowEdge(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is FlowEdge v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new FlowEdge();
                    RegisterFlowEdge(newVal);
                    stuff.Add(offset, newVal);
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();

                    return newVal;
                }
            }

            public FlowAction GetFlowAction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is FlowAction v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = FlowAction.Read(br, this);
                    RegisterFlowAction(newVal);
                    br.StepOut();

                    stuff.Add(offset, newVal);
                    return newVal;
                }
            }

        }
    }
}
