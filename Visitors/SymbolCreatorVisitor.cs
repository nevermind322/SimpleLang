using ProgramTree;
using SimpleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLang.Visitors
{
     class SymbolCreatorVisitor : AutoVisitor
    {
        public override void VisitFuncNode(FuncNode fn)
        {
            SymbolTableStack.addInfo(fn);   
        }

        public override void VisitParamNode(ParamNode pn)
        {
            SymbolTableStack.addInfo(pn);
        }

        public override void VisitVarNode(VarNode vn)
        {
            SymbolTableStack.addInfo(vn);
        }
    }
}
