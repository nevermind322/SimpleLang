%{
// ��� ���������� ����������� � ����� GPPGParser, �������������� ����� ������, ������������ �������� gppg
    public BlockNode root; // �������� ���� ��������������� ������ 
    public Parser(AbstractScanner<ValueType, LexLocation> scanner) : base(scanner) { }
	private bool InDefSect = false;
%}

%output = SimpleYacc.cs

%union { 
			public double dVal; 
			public int iVal;
			public bool bVal;
			public string sVal; 
			public Node nVal;
			public ExprNode eVal;
			public StatementNode stVal;
			public BlockNode blVal;
			public List<ParamNode> parList;
			public List<ExprNode> exprList;
       }

%using System.IO;
%using ProgramTree;

%namespace SimpleParser

%start progr

%token BEGIN END CYCLE ASSIGN ASSIGNPLUS ASSIGNMINUS ASSIGNMULT SEMICOLON VAR PLUS MINUS MULT DIV LPAREN RPAREN COLUMN DIVI MOD IF THEN ELSE FUN
%token COLON RETURN WRITE WHILE FOR FALSE TRUE L, LE, G, GE, EQ, NEQ OR AND NOT
%token <iVal> INUM 
%token <dVal> RNUM 
%token <sVal> ID 



%type <eVal> expr ident T F func_call valueParam bool_expr disjunction conjunction additive_expr
%type <stVal> statement assign  cycle empty if return  while
%type <stVal> declaration var_decl func_decl param write loop
%type <blVal> stlist block
%type <parList> params
%type <exprList> valueParams
%type <bVal> bool_literal

%%

progr   : block { root = $1; }
		;

stlist	: 
		 stlist SEMICOLON statement 
			{ 
			$1.Add($3); 
				$$ = $1; 
			}
			| statement 
			{ 
				$$ = new BlockNode($1, @$); 
			}
		;

statement: assign { $$ = $1; }
		| block   { $$ = $1; }
		| loop  { $$ = $1; }
		| declaration {$$ = $1;}
		| if      { $$ = $1; }
		| return  {$$ = $1;}
		| write   {$$ = $1;}
		| empty   { $$ = $1; }
		| func_call {$$ = new FuncCallStmntNode($1 as FuncCallNode, @$); }
		;

empty	: { $$ = new EmptyNode( @$); }
		;
	
write	: WRITE LPAREN expr RPAREN { $$ = new WriteNode($3, @$); }
		;

ident 	: ID {$$ = new IdNode($1, @$);}	
	;
	
assign 	: ident ASSIGN expr { $$ = new AssignNode($1 as IdNode, $3, @$); }
		;


expr	: disjunction  { $$ = $1; }
		;

disjunction : disjunction OR conjunction{$$ = new LogicBinOpNode($1, $3, "||", @$);}
		| conjunction {$$ = $1;}
		;
conjunction :  conjunction AND bool_expr {$$ = new LogicBinOpNode($1, $3, "&&", @$);}
		| bool_expr {$$ = $1;}
		;

additive_expr :additive_expr PLUS T { $$ = new BinOpNode($1,$3,"+", @$); }
		| additive_expr MINUS T { $$ = new BinOpNode($1,$3,"-", @$); }
		| T {$$ = $1;}
		;

bool_expr	: bool_expr L additive_expr {$$ = new BoolBinOpNode($1, $3, "<", @$);}
		| bool_expr LE additive_expr {$$ = new BoolBinOpNode($1, $3, "<=", @$);}
		| bool_expr G additive_expr {$$ = new BoolBinOpNode($1, $3, ">", @$);}
		| bool_expr GE additive_expr {$$ = new BoolBinOpNode($1, $3, ">=", @$);}
		| bool_expr EQ additive_expr {$$ = new BoolBinOpNode($1, $3, "==", @$);}
		| bool_expr NEQ additive_expr {$$ = new BoolBinOpNode($1, $3, "!=", @$);}
		| additive_expr {$$ = $1;}
		;
T 		: T MULT F { $$ = new BinOpNode($1,$3,"*", @$); }
		| T DIV F { $$ = new BinOpNode($1,$3,"/", @$); }
		| T MOD F {$$ = new BinOpNode($1,$3,"%", @$);}
		| T DIVI F {$$ = new BinOpNode($1,$3,"//", @$);}
		| F { $$ = $1; }
		
		;
		
F 		: ident  { $$ = $1 as IdNode; }
		| INUM { $$ = new IntNumNode($1, @$); }
		| RNUM {$$ = new RealNumNode($1, @$);}
		| LPAREN expr RPAREN { $$ = $2; }
		| func_call {$$ = $1;}
		| bool_literal {$$ = new BoolNode($1, @$);}
		| NOT F {$$ = new UnaryOpNode($2, "!", @$);}
		;

bool_literal :TRUE {$$ = true;}| FALSE {$$ = false;}
		;
block	: BEGIN stlist END { $$ = $2; }
		;
		
if		: IF expr THEN statement {$$  = new IfNode($2, $4, null, @$);}
		| IF expr THEN statement ELSE statement	{$$ = new IfNode($2, $4, $6, @$);}
		;

loop	: cycle {$$ = $1;} | while {$$ =$1;} 
		;

cycle	: CYCLE expr statement { $$ = new CycleNode($2,$3, @$); }
		;

while	: WHILE LPAREN expr RPAREN statement {$$ = new WhileNode($3, $5, @$);}
		;

declaration : var_decl	{$$ = $1;}
		| func_decl	{$$ = $1;}	
		;

var_decl : ident ident {$$ =  new VarNode($1 as IdNode, $2 as IdNode, null, @$);}
		| ident ident ASSIGN expr {$$ =  new VarNode($1 as IdNode, $2 as IdNode, $4, @$);}
		;

func_decl : FUN ident LPAREN params RPAREN COLON ident block
			{$$ = new FuncNode($2 as IdNode, $4, $7 as IdNode,new FuncBodyNode($8, @$), @$);}
		;

params  : param {$$ = new List<ParamNode>(); $$.Add($1 as ParamNode);}
		| params COLUMN param {$$ = $1; $$.Add($3 as ParamNode);}
		| empty {$$ = new List<ParamNode>();}
		; 

param	: ident ident {$$ = new ParamNode($1 as IdNode, $2 as IdNode, @$);}
		;

return	: RETURN expr {$$ = new ReturnNode($2, @$);}| RETURN {$$ = new ReturnNode(null, @$);}
		;

func_call : ident LPAREN valueParams RPAREN {$$ = new FuncCallNode($1 as IdNode, $3, @$);}
		;

valueParams : valueParam {$$ = new List<ExprNode>(); $$.Add($1);}
		| valueParams COLUMN valueParam {$$ = $1; $$.Add($3);}
		| empty {$$ = new List<ExprNode>();}
		;

valueParam : expr {$$ = $1;}
		;

%%

