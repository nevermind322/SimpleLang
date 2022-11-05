using System.Collections.Generic;
using System;
using ProgramTree;

namespace SimpleParser
{
    public class FuncType { }
    public enum TYPE { INT, DOUBLE}
    public static class SymbolTable {

        

        private static Dictionary<string, SymbolInfo> table = new Dictionary<string, SymbolInfo>();

        public static void Add(DeclarationNode dec) { 
            
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

        public static SymbolInfo Get(string id) {
            if (table.ContainsKey(id)) { 
                return table[id];
            }
            throw new SyntaxException("{id} не объявлена" );
        }

        private static void addFuncInfo(FuncNode fn) { 
            throw new NotImplementedException();
        }

        private static void addVarInfo(VarNode vn) {
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
                default:
                {
                     throw new SyntaxException("Неизвестный тип");
                }
            }
            var info = new VarInfo(type);
            table.Add(vn.name.Name, info);
        } 

        public class SymbolInfo { 
            public enum Kind { FUNCTION, VAR}
            public Kind kind;
            public SymbolInfo(Kind k) { kind = k; }
        }

        public class FunctionInfo : SymbolInfo { 
            
            public FuncType type;
            public FunctionInfo(FuncType t) : base(Kind.FUNCTION )
            {
                type = t;
            }
        }

        public class VarInfo : SymbolInfo
        {
            public TYPE type;

            public VarInfo(TYPE type) : base(Kind.VAR )
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