using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;
using System.Reflection.Emit;
using SimpleParser;
using System.Dynamic;

namespace SimpleLang.Visitors
{
    class GenCodeVisitor: Visitor
    {
        private Dictionary<string, LocalBuilder> vars = new Dictionary<string, LocalBuilder>();
        private GenCodeCreator genc;

        public GenCodeVisitor()
        {
            genc = new GenCodeCreator();
        }
        public override void VisitIdNode(IdNode id) 
        {
            // Этот Visit не вызывается если переменная стоит слева от оператора присваивания !
            // Т.е. он вызывается только если id находится в выражении, а значит, мы просто кладем его значение на стек!
            genc.Emit(OpCodes.Ldloc, vars[id.Name]);
        }
        public override void VisitRealNumNode(RealNumNode num)
        {
            genc.Emit(OpCodes.Ldc_R8, num.Value);
            
        }

        public override void VisitEmptyNode(EmptyNode w)
        {
            
        }

        public override void VisitVarNode(VarNode vn)
        {   TYPE id_type = vn.name.type;

            if (vn.valExpr != null)
            {
                
                vn.valExpr.Invite(this);
                TYPE expr_type = vn.valExpr.type;
                if (id_type == TYPE.INT && expr_type == TYPE.DOUBLE)
                    throw new SyntaxException("Невозможно привести double к  int");
                else if (id_type != TYPE.INT)
                {   
                    
                    if (expr_type == TYPE.INT) genc.Emit(OpCodes.Conv_R8);
                    vars[vn.name.Name] = genc.DeclareLocal(typeof(double));
                }
                
                else vars[vn.name.Name] = genc.DeclareLocal(typeof(int));
                genc.Emit(OpCodes.Stloc, vars[vn.name.Name]);
                
            }
            else if(id_type == TYPE.INT) 
                vars[vn.name.Name] = genc.DeclareLocal(typeof(int));
            else vars[vn.name.Name] = genc.DeclareLocal(typeof(double));
        }

        public override void VisitFuncNode(FuncNode fn)
        {
            throw new NotImplementedException();
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
                case '+':
                    genc.Emit(OpCodes.Add);
                    break;
                case '-':
                    genc.Emit(OpCodes.Sub);
                    break;
                case '*':
                    genc.Emit(OpCodes.Mul);
                    break;
                case '/':
                    genc.Emit(OpCodes.Div);
                    break;
                case '\\':
                    genc.Emit(OpCodes.Div);
                    break;
                case '%':
                    genc.Emit(OpCodes.Rem);
                    break;

            }
        }
        public override void VisitAssignNode(AssignNode a) 
        {
            TYPE id_type = a.Id.type;
            a.Expr.Invite(this);
            TYPE expr_type = a.Expr.type;
            if (id_type == TYPE.INT && expr_type == TYPE.DOUBLE)
                throw new SyntaxException("Невозможно привести double к  int");
            else if (id_type != TYPE.INT)
            {
                if (expr_type == TYPE.INT) genc.Emit(OpCodes.Conv_R8);
            }
            
        
            genc.Emit(OpCodes.Stloc, vars[a.Id.Name]);
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
        public override void VisitBlockNode(BlockNode bl) 
        {
            foreach (var st in bl.StList)
                st.Invite(this);
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
            if (w._else != null) {
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

        public override void VisitWriteNode(WriteNode wr)
        {
            wr.Expr.Invite(this);
            genc.EmitWriteLine(wr.Expr.type);
        }
    }
}
