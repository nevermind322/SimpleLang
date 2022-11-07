%{
// Эти объявления добавляются в класс GPPGParser, представляющий собой парсер, генерируемый системой gppg
    public BlockNode root; // Корневой узел синтаксического дерева 
    public Parser(AbstractScanner<ValueType, LexLocation> scanner) : base(scanner) { }
	private bool InDefSect = false;
%}

%output = SimpleYacc.cs

%union { 
			public double dVal; 
			public int iVal; 
			public string sVal; 
			public Node nVal;
			public ExprNode eVal;
			public StatementNode stVal;
			public BlockNode blVal;
			public List<ParamNode> parList;
       }

%using System.IO;
%using ProgramTree;

%namespace SimpleParser

%start progr

%token BEGIN END CYCLE ASSIGN ASSIGNPLUS ASSIGNMINUS ASSIGNMULT SEMICOLON VAR PLUS MINUS MULT DIV LPAREN RPAREN COLUMN DIVI MOD IF THEN ELSE FUN
%token COLON RETURN WRITE
%token <iVal> INUM 
%token <dVal> RNUM 
%token <sVal> ID 


%type <eVal> expr ident T F func_call valueParam
%type <stVal> statement assign block cycle empty if return
%type <stVal> declaration var_decl func_decl param valueParams write
%type <blVal> stlist block
%type <parList> params

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
				$$ = new BlockNode($1); 
			}
		;

statement: assign { $$ = $1; }
		| block   { $$ = $1; }
		| cycle   { $$ = $1; }
		| declaration {$$ = $1;}
		| if      { $$ = $1; }
		| return  {$$ = $1;}
		| write   {$$ = $1;}
		| empty   { $$ = $1; }
		;

empty	: { $$ = new EmptyNode(); }
		;
	
write	: WRITE LPAREN expr RPAREN { $$ = new WriteNode($3); }
		;

ident 	: ID 
		{
			
			$$ = new IdNode($1); 
		}	
	;
	
assign 	: ident ASSIGN expr { $$ = new AssignNode($1 as IdNode, $3); }
		;

expr	: expr PLUS T { $$ = new BinOpNode($1,$3,'+'); }
		| expr MINUS T { $$ = new BinOpNode($1,$3,'-'); }
		| T { $$ = $1; }
		;
		
T 		: T MULT F { $$ = new BinOpNode($1,$3,'*'); }
		| T DIV F { $$ = new BinOpNode($1,$3,'/'); }
		| T MOD F {$$ = new BinOpNode($1,$3,'%');}
		| T DIVI F {$$ = new BinOpNode($1,$3,'\\');}
		| F { $$ = $1; }
		;
		
F 		: ident  { $$ = $1 as IdNode; }
		| INUM { $$ = new IntNumNode($1); }
		| RNUM {$$ = new RealNumNode($1);}
		| LPAREN expr RPAREN { $$ = $2; }
		| func_call {$$ = $1;}
		;

block	: BEGIN stlist END { $$ = $2; }
		;

if		: IF expr THEN statement {$$  = new IfNode($2, $4, null);}
		| IF expr THEN statement ELSE statement	{$$ = new IfNode($2, $4, $6);}
		;

cycle	: CYCLE expr statement { $$ = new CycleNode($2,$3); }
		;
		
declaration : var_decl	{$$ = $1;}
		| func_decl	{$$ = $1;}	
		;

var_decl : ident ident {$$ =  new VarNode($1 as IdNode, $2 as IdNode, null);}
		| ident ident ASSIGN expr {$$ =  new VarNode($1 as IdNode, $2 as IdNode, $4);}
		;

func_decl : FUN ident LPAREN params RPAREN COLON ident block
			{$$ = new FuncNode($2 as IdNode, $4, $7 as IdNode,new FuncBodyNode($8));}
		;

params  : param {$$ = new List<ParamNode>(); $$.Add($1 as ParamNode);}
		| params COLUMN param {$$ = $1; $$.Add($3 as ParamNode);}
		| empty {$$ = new List<ParamNode>();}
		; 

param	: ident ident {$$ = new ParamNode($1 as IdNode, $2 as IdNode);}
		;

return	: RETURN expr | RETURN
		;

func_call : ident LPAREN valueParams RPAREN
		;

valueParams : valueParam
		| valueParam COLUMN valueParams
		| empty
		;

valueParam : expr 
		;

%%

