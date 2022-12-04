using ProgramTree;

namespace SimpleLang.Visitors
{
    // базовая логика обхода без действий
    // Если нужны действия или другая логика обхода, то соответствующие методы надо переопределять
    // При переопределении методов для задания действий необходимо не забывать обходить подузлы
    class AutoVisitor: Visitor
    {
        protected SimpleParser.SymbolTable top = null;
        public override void VisitBinOpNode(BinOpNode binop) 
        {
            binop.Left.Invite(this);
            binop.Right.Invite(this);
        }
        public override void VisitAssignNode(AssignNode a) 
        {
            // для каких-то визиторов порядок может быть обратный - вначале обойти выражение, потом - идентификатор
            a.Id.Invite(this);
            a.Expr.Invite(this);
        }
        public override void VisitCycleNode(CycleNode c) 
        {
            c.Expr.Invite(this);
            c.Stat.Invite(this);
        }
        public override void VisitBlockNode(BlockNode bl) 
        {
            top = bl.table;

            if (bl.StList.Count > 0 && bl.StList != null)
            {
                foreach (var st in bl.StList)
                    st.Invite(this);
            }
            top = top.prev;

        }



        public override void VisitIfNode(IfNode w)
        {
            w.expr.Invite(this);
            w.then.Invite(this);
            if (w._else != null) {
                w._else.Invite(this);
            }
        }

        public override void VisitIdNode(IdNode id){}

        public override void VisitIntNumNode(IntNumNode num){}

        public override void VisitRealNumNode(RealNumNode num) {}

        public override void VisitEmptyNode(EmptyNode w){}

        public override void VisitVarNode(VarNode vn)
        {
            vn.name.Invite(this);
            if (vn.valExpr != null) vn.valExpr.Invite(this);
        }

        public override void VisitFuncNode(FuncNode fn)
        {
            fn.name.Invite(this);
            foreach (var p in fn._params) p.Invite(this); 
            fn.returnTypeId.Invite(this);
            fn.body.Invite(this);
            
        }

        public override void VisitWriteNode(WriteNode wr)
        {
            wr.Expr.Invite(this);
        }

        public override void VisitParamNode(ParamNode pn)
        {
            pn.name.Invite(this);
            pn.typeId.Invite(this);
        }

        public override void VisitFuncBodyNode(FuncBodyNode fbn)
        {
            VisitBlockNode(fbn);
        }

        public override void VisitFuncCallNode(FuncCallNode fn)
        {
            fn.id.Invite(this);
            foreach (var arg in fn.args)
            {
                arg.Invite(this);
            }
        }

        public override void VisitReturnNode(ReturnNode returnNode)
        {
            returnNode.retExpr?.Invite(this);
        }

        public override void VisitFuncCallStmntNode(FuncCallStmntNode funcCallStmntNode)
        {
            funcCallStmntNode.call.Invite(this);
        }

        public override void VisitBoolNode(BoolNode boolNode)
        {
            
        }

        public override void VisitBoolBinOpNode(BoolBinOpNode boolBinOpNode)
        {
            boolBinOpNode.Left.Invite(this);
            boolBinOpNode.Right.Invite(this);
        }

        public override void VisitLogicBinOpNode(LogicBinOpNode binop)
        {
            binop.Left.Invite(this);
            binop.Right.Invite(this);
        }

        public override void VisitUnaryOpNode(UnaryOpNode unaryOp)
        {
            unaryOp.expr.Invite(this);
        }

        public override void VisitWhileNode(WhileNode whileNode)
        {
            whileNode.Expr.Invite(this);
            whileNode.stmnt.Invite(this);
        }
    }
}
