using System.Collections.Generic;
using System;
using ProgramTree;
using System.Linq;
using System.Reflection.Emit;

namespace SimpleParser
{

    public class FuncType
    {
        public List<TYPE> params_type = new();
        public TYPE return_type;

        public FuncType()
        {
        }

        public static Type getCStype(TYPE t)
        {
            return t switch
            {
                TYPE.VOID => typeof(void),
                TYPE.INT => typeof(int),
                TYPE.DOUBLE => typeof(double),
                _ => throw new SyntaxException("Неизвестный тип"),
            };
        }

        public void addParamType(TYPE t)
        {
            params_type.Add(t);
        }
    }
    public enum TYPE { BOOL, INT, DOUBLE, VOID }

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
                if (t.table.TryGetValue(name, out res)) return res;
            }
            throw new SyntaxException(name + " не объявлена");
        }

        public void Add(FuncNode dec)
        {
            addFuncInfo(dec);
        }

        public void Add(VarNode dec)
        {
            addVarInfo(dec);
        }

        public void Add(ParamNode dec)
        {
            addParamInfo(dec);
        }

        private void addParamInfo(ParamNode pn)
        {

            TYPE type = ParserHelper.getType(pn.typeId.Name);

            if (type == TYPE.VOID)
                throw new SyntaxException("параметр не может быть void");

            var info = new ParamInfo(type, pn.name.Name, pn.pos);
            table.Add(pn.name.Name, info);
        }

        private void addFuncInfo(FuncNode fn)
        {
            var ft = new FuncType();
            TYPE t;
            foreach (var param in fn._params)
            {
                t = ParserHelper.getType(param.typeId.Name);
                if (t == TYPE.VOID) throw new SyntaxException("параметр не может быть void");
                ft.addParamType(t);

            }
            t = ParserHelper.getType(fn.returnTypeId.Name);

            ft.return_type = t;
            FunctionInfo info = new(ft, fn.name.Name);

            info.BodyTable = fn.body.table;
            table.Add(fn.name.Name, info);

        }

        private void addVarInfo(VarNode vn)
        {
            TYPE type = ParserHelper.getType(vn.typeId.Name);

            if (type == TYPE.VOID)
                throw new SyntaxException("переменная не может быть void");

            var info = new VarInfo(type, vn.name.Name);
            table.Add(vn.name.Name, info);
        }

        public void allocate(string name, LocalBuilder addr)
        {

            table[name].addr = addr;
            table[name].allocated = true;
        }

        public void init(string name)
        {
            find(name).initialized = true;
        }

        public class SymbolInfo
        {
            public enum Kind { FUNCTION, VAR, PARAM }
            public Kind kind;
            public string name;
            public bool allocated = false;
            public bool initialized = false;
            public LocalBuilder addr;
            public SymbolInfo(Kind k, string n) { kind = k; name = n; }
        }

        public class FunctionInfo : SymbolInfo
        {
            public DynamicMethod dyn;
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

            public VarInfo(TYPE type, string n) : base(Kind.VAR, n)
            {
                this.type = type;

            }
        }

        public class ParamInfo : SymbolInfo
        {
            public int pos = -1;
            public TYPE type;
            public ParamInfo(TYPE t, string name, int pos) : base(Kind.PARAM, name)
            {
                this.pos = pos;
                type = t;
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
        public static TYPE getType(string s)
        {
            switch (s)
            {

                case "double":
                    {
                        return TYPE.DOUBLE;

                    }
                case "int":
                    {
                        return TYPE.INT;

                    }
                case "void":
                    {
                        return TYPE.VOID;

                    }
                case "bool":
                    {
                        return TYPE.BOOL;
                    }
                default:
                    {
                        throw new SyntaxException("Неизвестный тип");
                    }
            }
        }
        public static TYPE widen(TYPE t1, TYPE t2)
        {
            if (t1 == TYPE.VOID || t2 == TYPE.VOID) throw new SyntaxException("нельзя привести к void");
            if (t1 == t2) return t1;
            return CoercionTable.getCoercion(t1, t2);
        }

        public static bool canBeWiden(TYPE from, TYPE to)
        {

            if (from == TYPE.VOID || to == TYPE.VOID) throw new SyntaxException("нельзя привести к void");
            if(to == from  ) return true;
            return CoercionTable.canBeCoerced(from, to);

        }
        public class Pair<L, R>
        {
            public L left;
            public R right;
            public Pair(L left, R right)
            {
                this.left = left; this.right = right;
            }
        }
        public static class CoercionTable
        {
            public static Dictionary<Pair<TYPE, TYPE>, TYPE> table = new();
            static CoercionTable()
            {
                table.Add(new Pair<TYPE, TYPE>(TYPE.BOOL, TYPE.INT), TYPE.INT);
                table.Add(new Pair<TYPE, TYPE>(TYPE.BOOL, TYPE.DOUBLE), TYPE.DOUBLE);
                table.Add(new Pair<TYPE, TYPE>(TYPE.INT, TYPE.DOUBLE), TYPE.INT);

            }
            public static TYPE getCoercion(TYPE t1, TYPE t2)
            {
                TYPE res;
                try
                {
                    res = table[new Pair<TYPE, TYPE>(t1, t2)];
                }
                catch
                {
                    res = table[new Pair<TYPE, TYPE>(t2, t1)];
                }
                return res;
            }

            public static bool canBeCoerced(TYPE from, TYPE to)
            {
                TYPE res;
                try
                {
                    res = table[new Pair<TYPE, TYPE>(from, to)];
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public static class TypeCoercionTree
            {
                static public Node root;
                public class Node
                {
                    public TYPE val;
                    public Node parent;
                    public List<Node> childs = new();
                    public int depth
                    {
                        get
                        {
                            int i = 0;
                            for (Node n = this; n != null; n = n.parent) i++;
                            return i;
                        }

                    }
                    public Node(Node parent, TYPE val)
                    {
                        this.parent = parent;
                        this.val = val;
                    }

                    public void addChild(Node child) { childs.Add(child); }
                }

                static TypeCoercionTree()
                {
                    root = new Node(null, TYPE.DOUBLE);
                    root.addChild(new Node(null, TYPE.INT));
                    root.childs[0].addChild(new Node(null, TYPE.BOOL));
                }


            }
        }
    }
}