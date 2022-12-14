using System;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class PrettyPrintVisitor: Visitor
    {
        public string Text = "";
        private int Indent = 0;

        private string IndentStr()
        {
            return new string(' ', Indent);
        }
        private void IndentPlus()
        {
            Indent += 2;
        }
        private void IndentMinus()
        {
            Indent -= 2;
        }
        public override void VisitIdNode(IdNode id) 
        {
            Text += id.Name;
        }
        public override void VisitIntNumNode(IntNumNode num) 
        {
            Text += num.Num.ToString();
        }

        public override void VisitRealNumNode(RealNumNode num)
        {
            Text += num.Value.ToString();
        }

        public override void VisitBinOpNode(BinOpNode binop) 
        {
            Text += "(";
            binop.Left.Invite(this);
            Text += " " + binop.Op + " ";
            binop.Right.Invite(this);
            Text += ")";
        }
        public override void VisitAssignNode(AssignNode a) 
        {
            Text += IndentStr();
            a.Id.Invite(this);
            Text += " = ";
            a.Expr.Invite(this);
        }
        public override void VisitCycleNode(CycleNode c) 
        {
            Text += IndentStr() + "cycle ";
            c.Expr.Invite(this);
            Text += Environment.NewLine;
            c.Stat.Invite(this);
        }
        public override void VisitBlockNode(BlockNode bl) 
        {
            Text += IndentStr() + "begin" + Environment.NewLine;
            IndentPlus();

            var Count = bl.StList.Count;

            if (Count>0)
                bl.StList[0].Invite(this);
            for (var i = 1; i < Count; i++)
            {
                Text += ';';
                if (!(bl.StList[i] is EmptyNode))
                    Text += Environment.NewLine;
                bl.StList[i].Invite(this);
            }
            IndentMinus();
            Text += Environment.NewLine + IndentStr() + "end";
        }

        

        public override void VisitIfNode(IfNode w)
        {
            Text += IndentStr() + "if(";
            w.expr.Invite(this);
            Text += ") then \n";
            IndentPlus();
            
            w.then.Invite(this);
            IndentMinus();
            if (w._else != null) {
                Text += "\n" + IndentStr() + "else \n";
                IndentPlus();
                w._else.Invite(this);
                IndentMinus();
                    }


        }

        public override void VisitEmptyNode(EmptyNode w)
        {
            
        }

        public override void VisitVarNode(VarNode vn)
        {
            Text += IndentStr() + vn.typeId.Name + " " + vn.name.Name;
            if (vn.valExpr != null)
            {
                Text += " = ";
                vn.valExpr.Invite(this);
            }
        }

        public override void VisitFuncNode(FuncNode fn)
        {
            Text += IndentStr() + "fun " + fn.name.Name + "(";
            foreach (var param in fn._params) { 
                param.Invite(this);
                Text += ", ";    
            }
            
            Text += ") : " + fn.returnTypeId.Name + "\n";
            fn.body.Invite(this);

        }

        public override void VisitWriteNode(WriteNode wr)
        {
            Text += IndentStr() + "Write(";
            wr.Expr.Invite(this);
            Text += ")";
        }

        public override void VisitParamNode(ParamNode pn)
        {
            Text += pn.typeId.Name + " " + pn.name.Name;
        }

        public override void VisitFuncBodyNode(FuncBodyNode fbn)
        {
            VisitBlockNode(fbn);
        }

        public override void VisitFuncCallNode(FuncCallNode fn)
        {
            Text += IndentStr();
            fn.id.Invite(this);
            Text +=  "(";
            foreach (var arg in fn.args) {
                arg.Invite(this);
                Text += ", ";
            }
            Text += ")";
        }

        public override void VisitReturnNode(ReturnNode returnNode)
        {
            Text += IndentStr() + "return ";
            returnNode.retExpr?.Invite(this);
        }

        public override void VisitFuncCallStmntNode(FuncCallStmntNode funcCallStmntNode)
        {
            funcCallStmntNode.call.Invite(this);    
        }

        public override void VisitBoolNode(BoolNode boolNode)
        {
            if (boolNode.value) Text += "true";
            else Text += "false";
        }

        public override void VisitBoolBinOpNode(BoolBinOpNode boolBinOpNode)
        {
            VisitBinOpNode(boolBinOpNode);
        }

        public override void VisitLogicBinOpNode(LogicBinOpNode logicBinOpNode)
        {
            VisitBinOpNode(logicBinOpNode);
        }

        public override void VisitUnaryOpNode(UnaryOpNode unaryOp)
        {
            Text += unaryOp.op;
            unaryOp.expr.Invite(this);
        }

        public override void VisitWhileNode(WhileNode whileNode)
        {
            Text += "while(";
            whileNode.Expr.Invite(this);
            Text += ")";
            whileNode.stmnt.Invite(this);
        }
    }
}
