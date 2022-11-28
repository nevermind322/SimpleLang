using ProgramTree;
using SimpleParser;
using System;

namespace SimpleLang.Visitors
{
    class TypeCheckingVisitor : AutoVisitor
    {
        public override void VisitIdNode(IdNode id)
        {
            SymbolTable.SymbolInfo si = top.find(id.Name);
            switch (si.kind)
            {
                case (SymbolTable.SymbolInfo.Kind.VAR):
                    {
                        var var_si = si as SymbolTable.VarInfo;
                        if (var_si != null)
                            id.type = var_si.type;
                        else throw new Exception();
                        return;
                    }
                case (SymbolTable.SymbolInfo.Kind.PARAM):
                    {
                        var var_si = si as SymbolTable.ParamInfo;
                        if (var_si != null)
                            id.type = var_si.type;
                        else throw new Exception();
                        return;
                    }
            }
            throw new SyntaxException(id.Name + " ссылается на функцию");
        }
        public override void VisitFuncNode(FuncNode fn)
        {
            fn.body.Invite(this);
        }

        public override void VisitFuncCallNode(FuncCallNode fn)
        {
            SymbolTable.SymbolInfo si = top.find(fn.id.Name);


            if (si.kind == SymbolTable.SymbolInfo.Kind.FUNCTION)
            {
                var FuncInfo = si as SymbolTable.FunctionInfo;
                fn.type = FuncInfo.type.return_type;

                if (FuncInfo.type.params_type.Count != fn.args.Count)
                    if (FuncInfo.type.params_type.Count < fn.args.Count)
                        throw new SyntaxException("слишком много аргументов");
                    else
                        throw new SyntaxException("слишком мало аргументов");
                int i = 0;
                TYPE arg_type;
                TYPE param_type;
                TYPE result_type;
                foreach (var arg in fn.args)
                {
                    arg.Invite(this);
                    arg_type = arg.type;
                    param_type = FuncInfo.type.params_type[i++];
                    result_type = ParserHelper.widen(arg_type, param_type);
                    if (result_type == TYPE.VOID) throw new SyntaxException("аргумент не может быть void");
                    if (ParserHelper.canBeWiden(from: result_type, to: param_type))
                        arg.type = param_type;
                    else
                        throw new SyntaxException("нельзя привести " + arg_type + " к " + param_type);
                }
                return;
            }
            throw new SyntaxException(fn.id.Name + " не функция");
        }
        public override void VisitBinOpNode(BinOpNode binop)
        {
            binop.Left.Invite(this); 
            binop.Right.Invite(this);
            TYPE r_type = binop.Right.type;
            TYPE l_type = binop.Left.type;
            if (binop.Left.type == TYPE.BOOL || binop.Right.type == TYPE.BOOL)
                throw new SyntaxException("применение " + binop.Op + " к bool недопустимо");
            if (r_type != l_type) binop.type = TYPE.DOUBLE;
            else binop.type = l_type;

            if (binop.Op == "%" || binop.Op == "//")
            {
                if (binop.type == TYPE.DOUBLE) throw new SyntaxException("Неверный тип");
            }
        }

        public override void VisitBoolBinOpNode(BoolBinOpNode binop)
        {
            binop.Left.Invite(this);
            binop.Right.Invite(this);
            if (binop.Left.type == TYPE.VOID || binop.Right.type == TYPE.VOID)
                throw new SyntaxException("нельзя сравнивать с void");
            if (binop.Left.type == TYPE.BOOL || binop.Right.type == TYPE.BOOL)
                throw new SyntaxException("нельзя сравнивать с bool");

        }


        public override void VisitLogicBinOpNode(LogicBinOpNode binop)
        {
            binop.Left.Invite(this);
            binop.Right.Invite(this);
            if (binop.Left.type != TYPE.BOOL || binop.Right.type != TYPE.BOOL)
                throw new SyntaxException("в логическом выражении должен быть bool");
        }
    }
}
