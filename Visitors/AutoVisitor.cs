using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    // базовая логика обхода без действий
    // Если нужны действия или другая логика обхода, то соответствующие методы надо переопределять
    // При переопределении методов для задания действий необходимо не забывать обходить подузлы
    class AutoVisitor: Visitor
    {
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
            foreach (var st in bl.StList)
                st.Invite(this);
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
            throw new NotImplementedException();
        }

        public override void VisitWriteNode(WriteNode wr)
        {
            wr.Expr.Invite(this);
        }
    }
}
