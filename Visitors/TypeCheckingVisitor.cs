﻿using ProgramTree;
using SimpleParser;


namespace SimpleLang.Visitors
{
    class TypeCheckingVisitor : AutoVisitor
    {
        public override void VisitIdNode(IdNode id)
        {
            SymbolTable.SymbolInfo si = SymbolTable.Get(id.Name);
            if (si.kind != SymbolTable.SymbolInfo.Kind.FUNCTION)
            {
                var var_si = si as SymbolTable.VarInfo;
                id.type = var_si.type;
                return;
            }
            throw new SyntaxException("{id.name} ссылается на функцию");
        }

        public override void VisitBinOpNode(BinOpNode binop)
        {
            binop.Left.Invite(this);
            binop.Right.Invite(this);
            TYPE r_type = binop.Left.type;
            TYPE l_type = binop.Left.type;
            if (r_type != l_type) binop.type = TYPE.DOUBLE;
            else binop.type = l_type;

            if(binop.Op == '%' || binop.Op == '\\')
                if (binop.type == TYPE.DOUBLE) throw new SyntaxException("Неверный тип");
        }

        public override void VisitWriteNode(WriteNode wr)
        {
            wr.Expr.Invite(this);
        }
    }
}
