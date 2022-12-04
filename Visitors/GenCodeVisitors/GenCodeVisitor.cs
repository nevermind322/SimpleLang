using System;
using ProgramTree;
using System.Reflection.Emit;
using SimpleParser;
using System.Collections.Generic;


namespace SimpleLang.Visitors
{
    class GenCodeVisitor : AutoVisitor
    {
        private GenCodeCreator genc;

        public GenCodeVisitor()
        {
            genc = new GenCodeCreator();
        }

        public GenCodeVisitor(List<Type> _params, Type ret)
        {
            genc = new GenCodeCreator(_params, ret);
        }
        public override void VisitIdNode(IdNode id)
        {
            // Этот Visit не вызывается если переменная стоит слева от оператора присваивания !
            // Т.е. он вызывается только если id находится в выражении, а значит, мы просто кладем его значение на стек!

            var si = top.find(id.Name);
            if (si == null) throw new SyntaxException(id.Name + "не объявлена", id.location);

            switch (si.kind)
            {
                case (SymbolTable.SymbolInfo.Kind.VAR):
                    {
                        if (!si.allocated)
                            throw new SyntaxException(si.name + " не может использоваться перед объявлением", id.location);
                        if (!si.initialized)
                            throw new SyntaxException(si.name + " не определена", id.location);
                        var v_si = si as SymbolTable.VarInfo;
                        genc.Emit(OpCodes.Ldloc, v_si.addr);
                        break;
                    }
                case (SymbolTable.SymbolInfo.Kind.FUNCTION):
                    {
                        throw new SemanticException(si.name + " ссылается на функцию!", id.location);
                    }
                case (SymbolTable.SymbolInfo.Kind.PARAM):
                    {
                        var v_si = si as SymbolTable.ParamInfo;
                        genc.Emit(OpCodes.Ldarg, v_si.pos);
                        break;
                    }
            }


        }
        public override void VisitRealNumNode(RealNumNode num)
        {
            genc.Emit(OpCodes.Ldc_R8, num.Value);
        }

        public override void VisitReturnNode(ReturnNode returnNode)
        {
            returnNode.retExpr?.Invite(this);
            genc.Emit(OpCodes.Ret);
        }

        public override void VisitFuncCallStmntNode(FuncCallStmntNode funcCallStmntNode)
        {
            funcCallStmntNode.call.Invite(this);
            var f_si = top.find(funcCallStmntNode.call.id.Name) as SymbolTable.FunctionInfo;
            if (f_si.type.return_type != TYPE.VOID) genc.Emit(OpCodes.Pop);
        }

        public override void VisitFuncCallNode(FuncCallNode fn)
        {
            foreach (var arg in fn.args) arg.Invite(this);
            var f_si = top.find(fn.id.Name) as SymbolTable.FunctionInfo;
            genc.Emit(OpCodes.Call, f_si.dyn);
        }

        public override void VisitBoolNode(BoolNode boolNode)
        {
            if (boolNode.value) genc.Emit(OpCodes.Ldc_I4_1);
            else genc.Emit(OpCodes.Ldc_I4_0);
        }

        public override void VisitVarNode(VarNode vn)
        {
            TYPE id_type = vn.name.type;
            LocalBuilder addr;
            if (id_type == TYPE.INT || id_type == TYPE.BOOL)
                addr = genc.DeclareLocal(typeof(int));
            else
                addr = genc.DeclareLocal(typeof(double));

            top.allocate(vn.name.Name, addr);

            if (vn.valExpr != null)
            {
                vn.valExpr.Invite(this);
                TYPE expr_type = vn.valExpr.type;
                try
                {
                    if (!ParserHelper.canBeWiden(from: expr_type, to: id_type))
                        throw new SemanticException("Невозможно привести " + expr_type
                            + " к " + id_type, vn.location);
                    else if (id_type != TYPE.INT && id_type != TYPE.BOOL)
                    {
                        if (expr_type == TYPE.INT || expr_type == TYPE.BOOL) genc.Emit(OpCodes.Conv_R8);
                    }
                }
                catch (TypeException e)
                    { throw new SemanticException(e.Message, vn.location); }
                genc.Emit(OpCodes.Stloc, addr);
                top.init(vn.name.Name);
            }

        }
        public override void VisitWriteNode(WriteNode wr)
        {
            wr.Expr.Invite(this);
            genc.EmitWriteLine(wr.Expr.type);
        }
        public override void VisitFuncNode(FuncNode fn)
        {
            var f_si = top.find(fn.name.Name) as SymbolTable.FunctionInfo;

            List<Type> types = new();
            foreach (var t in f_si.type.params_type)
            {
                try
                {
                    types.Add(FuncType.getCStype(t));
                }
                catch (TypeException e)
                {
                    throw new SemanticException(e.Message, fn.location);
                }
            }
            if (types.Count == 0) types = null;
            GenCodeVisitor func_code_creator;
            try
            {
                func_code_creator = new GenCodeVisitor(types,
                   FuncType.getCStype(f_si.type.return_type))
                {
                    top = f_si.BodyTable
                };
            }
            catch (TypeException e)
            {
                throw new SemanticException(e.Message, fn.location);
            }
            fn.body.Invite(func_code_creator);
            if (f_si.type.return_type == TYPE.VOID)
                func_code_creator.EndProgram();
            f_si.dyn = func_code_creator.getDynamicMethod();

        }

        private DynamicMethod getDynamicMethod()
        {
            return genc.getDynamicMethod();
        }

        public override void VisitParamNode(ParamNode pn)
        {

        }

        public override void VisitFuncBodyNode(FuncBodyNode fbn)
        {

            foreach (var stmnt in fbn.StList)
            {
                stmnt.Invite(this);
                Console.WriteLine(stmnt);
            }

        }
        public override void VisitIntNumNode(IntNumNode num)
        {
            genc.Emit(OpCodes.Ldc_I4, num.Num);
        }
        public override void VisitBinOpNode(BinOpNode binop)
        {
            TYPE r_type = binop.Right.type;
            TYPE l_type = binop.Left.type;
            binop.Left.Invite(this);
            if (binop.type != l_type)
                genc.Emit(OpCodes.Conv_R8);
            binop.Right.Invite(this);
            if (binop.type != r_type)
                genc.Emit(OpCodes.Conv_R8);

            switch (binop.Op)
            {
                case "+":
                    genc.Emit(OpCodes.Add);
                    break;
                case "-":
                    genc.Emit(OpCodes.Sub);
                    break;
                case "*":
                    genc.Emit(OpCodes.Mul);
                    break;
                case "/":
                    genc.Emit(OpCodes.Div);
                    break;
                case "//":
                    genc.Emit(OpCodes.Div);
                    break;
                case "%":
                    genc.Emit(OpCodes.Rem);
                    break;

            }
        }

        public override void VisitBoolBinOpNode(BoolBinOpNode binop)
        {
            TYPE r_type = binop.Right.type;
            TYPE l_type = binop.Left.type;
            binop.Left.Invite(this);
            if (binop.type != l_type)
                genc.Emit(OpCodes.Conv_R8);
            binop.Right.Invite(this);
            if (binop.type != r_type)
                genc.Emit(OpCodes.Conv_R8);
            switch (binop.Op)
            {
                case ("<"):
                    {
                        genc.EmitEq(OpCodes.Blt);
                        break;
                    }
                case ("<="):
                    {
                        genc.EmitEq(OpCodes.Ble);
                        break;
                    }
                case (">"):
                    {
                        genc.EmitEq(OpCodes.Bgt);
                        break;
                    }
                case (">="):
                    {
                        genc.EmitEq(OpCodes.Bge);
                        break;
                    }
                case ("=="):
                    {
                        genc.EmitEq(OpCodes.Beq);
                        break;
                    }
                case ("!="):
                    {
                        genc.EmitEq(OpCodes.Bne_Un);
                        break;
                    }
                default: throw new SyntaxException("неизвестная операция", binop.location);
            }
        }

        public override void VisitLogicBinOpNode(LogicBinOpNode binop)
        {
            binop.Left.Invite(this);
            binop.Right.Invite(this);
            switch (binop.Op)
            {
                case ("||"):
                    {
                        genc.Emit(OpCodes.Or);
                        break;
                    }
                case ("&&"):
                    {
                        genc.Emit(OpCodes.And);
                        break;
                    }
                default:
                    {
                        throw new SyntaxException("неизвестная операция", binop.location);
                    }
            }
        }
        public override void VisitUnaryOpNode(UnaryOpNode unaryOp)
        {
            unaryOp.expr.Invite(this);

            switch (unaryOp.op)
            {
                case ("!"):
                    {
                        if (unaryOp.expr.type != TYPE.BOOL)
                            throw new SemanticException("Отрицание можно применить только в bool", unaryOp.location);

                        Label l = genc.DefineLabel();
                        Label r = genc.DefineLabel();
                        genc.Emit(OpCodes.Brtrue, l);
                        genc.Emit(OpCodes.Ldc_I4_1);
                        genc.Emit(OpCodes.Br, r);
                        genc.MarkLabel(l);
                        genc.Emit(OpCodes.Ldc_I4_0);
                        genc.MarkLabel(r);

                        break;
                    }
                default: throw new SyntaxException("неизвестная операция", unaryOp.location);

            }
        }
        public override void VisitAssignNode(AssignNode a)
        {
            TYPE id_type = a.Id.type;
            a.Expr.Invite(this);
            TYPE expr_type = a.Expr.type;
            if (id_type == TYPE.INT && expr_type == TYPE.DOUBLE)
                throw new SemanticException("Невозможно привести double к  int", a.location);
            else if (id_type != TYPE.INT)
            {
                if (expr_type == TYPE.INT) genc.Emit(OpCodes.Conv_R8);
            }
            var si = top.find(a.Id.Name);

            switch (si.kind)
            {
                case (SymbolTable.SymbolInfo.Kind.VAR):
                    {
                        var v_si = si as SymbolTable.VarInfo;
                        genc.Emit(OpCodes.Stloc, v_si.addr);
                        top.init(a.Id.Name);
                        break;
                    }
                case (SymbolTable.SymbolInfo.Kind.FUNCTION):
                    {
                        throw new SyntaxException(si.name + " ссылается на функцию!", a.location);
                    }
            }


        }
        public override void VisitCycleNode(CycleNode c)
        {
            var i = genc.DeclareLocal(typeof(int)); // переменная цикла cycle
            c.Expr.Invite(this); // сгенерировать команды, связанные с вычислением количества итераций цикла
            genc.Emit(OpCodes.Stloc, i); // i := кво итераций

            Label startLoop = genc.DefineLabel();
            Label endLoop = genc.DefineLabel();

            genc.MarkLabel(startLoop);

            genc.Emit(OpCodes.Ldloc, i);
            genc.Emit(OpCodes.Ldc_I4_0);
            genc.Emit(OpCodes.Ble, endLoop); // if i<=0 then goto endLoop

            c.Stat.Invite(this); // выполнить тело цикла

            genc.Emit(OpCodes.Ldloc, i); // положить i на стек
            genc.Emit(OpCodes.Ldc_I4_1); // положить 1 на стек
            genc.Emit(OpCodes.Sub);
            genc.Emit(OpCodes.Stloc, i); // i := i - 1;

            genc.Emit(OpCodes.Br, startLoop);

            genc.MarkLabel(endLoop);
        }

        public override void VisitWhileNode(WhileNode whileNode)
        {
            Label start = genc.DefineLabel();
            Label end = genc.DefineLabel();

            genc.MarkLabel(start);
            if (whileNode.Expr.type != TYPE.BOOL)
                throw new SemanticException("ожидалось bool", whileNode.location);
            whileNode.Expr.Invite(this);
            genc.Emit(OpCodes.Brfalse, end);
            
            whileNode.stmnt.Invite(this);
            genc.Emit(OpCodes.Br, start);
            genc.MarkLabel(end);
        }

        public override void VisitIfNode(IfNode w)
        {
            genc.Emit(OpCodes.Ldc_I4, 0);
            w.expr.Invite(this);

            Label then = genc.DefineLabel();
            Label _else = genc.DefineLabel();
            Label endif = genc.DefineLabel();

            genc.Emit(OpCodes.Blt, then);
            if (w._else != null) genc.Emit(OpCodes.Br, _else);
            genc.Emit(OpCodes.Br, endif);
            genc.MarkLabel(then);
            w.then.Invite(this);
            genc.Emit(OpCodes.Br, endif);
            if (w._else != null)
            {
                genc.MarkLabel(_else);
                w._else.Invite(this);
            }
            genc.MarkLabel(endif);
        }

        public void EndProgram()
        {
            genc.EndProgram();
        }

        public void RunProgram()
        {
            genc.RunProgram();
        }

        public void PrintCommands()
        {
            foreach (var s in genc.commands)
                Console.WriteLine(s);
        }
    }
}
