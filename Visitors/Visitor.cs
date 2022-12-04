using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public abstract class Visitor
    {

        public abstract void VisitWriteNode(WriteNode wr);

        public abstract void VisitIdNode(IdNode id);
        public abstract void VisitIntNumNode(IntNumNode num);
        public abstract void VisitRealNumNode(RealNumNode num);
        public abstract void VisitBinOpNode(BinOpNode binop);
        
        public abstract void VisitAssignNode(AssignNode a);
        public abstract void VisitCycleNode(CycleNode c) ;
        public abstract void VisitBlockNode(BlockNode bl);
    
        public abstract void VisitEmptyNode(EmptyNode w);
        public abstract void VisitIfNode(IfNode w);
        public abstract void VisitVarNode(VarNode vn);
        public abstract void VisitFuncNode(FuncNode fn);

        public abstract void VisitParamNode(ParamNode pn);
                
        public abstract void VisitFuncBodyNode(FuncBodyNode fbn);

        public abstract void VisitFuncCallNode(FuncCallNode fn);
        public abstract void VisitReturnNode(ReturnNode returnNode);

        public abstract void VisitFuncCallStmntNode(FuncCallStmntNode funcCallStmntNode);

        public abstract void VisitBoolNode(BoolNode boolNode);

        public abstract void VisitBoolBinOpNode(BoolBinOpNode boolBinOpNode);
        public  abstract void VisitLogicBinOpNode(LogicBinOpNode logicBinOpNode);
        public abstract void VisitUnaryOpNode(UnaryOpNode unaryOp);
        public  abstract void VisitWhileNode(WhileNode whileNode);
    }
}
