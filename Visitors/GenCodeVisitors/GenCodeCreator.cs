using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace SimpleLang.Visitors
{
    class GenCodeCreator
    {
        private DynamicMethod dyn;
        private ILGenerator gen;
        private bool write_commands = true;
        private static MethodInfo writeLineInt = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) });
        private static MethodInfo writeLineDouble = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(double) });
        private static MethodInfo writeLineString = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });

        public List<string> commands = new List<string>();
        private List<Type> @params;
        private Type ret;

        public GenCodeCreator()
        {
            dyn = new DynamicMethod("My", null, null, typeof(void));
            gen = dyn.GetILGenerator();
        }

        public GenCodeCreator(List<Type> @params, Type ret)
        {
            this.@params = @params;
            this.ret = ret;
            if (@params != null)
                dyn = new DynamicMethod("Myf", ret, @params.ToArray(), typeof(void));
            else dyn = new DynamicMethod("Myf", ret, null, typeof(void));
            gen = dyn.GetILGenerator();
        }

        public void Emit(OpCode op, DynamicMethod dyn)
        {
            gen.Emit(op, dyn);
        }

        public void EmitEq(OpCode op)
        {
            Label @true = gen.DefineLabel();
            Label resume = gen.DefineLabel();
            gen.Emit(op, @true);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Br, resume);
            MarkLabel(@true);
            gen.Emit(OpCodes.Ldc_I4_1);
            MarkLabel(resume);
            

        }

        public void Emit(OpCode op)
        {
            gen.Emit(op);
            if (write_commands)
                commands.Add(op.ToString());
        }

        public void Emit(OpCode op, int num)
        {
            gen.Emit(op, num);
            if (write_commands)
                commands.Add(op.ToString() + " " + num);
        }

        public void Emit(OpCode op, double num)
        {
            gen.Emit(op, num);
            if (write_commands)
                commands.Add(op.ToString() + " " + num);
        }

        public void Emit(OpCode op, LocalBuilder lb)
        {
            gen.Emit(op, lb);
            if (write_commands)
                commands.Add(op.ToString() + " var" + lb.LocalIndex);
        }



        public void Emit(OpCode op, Label l)
        {
            gen.Emit(op, l);
            if (write_commands)
                commands.Add(op.ToString() + " Label" + l.GetHashCode());
        }

        public LocalBuilder DeclareLocal(Type t)
        {
            var lb = gen.DeclareLocal(t);
            if (write_commands)
                commands.Add("DeclareLocal " + "var" + lb.LocalIndex + ": " + t);
            return lb;
        }

        public Label DefineLabel()
        {
            var l = gen.DefineLabel();
            if (write_commands)
                commands.Add("DefineLabel" + " Label" + l.GetHashCode());

            return l;
        }

        public void MarkLabel(Label l)
        {
            gen.MarkLabel(l);
            if (write_commands)
                commands.Add("MarkLabel" + " Label" + l.GetHashCode());
        }

        public void EmitWriteLine(SimpleParser.TYPE t)
        {
            
            switch (t)
            {
                case (SimpleParser.TYPE.INT):
                    {
                        gen.Emit(OpCodes.Call, writeLineInt);
                        break;
                    }
                case (SimpleParser.TYPE.BOOL):
                    {
                        Label @true = gen.DefineLabel();
                        Label writeLine = gen.DefineLabel();
                        gen.Emit(OpCodes.Brtrue, @true);
                        gen.Emit(OpCodes.Ldstr, "false");
                        gen.Emit(OpCodes.Br, writeLine);
                        MarkLabel(@true);
                        gen.Emit(OpCodes.Ldstr, "true");
                        MarkLabel(writeLine);
                        gen.Emit(OpCodes.Call, writeLineString);
                        break;
                    }
                case (SimpleParser.TYPE.DOUBLE):
                    {
                        gen.Emit(OpCodes.Call, writeLineDouble);
                        break;
                    }
            }

            if (write_commands)
                commands.Add("WriteLine");
        }

        public void EndProgram()
        {
            gen.Emit(OpCodes.Ret);
        }

        public void RunProgram()
        {
            dyn.Invoke(null, null);
            /*var resolver = typeof(DynamicMethod).GetField("m_resolver", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dyn);
            if (resolver == null) throw new ArgumentException("The dynamic method's IL has not been finalized.");
            var  res = (byte[])resolver.GetType().GetField("m_code", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(resolver);
            foreach (var b in res) Console.WriteLine(b); */
        }

        public void WriteCommandsOn()
        {
            write_commands = true;
        }

        public void WriteCommandsOff()
        {
            write_commands = false;
        }

        internal DynamicMethod getDynamicMethod()
        {
            return dyn;
        }
    }
}
