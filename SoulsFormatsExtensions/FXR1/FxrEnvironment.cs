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

            public ASTPool1 GetASTPool1(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is ASTPool1 v)
                        return v;
                    else 
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTPool1.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
                    return newVal;
                }
            }

            public ASTPool2 GetASTPool2(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (stuff.ContainsKey(offset))
                {
                    if (stuff[offset] is ASTPool2 v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTPool2.Read(br, this);
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
                    br.StepIn(offset);
                    var newVal = AST.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
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
                    br.StepIn(offset);
                    var newVal = FlowNode.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
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
                    br.StepIn(offset);
                    var newVal = FlowEdge.Read(br, this);
                    br.StepOut();

                    stuff.Add(offset, newVal);
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
                    br.StepOut();

                    stuff.Add(offset, newVal);
                    return newVal;
                }
            }

        }
    }
}
