using System.Collections.Generic;
using System;
using ProgramTree;
using System.Linq;
using System.Reflection.Emit;

namespace SimpleParser
{
    public class FuncType
    {
        List<TYPE> params_type = new List<TYPE>();
        public TYPE return_type;

        public FuncType()
        {
        }

        public void addParamType(TYPE t)
        {
            params_type.Add(t);
        }
    }
    public enum TYPE { INT, DOUBLE, VOID }

    public class SymbolTable
    {

        private Dictionary<string, SymbolInfo> table = new Dictionary<string, SymbolInfo>();

        public SymbolTable prev;

        public SymbolTable() { }
        public SymbolTable(SymbolTable prev)
        {
            this.prev = prev;
        }

        public SymbolInfo find(string name)
        {
            SymbolInfo res = null;
            for (var t = this; t != null; t = t.prev)
            {
                if (table.TryGetValue(name, out res)) return res;
            }
            return null;
        }

        public void Add(FuncNode dec)
        {
            addFuncInfo(dec as FuncNode);
        }

        public void Add(VarNode dec)
        {
            addVarInfo(dec);
        }


        private void addFuncInfo(FuncNode fn)
        {
            var ft = new FuncType();
            TYPE t;
            foreach (var param in fn._params)
            {
                switch (param.typeId.Name)
                {
                    
                    case "double":
                        {
                            t = TYPE.DOUBLE;
                            break;
                        }
                    case "int":
                        {
                            t = TYPE.INT;
                            break;
                        }
                    case "void":
                        {
                            throw new SyntaxException("параметр не может быть void");
                        }
                    default:
                        {
                            throw new SyntaxException("Неизвестный тип");
                        }
                }
                ft.addParamType(t);

            }
            switch (fn.returnTypeId.Name)
                {
                    
                    case "double":
                        {
                            t = TYPE.DOUBLE;
                            break;
                        }
                    case "int":
                        {
                            t = TYPE.INT;
                            break;
                        }
                    case "void":
                        {
                           t = TYPE.VOID;
                           break;
                        }
                    default:
                        {
                            throw new SyntaxException("Неизвестный тип");
                        }
                }
            ft.return_type = t;
            FunctionInfo info = new FunctionInfo(ft, fn.name.Name);
            SimpleLang.Visitors.SymbolTableCreatorVisitor table_creator = new  SimpleLang.Visitors.SymbolTableCreatorVisitor();
            foreach (var param in fn._params)
            {
                fn.body.table.Add(param);
            }
            fn.body.Invite(table_creator);
            info.BodyTable = fn.body.table;
            table.Add(fn.name.Name, info);

        }

        private void addVarInfo(VarNode vn)
        {
            TYPE type;
            switch (vn.typeId.Name)
            {
                case "double":
                    {
                        type = TYPE.DOUBLE;
                        break;
                    }
                case "int":
                    {
                        type = TYPE.INT;
                        break;
                    }
                case "void":
                    {
                        throw new SyntaxException("переменная не может быть void");
                    }
                default:
                    {
                        throw new SyntaxException("Неизвестный тип");
                    }
            }
            var info = new VarInfo(type, vn.name.Name);
            table.Add(vn.name.Name, info);
        }

        public class SymbolInfo
        {
            public enum Kind { FUNCTION, VAR }
            public Kind kind;
            public string name;
            public bool allocated = false;
            public bool initialized = false;
            public SymbolInfo(Kind k, string n) { kind = k; name = n; }
        }

        public class FunctionInfo : SymbolInfo
        {

            public FuncType type;
            public SymbolTable BodyTable;
            public FunctionInfo(FuncType t, string n) : base(Kind.FUNCTION, n)
            {
                type = t;
            }
        }

        public class VarInfo : SymbolInfo
        {
            public TYPE type;
            public LocalBuilder addr;
            public VarInfo(TYPE type, string n) : base(Kind.VAR, n)
            {
                this.type = type;

            }
        }
    }

    public class LexException : Exception
    {
        public LexException(string msg) : base(msg) { }
    }

    public class SyntaxException : Exception
    {
        public SyntaxException(string msg) : base(msg) { }
    }

    public static class ParserHelper
    {
    }
}