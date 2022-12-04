using System.Collections.Generic;
using System.Security.Policy;
using QUT.Gppg;
using SimpleLang.Visitors;
using SimpleParser;

namespace ProgramTree
{
    public enum AssignType { Assign, AssignPlus, AssignMinus, AssignMult, AssignDivide };

    public abstract class Node // базовый класс для всех узлов    
    {
        public LexLocation location;
        public Node(LexLocation location)
        {
            this.location = location;
        }

        public abstract void Invite(Visitor v);
    }

    public class FuncCallStmntNode : StatementNode {
        public FuncCallNode call;
        public FuncCallStmntNode(FuncCallNode c, LexLocation l) : base(l)
        { call = c; }
        public override void Invite(Visitor v) { v.VisitFuncCallStmntNode(this); }
    }

    public abstract class ExprNode : Node // базовый класс для всех выражений
    {
        public ExprNode(LexLocation l) : base(l) { }
        public TYPE type;
    }

    public class IdNode : ExprNode
    {
        public string Name { get; set; }
        public IdNode(string name, LexLocation l) : base(l) 
        { Name = name; }
        public override void Invite(Visitor v)
        {
            v.VisitIdNode(this);
        }
    }

    public class WriteNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public WriteNode(ExprNode Expr, LexLocation l) : base(l)
        {
            this.Expr = Expr;
        }
        public override void Invite(Visitor v)
        {
            v.VisitWriteNode(this);
        }
    }

    public class IntNumNode : ExprNode
    {

        public int Num { get; set; }
        public IntNumNode(int num, LexLocation l) : base(l)
            { Num = num; type = TYPE.INT; }
        public override void Invite(Visitor v)
        {
            v.VisitIntNumNode(this);
        }
    }

    public class RealNumNode : ExprNode
    {

        public RealNumNode(double value, LexLocation l) : base(l)
        {
            Value = value;
            type = TYPE.DOUBLE;
        }
        public double Value { get; set; }
        public override void Invite(Visitor v)
        {
            v.VisitRealNumNode(this);
        }
    }

    public class BoolNode : ExprNode
    {
        public bool value;
        public BoolNode(bool value, LexLocation l) : base(l)
        {
            this.value = value;
        }
        public override void Invite(Visitor v)
        {
            v.VisitBoolNode(this);
        }
    }

    public class BinOpNode : ExprNode
    {
        public ExprNode Left { get; set; }
        public ExprNode Right { get; set; }
        public string Op { get; set; }
        public BinOpNode(ExprNode Left, ExprNode Right, string op, LexLocation l) :base(l)
        {
            this.Left = Left;
            this.Right = Right;
            this.Op = op;
        }
        public override void Invite(Visitor v)
        {
            v.VisitBinOpNode(this);
        }
    }

    public class BoolBinOpNode : BinOpNode
    {
        public BoolBinOpNode(ExprNode Left, ExprNode Right, string op, LexLocation l) 
            : base(Left, Right, op, l)
        {
            type = TYPE.BOOL;
        }
        public override void Invite(Visitor v)
        {
            v.VisitBoolBinOpNode(this);
        }
    }

    public class LogicBinOpNode : BinOpNode
    {
        public LogicBinOpNode(ExprNode Left, ExprNode Right, string op, LexLocation l) 
            : base(Left, Right, op, l)
        {
            type = TYPE.BOOL;
        }
        public override void Invite(Visitor v)
        {
            v.VisitLogicBinOpNode(this);
        }
    }

    public class UnaryOpNode : ExprNode
    {
        public ExprNode expr;
        public string op;
        public UnaryOpNode(ExprNode expr, string op, LexLocation l) : base(l)
        {
            this.expr = expr;
            this.op = op;
        }

        public override void Invite(Visitor v)
        {
            v.VisitUnaryOpNode(this);
        }
    }
    public abstract class StatementNode : Node // базовый класс для всех операторов
    {
        public StatementNode(LexLocation l) : base(l) { } 
    }

    public class ReturnNode : StatementNode {
        public ExprNode retExpr;
        public ReturnNode(ExprNode expr, LexLocation l) : base(l) {
            retExpr = expr; 
        }

        public override void Invite(Visitor v)
        {
            v.VisitReturnNode(this);
        }
    }

    public class AssignNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExprNode Expr { get; set; }
        public AssignType AssOp { get; set; }
        public AssignNode(IdNode id, ExprNode expr,
            LexLocation l, AssignType assop = AssignType.Assign ) : base(l)
        {
            Id = id;
            Expr = expr;
            AssOp = assop;
        }
        public override void Invite(Visitor v)
        {
            v.VisitAssignNode(this);
        }
    }

    public class CycleNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public CycleNode(ExprNode expr, StatementNode stat, LexLocation l) : base(l)
        {
            Expr = expr;
            Stat = stat;
        }
        public override void Invite(Visitor v)
        {
            v.VisitCycleNode(this);
        }
    }

    public class WhileNode : StatementNode
    {
        public ExprNode Expr;
        public StatementNode stmnt;
        
        public WhileNode(ExprNode e, StatementNode s, LexLocation l) : base(l)
        {
            Expr= e;
            stmnt= s;
        }

        public override void Invite(Visitor v)
        {
            v.VisitWhileNode(this);
        }
    }

    public class IfNode : StatementNode
    {
        public ExprNode expr { get; set; }
        public StatementNode then { get; set; }
        public StatementNode _else { get; set; }

        public IfNode(ExprNode ex, StatementNode t, StatementNode el, LexLocation l )
        : base(l)
        {
            expr = ex;
            then = t;
            _else = el;
        }
        public override void Invite(Visitor v)
        {
            v.VisitIfNode(this);
        }
    }

    public class BlockNode : StatementNode
    {
        public List<StatementNode> StList = new List<StatementNode>();
        public SymbolTable table ;
        public BlockNode(StatementNode stat, LexLocation l) : base(l) 
        {
            Add(stat);
        }
        public void Add(StatementNode stat)
        {
            StList.Add(stat);
        }
        public override void Invite(Visitor v)
        {
            v.VisitBlockNode(this);    
        }
    }



    public class EmptyNode : StatementNode
    {
        public EmptyNode(LexLocation l) : base(l) { }
        public override void Invite(Visitor v)
        {
            v.VisitEmptyNode(this);
        }
    }

    public abstract class DeclarationNode : StatementNode
    {
        public enum Kind { FUNCTION, VAR }

        public IdNode name;
        public Kind kind;

        protected DeclarationNode(IdNode n, Kind k, LexLocation l) : base(l) 
        {
            name = n;
            kind = k;
        }
    }

    public class VarNode : DeclarationNode
    {
        public IdNode typeId;
        public ExprNode valExpr = null;
        public VarNode(IdNode t, IdNode n, ExprNode expr, LexLocation l) 
            : base(n, Kind.VAR, l) 
        {
            valExpr = expr;
            typeId = t;
        }

        public override void Invite(Visitor v)
        {
            v.VisitVarNode(this);
        }
    }

    public class FuncNode : DeclarationNode
    {
        public FuncBodyNode body;
        public List<ParamNode> _params;
        public IdNode returnTypeId;
        public FuncNode(IdNode n,List<ParamNode> p ,IdNode r, FuncBodyNode body, LexLocation l) 
            : base(n, Kind.FUNCTION ,l)
        {
            _params = p;
            this.body = body;
            
            returnTypeId = r;
        }

        public override void Invite(Visitor v)
        {
            v.VisitFuncNode(this);
        }
    }
    public class ParamNode : VarNode
    {
        internal int pos;

        public ParamNode(IdNode t, IdNode n, LexLocation l) : base(t, n, null, l) {}

        public override void Invite(Visitor v)
        {
            v.VisitParamNode(this);
        }
    }

    public class FuncBodyNode : BlockNode
    {
       
        public FuncBodyNode(BlockNode b, LexLocation l) : base(null, l)
        {
            table  =  b.table;
            StList =  b.StList;
        }

        public override void Invite(Visitor v)
        {
            v.VisitFuncBodyNode(this);
        }
    }

    public class FuncCallNode : ExprNode
    {
        public IdNode id;
        public List<ExprNode> args;

        public FuncCallNode(IdNode id, List<ExprNode> args, LexLocation l ) : base(l)
        {
            this.id = id;
            this.args = args;
        }
    
        public override void Invite(Visitor v)
        {
            v.VisitFuncCallNode(this);
        }

    }
}