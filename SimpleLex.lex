%using SimpleParser;
%using QUT.Gppg;
%using System.Linq;

%namespace SimpleScanner

Alpha 	[a-zA-Z_]
Digit   [0-9] 
AlphaDigit {Alpha}|{Digit}
INTNUM  {Digit}+
REALNUM {INTNUM}\.{INTNUM}
ID {Alpha}{AlphaDigit}* 

%%

{INTNUM} { 
  yylval.iVal = int.Parse(yytext); 
  return (int)Tokens.INUM; 
}

{REALNUM} { 
  yylval.dVal = double.Parse(yytext, CultureInfo.InvariantCulture); 
  return (int)Tokens.RNUM;
}

{ID}  { 
  int res = ScannerHelper.GetIDToken(yytext);
  if (res == (int)Tokens.ID)
	yylval.sVal = yytext;
  return res;
}

"=" { return (int)Tokens.ASSIGN; }
";" { return (int)Tokens.SEMICOLON; }
"-=" { return (int)Tokens.ASSIGNMINUS; }
"+=" { return (int)Tokens.ASSIGNPLUS; }
"*=" { return (int)Tokens.ASSIGNMULT; }
"+" { return (int)Tokens.PLUS; }
"-" { return (int)Tokens.MINUS; }
"*" { return (int)Tokens.MULT; }
"/" { return (int)Tokens.DIV; }
"(" { return (int)Tokens.LPAREN; }
")" { return (int)Tokens.RPAREN; }
"," { return (int)Tokens.COLUMN; }
"//" { return (int)Tokens.DIVI;}
"%" {return (int) Tokens.MOD; }
":" {return (int) Tokens.COLON; }
"<" {return (int) Tokens.L; }
"<=" {return (int) Tokens.LE; }
">" {return (int) Tokens.G; }
">=" {return (int) Tokens.GE; }
"==" {return (int) Tokens.EQ; }
"!=" {return (int) Tokens.NEQ; }
"&&" {return (int) Tokens.AND; }
"||" {return (int) Tokens.OR; }
"!" {return (int) Tokens.NOT; }
[^ \t\r\n] {
	LexError();
}

%{
  yylloc = new LexLocation(tokLin, tokCol, tokELin, tokECol);
%}

%%

public override void yyerror(string format, params object[] args) // обработка синтаксических ошибок
{
  var ww = args.Skip(1).Cast<string>().ToArray();
  string errorMsg = string.Format("({0},{1}): Встречено {2}, а ожидалось {3}", yyline, yycol, args[0], string.Join(" или ", ww));
  throw new SyntaxException(errorMsg);
}

public void LexError()
{
  string errorMsg = string.Format("({0},{1}): Неизвестный символ {2}", yyline, yycol, yytext);
  throw new LexException(errorMsg);
}

class ScannerHelper 
{
  private static Dictionary<string,int> keywords;

  static ScannerHelper() 
  {
    keywords = new Dictionary<string,int>();
    keywords.Add("begin",(int)Tokens.BEGIN);
    keywords.Add("end",(int)Tokens.END);
    keywords.Add("cycle",(int)Tokens.CYCLE);
    keywords.Add("while",(int)Tokens.WHILE);
    keywords.Add("for",(int)Tokens.FOR);
    keywords.Add("var",(int)Tokens.VAR);
    keywords.Add("if",(int)Tokens.IF);
    keywords.Add("then",(int)Tokens.THEN);
    keywords.Add("else",(int)Tokens.ELSE);
    keywords.Add("write",(int)Tokens.WRITE);
    keywords.Add("fun",(int)Tokens.FUN);
    keywords.Add("return",(int)Tokens.RETURN);
    keywords.Add("true",(int)Tokens.TRUE);
    keywords.Add("false",(int)Tokens.FALSE);



  }
  public static int GetIDToken(string s)
  {
	if (keywords.ContainsKey(s.ToLower()))
	  return keywords[s];
	else
      return (int)Tokens.ID;
  }
  
}
