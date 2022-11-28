using ProgramTree;
using System;

namespace SimpleLang.Visitors
{

    class SymbolTableCreatorVisitor : AutoVisitor
    {
        

        public override void VisitBlockNode(BlockNode bl)
        {
            var st = new SimpleParser.SymbolTable(top);
            bl.table = st;
            base.VisitBlockNode(bl);
            
        }

        public override void VisitVarNode(VarNode vn)
        {
            top.Add(vn);
        }

        public override void VisitFuncNode(FuncNode fn)
        {

            fn.body.Invite(this);
            top = fn.body.table;
            int i = 0;
            foreach (var param in fn._params) {
                Console.WriteLine(param.name.Name);
                param.pos = i++; 
                param.Invite(this);
            }
            top = top.prev;
            top.Add(fn);
        }

        public override void VisitParamNode(ParamNode pn)
        {
            Console.WriteLine(pn.name.Name);    
            top.Add(pn);
        }
                
    }

}