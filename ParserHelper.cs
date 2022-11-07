using System.Collections.Generic;
using System;
using ProgramTree;
using System.Linq;
using System.Reflection.Emit;

namespace SimpleParser
{
    public class FuncType { }
    public enum TYPE { INT, DOUBLE, VOID}

    public static class SymbolTableStack {

        static List<SymbolTable> stack = new List<SymbolTable>();
        
        static SymbolTable get() { return stack.Last(); } 

        public static void push(SymbolTable table) {
            stack.Add(table);    
        }

        public static bool pop()
        {
            if (stack.Count > 0)
            {
                stack.RemoveAt(stack.Count - 1);
                return true;
            }
            return false;
        }

        public static void  addInfo(DeclarationNode dec) {
            get().Add(dec);
        }

        public static SymbolTable.SymbolInfo findSymbol(string name, bool needAllocated = false) {

            SymbolTable.SymbolInfo res = null;

            int i = stack.Count - 1;
            while(i >= 0 && res == null) {
                res = stack[i].Get(name);
                if (needAllocated && res != null && !res.allocated) res = null;
                i--;
            }
            if (res != null) return res;
            throw new SyntaxException(name + " не объявлена");
        }

        public static void allocate(string name,LocalBuilder addr) { 
            var si = findSymbol(name);
            switch (si.kind)
            {
                case (SymbolTable.SymbolInfo.Kind.VAR):
                {
                        var v_si = si as SymbolTable.VarInfo;
                        v_si.addr = addr;
                        v_si.allocated = true;
                        break;

                }
                case (SymbolTable.SymbolInfo.Kind.FUNCTION):
                {
                    break;
                }
            }
        }

        public static void init(string name)
        {
            var si = findSymbol(name, true);
            si.initialized = true;
        }
    }

    public  class SymbolTable {

        

        private  Dictionary<string, SymbolInfo> table = new Dictionary<string, SymbolInfo>();

        public  void Add(DeclarationNode dec) { 
            
            switch (dec.kind)
            {
                case DeclarationNode.Kind.FUNCTION:
                    {
                        addFuncInfo(dec as FuncNode);
                        break;
                    }
                case DeclarationNode.Kind.VAR: {     
                        addVarInfo(dec as VarNode);  
                        break;
                    }
            }
        }

        public  SymbolInfo Get(string id) {
            if (table.ContainsKey(id)) { 
                return table[id];
            }
            return null;
        }

        private  void addFuncInfo(FuncNode fn) { 
            throw new NotImplementedException();
        }

        private  void addVarInfo(VarNode vn) {
            TYPE type;
            switch (vn.typeId.Name) {
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

        public class SymbolInfo {
            public enum Kind { FUNCTION, VAR}
            public Kind kind;
            public string name;
            public bool allocated = false;
            public bool initialized = false;
            public SymbolInfo(Kind k, string n) { kind = k; name = n; }
        }

        public class FunctionInfo : SymbolInfo { 
            
            public FuncType type;
            public FunctionInfo(FuncType t, string n) : base(Kind.FUNCTION ,n)
            {
                type = t;
            }
        }

        public class VarInfo : SymbolInfo
        {
            public TYPE type;
            public LocalBuilder addr;
            public VarInfo(TYPE type, string n) : base(Kind.VAR ,n)
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