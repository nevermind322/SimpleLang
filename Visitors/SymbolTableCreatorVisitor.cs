using ProgramTree;

namespace SimpleLang.Visitors
{

    class SymbolTableCreatorVisitor : AutoVisitor
    {
        public SimpleParser.SymbolTable currentTable = new SimpleParser.SymbolTable(null);

        public override void VisitBlockNode(BlockNode bl)
        {
            var st = new SimpleParser.SymbolTable(currentTable);
            currentTable = st;
            bl.table = st;
            bl.Invite(this);
            currentTable = st.prev;
        }

        public override void VisitVarNode(VarNode vn)
        {
            currentTable.Add(vn);
        }

        public override void VisitFuncNode(FuncNode fn)
        {
            currentTable.Add(fn);
        }
    }

}