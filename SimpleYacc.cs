// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.3.6
// Machine:  LAPTOP-9M8I2R2U
// DateTime: 05.11.2022 22:02:35
// UserName: ???????
// Input file <SimpleYacc.y>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using System.IO;
using ProgramTree;

namespace SimpleParser
{
public enum Tokens {
    error=1,EOF=2,BEGIN=3,END=4,CYCLE=5,ASSIGN=6,
    ASSIGNPLUS=7,ASSIGNMINUS=8,ASSIGNMULT=9,SEMICOLON=10,VAR=11,PLUS=12,
    MINUS=13,MULT=14,DIV=15,LPAREN=16,RPAREN=17,COLUMN=18,
    DIVI=19,MOD=20,IF=21,THEN=22,ELSE=23,FUN=24,
    COLON=25,RETURN=26,WRITE=27,INUM=28,RNUM=29,ID=30};

public struct ValueType
{ 
			public double dVal; 
			public int iVal; 
			public string sVal; 
			public Node nVal;
			public ExprNode eVal;
			public StatementNode stVal;
			public BlockNode blVal;
       }
// Abstract base class for GPLEX scanners
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from SimpleYacc.y
// ��� ���������� ����������� � ����� GPPGParser, �������������� ����� ������, ������������ �������� gppg
    public BlockNode root; // �������� ���� ��������������� ������ 
    public Parser(AbstractScanner<ValueType, LexLocation> scanner) : base(scanner) { }
	private bool InDefSect = false;
  // End verbatim content from SimpleYacc.y

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[49];
  private static State[] states = new State[85];
  private static string[] nonTerms = new string[] {
      "progr", "expr", "ident", "T", "F", "func_call", "valueParam", "statement", 
      "assign", "block", "cycle", "empty", "if", "return", "declaration", "var_decl", 
      "func_decl", "params", "param", "valueParams", "write", "stlist", "$accept", 
      };

  static Parser() {
    states[0] = new State(new int[]{3,4},new int[]{-1,1,-10,3});
    states[1] = new State(new int[]{2,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{30,29,3,4,5,48,24,54,21,69,26,76,27,79,4,-13,10,-13},new int[]{-22,5,-8,84,-9,9,-3,10,-10,46,-11,47,-15,51,-16,52,-17,53,-13,68,-14,75,-21,78,-12,83});
    states[5] = new State(new int[]{4,6,10,7});
    states[6] = new State(-30);
    states[7] = new State(new int[]{30,29,3,4,5,48,24,54,21,69,26,76,27,79,4,-13,10,-13},new int[]{-8,8,-9,9,-3,10,-10,46,-11,47,-15,51,-16,52,-17,53,-13,68,-14,75,-21,78,-12,83});
    states[8] = new State(-3);
    states[9] = new State(-5);
    states[10] = new State(new int[]{6,11,30,29},new int[]{-3,43});
    states[11] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-2,12,-4,35,-5,41,-3,17,-6,38});
    states[12] = new State(new int[]{12,13,13,25,4,-16,10,-16,23,-16});
    states[13] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-4,14,-5,41,-3,17,-6,38});
    states[14] = new State(new int[]{14,15,15,27,20,36,19,39,12,-17,13,-17,4,-17,10,-17,23,-17,18,-17,17,-17,30,-17,3,-17,5,-17,24,-17,21,-17,26,-17,27,-17,22,-17});
    states[15] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-5,16,-3,17,-6,38});
    states[16] = new State(-20);
    states[17] = new State(new int[]{16,18,14,-25,15,-25,20,-25,19,-25,12,-25,13,-25,4,-25,10,-25,23,-25,18,-25,17,-25,30,-25,3,-25,5,-25,24,-25,21,-25,26,-25,27,-25,22,-25});
    states[18] = new State(new int[]{30,29,28,30,29,31,16,32,17,-13},new int[]{-20,19,-7,21,-2,24,-4,35,-5,41,-3,17,-6,38,-12,42});
    states[19] = new State(new int[]{17,20});
    states[20] = new State(-44);
    states[21] = new State(new int[]{18,22,17,-45});
    states[22] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-7,23,-2,24,-4,35,-5,41,-3,17,-6,38});
    states[23] = new State(-46);
    states[24] = new State(new int[]{12,13,13,25,18,-48,17,-48});
    states[25] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-4,26,-5,41,-3,17,-6,38});
    states[26] = new State(new int[]{14,15,15,27,20,36,19,39,12,-18,13,-18,4,-18,10,-18,23,-18,18,-18,17,-18,30,-18,3,-18,5,-18,24,-18,21,-18,26,-18,27,-18,22,-18});
    states[27] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-5,28,-3,17,-6,38});
    states[28] = new State(-21);
    states[29] = new State(-15);
    states[30] = new State(-26);
    states[31] = new State(-27);
    states[32] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-2,33,-4,35,-5,41,-3,17,-6,38});
    states[33] = new State(new int[]{17,34,12,13,13,25});
    states[34] = new State(-28);
    states[35] = new State(new int[]{14,15,15,27,20,36,19,39,12,-19,13,-19,4,-19,10,-19,23,-19,18,-19,17,-19,30,-19,3,-19,5,-19,24,-19,21,-19,26,-19,27,-19,22,-19});
    states[36] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-5,37,-3,17,-6,38});
    states[37] = new State(-22);
    states[38] = new State(-29);
    states[39] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-5,40,-3,17,-6,38});
    states[40] = new State(-23);
    states[41] = new State(-24);
    states[42] = new State(-47);
    states[43] = new State(new int[]{6,44,4,-36,10,-36,23,-36});
    states[44] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-2,45,-4,35,-5,41,-3,17,-6,38});
    states[45] = new State(new int[]{12,13,13,25,4,-37,10,-37,23,-37});
    states[46] = new State(-6);
    states[47] = new State(-7);
    states[48] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-2,49,-4,35,-5,41,-3,17,-6,38});
    states[49] = new State(new int[]{12,13,13,25,30,29,3,4,5,48,24,54,21,69,26,76,27,79,4,-13,10,-13,23,-13},new int[]{-8,50,-9,9,-3,10,-10,46,-11,47,-15,51,-16,52,-17,53,-13,68,-14,75,-21,78,-12,83});
    states[50] = new State(-33);
    states[51] = new State(-8);
    states[52] = new State(-34);
    states[53] = new State(-35);
    states[54] = new State(new int[]{30,29},new int[]{-3,55});
    states[55] = new State(new int[]{16,56});
    states[56] = new State(new int[]{30,29},new int[]{-18,57,-19,62,-3,65});
    states[57] = new State(new int[]{17,58});
    states[58] = new State(new int[]{25,59});
    states[59] = new State(new int[]{30,29},new int[]{-3,60});
    states[60] = new State(new int[]{3,4},new int[]{-10,61});
    states[61] = new State(-38);
    states[62] = new State(new int[]{18,63,17,-39});
    states[63] = new State(new int[]{30,29},new int[]{-18,64,-19,62,-3,65});
    states[64] = new State(-40);
    states[65] = new State(new int[]{25,66});
    states[66] = new State(new int[]{30,29},new int[]{-3,67});
    states[67] = new State(-41);
    states[68] = new State(-9);
    states[69] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-2,70,-4,35,-5,41,-3,17,-6,38});
    states[70] = new State(new int[]{22,71,12,13,13,25});
    states[71] = new State(new int[]{30,29,3,4,5,48,24,54,21,69,26,76,27,79,4,-13,10,-13,23,-13},new int[]{-8,72,-9,9,-3,10,-10,46,-11,47,-15,51,-16,52,-17,53,-13,68,-14,75,-21,78,-12,83});
    states[72] = new State(new int[]{23,73,4,-31,10,-31});
    states[73] = new State(new int[]{30,29,3,4,5,48,24,54,21,69,26,76,27,79,4,-13,10,-13,23,-13},new int[]{-8,74,-9,9,-3,10,-10,46,-11,47,-15,51,-16,52,-17,53,-13,68,-14,75,-21,78,-12,83});
    states[74] = new State(-32);
    states[75] = new State(-10);
    states[76] = new State(new int[]{30,29,28,30,29,31,16,32,4,-43,10,-43,23,-43},new int[]{-2,77,-4,35,-5,41,-3,17,-6,38});
    states[77] = new State(new int[]{12,13,13,25,4,-42,10,-42,23,-42});
    states[78] = new State(-11);
    states[79] = new State(new int[]{16,80});
    states[80] = new State(new int[]{30,29,28,30,29,31,16,32},new int[]{-2,81,-4,35,-5,41,-3,17,-6,38});
    states[81] = new State(new int[]{17,82,12,13,13,25});
    states[82] = new State(-14);
    states[83] = new State(-12);
    states[84] = new State(-4);

    rules[1] = new Rule(-23, new int[]{-1,2});
    rules[2] = new Rule(-1, new int[]{-10});
    rules[3] = new Rule(-22, new int[]{-22,10,-8});
    rules[4] = new Rule(-22, new int[]{-8});
    rules[5] = new Rule(-8, new int[]{-9});
    rules[6] = new Rule(-8, new int[]{-10});
    rules[7] = new Rule(-8, new int[]{-11});
    rules[8] = new Rule(-8, new int[]{-15});
    rules[9] = new Rule(-8, new int[]{-13});
    rules[10] = new Rule(-8, new int[]{-14});
    rules[11] = new Rule(-8, new int[]{-21});
    rules[12] = new Rule(-8, new int[]{-12});
    rules[13] = new Rule(-12, new int[]{});
    rules[14] = new Rule(-21, new int[]{27,16,-2,17});
    rules[15] = new Rule(-3, new int[]{30});
    rules[16] = new Rule(-9, new int[]{-3,6,-2});
    rules[17] = new Rule(-2, new int[]{-2,12,-4});
    rules[18] = new Rule(-2, new int[]{-2,13,-4});
    rules[19] = new Rule(-2, new int[]{-4});
    rules[20] = new Rule(-4, new int[]{-4,14,-5});
    rules[21] = new Rule(-4, new int[]{-4,15,-5});
    rules[22] = new Rule(-4, new int[]{-4,20,-5});
    rules[23] = new Rule(-4, new int[]{-4,19,-5});
    rules[24] = new Rule(-4, new int[]{-5});
    rules[25] = new Rule(-5, new int[]{-3});
    rules[26] = new Rule(-5, new int[]{28});
    rules[27] = new Rule(-5, new int[]{29});
    rules[28] = new Rule(-5, new int[]{16,-2,17});
    rules[29] = new Rule(-5, new int[]{-6});
    rules[30] = new Rule(-10, new int[]{3,-22,4});
    rules[31] = new Rule(-13, new int[]{21,-2,22,-8});
    rules[32] = new Rule(-13, new int[]{21,-2,22,-8,23,-8});
    rules[33] = new Rule(-11, new int[]{5,-2,-8});
    rules[34] = new Rule(-15, new int[]{-16});
    rules[35] = new Rule(-15, new int[]{-17});
    rules[36] = new Rule(-16, new int[]{-3,-3});
    rules[37] = new Rule(-16, new int[]{-3,-3,6,-2});
    rules[38] = new Rule(-17, new int[]{24,-3,16,-18,17,25,-3,-10});
    rules[39] = new Rule(-18, new int[]{-19});
    rules[40] = new Rule(-18, new int[]{-19,18,-18});
    rules[41] = new Rule(-19, new int[]{-3,25,-3});
    rules[42] = new Rule(-14, new int[]{26,-2});
    rules[43] = new Rule(-14, new int[]{26});
    rules[44] = new Rule(-6, new int[]{-3,16,-20,17});
    rules[45] = new Rule(-20, new int[]{-7});
    rules[46] = new Rule(-20, new int[]{-7,18,-7});
    rules[47] = new Rule(-20, new int[]{-12});
    rules[48] = new Rule(-7, new int[]{-2});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
      case 2: // progr -> block
{ root = ValueStack[ValueStack.Depth-1].blVal; }
        break;
      case 3: // stlist -> stlist, SEMICOLON, statement
{ 
			ValueStack[ValueStack.Depth-3].blVal.Add(ValueStack[ValueStack.Depth-1].stVal); 
				CurrentSemanticValue.blVal = ValueStack[ValueStack.Depth-3].blVal; 
			}
        break;
      case 4: // stlist -> statement
{ 
				CurrentSemanticValue.blVal = new BlockNode(ValueStack[ValueStack.Depth-1].stVal); 
			}
        break;
      case 5: // statement -> assign
{ CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal; }
        break;
      case 6: // statement -> block
{ CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].blVal; }
        break;
      case 7: // statement -> cycle
{ CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal; }
        break;
      case 8: // statement -> declaration
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 9: // statement -> if
{ CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal; }
        break;
      case 10: // statement -> return
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 11: // statement -> write
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 12: // statement -> empty
{ CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal; }
        break;
      case 13: // empty -> /* empty */
{ CurrentSemanticValue.stVal = new EmptyNode(); }
        break;
      case 14: // write -> WRITE, LPAREN, expr, RPAREN
{ CurrentSemanticValue.stVal = new WriteNode(ValueStack[ValueStack.Depth-2].eVal); }
        break;
      case 15: // ident -> ID
{
			
			CurrentSemanticValue.eVal = new IdNode(ValueStack[ValueStack.Depth-1].sVal); 
		}
        break;
      case 16: // assign -> ident, ASSIGN, expr
{ CurrentSemanticValue.stVal = new AssignNode(ValueStack[ValueStack.Depth-3].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal); }
        break;
      case 17: // expr -> expr, PLUS, T
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,'+'); }
        break;
      case 18: // expr -> expr, MINUS, T
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,'-'); }
        break;
      case 19: // expr -> T
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal; }
        break;
      case 20: // T -> T, MULT, F
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,'*'); }
        break;
      case 21: // T -> T, DIV, F
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,'/'); }
        break;
      case 22: // T -> T, MOD, F
{CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,'%');}
        break;
      case 23: // T -> T, DIVI, F
{CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,'\\');}
        break;
      case 24: // T -> F
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal; }
        break;
      case 25: // F -> ident
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal as IdNode; }
        break;
      case 26: // F -> INUM
{ CurrentSemanticValue.eVal = new IntNumNode(ValueStack[ValueStack.Depth-1].iVal); }
        break;
      case 27: // F -> RNUM
{CurrentSemanticValue.eVal = new RealNumNode(ValueStack[ValueStack.Depth-1].dVal);}
        break;
      case 28: // F -> LPAREN, expr, RPAREN
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-2].eVal; }
        break;
      case 29: // F -> func_call
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 30: // block -> BEGIN, stlist, END
{ CurrentSemanticValue.blVal = ValueStack[ValueStack.Depth-2].blVal; }
        break;
      case 31: // if -> IF, expr, THEN, statement
{CurrentSemanticValue.stVal  = new IfNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].stVal, null);}
        break;
      case 32: // if -> IF, expr, THEN, statement, ELSE, statement
{CurrentSemanticValue.stVal = new IfNode(ValueStack[ValueStack.Depth-5].eVal, ValueStack[ValueStack.Depth-3].stVal, ValueStack[ValueStack.Depth-1].stVal);}
        break;
      case 33: // cycle -> CYCLE, expr, statement
{ CurrentSemanticValue.stVal = new CycleNode(ValueStack[ValueStack.Depth-2].eVal,ValueStack[ValueStack.Depth-1].stVal); }
        break;
      case 34: // declaration -> var_decl
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 36: // var_decl -> ident, ident
{CurrentSemanticValue.stVal =  new VarNode(ValueStack[ValueStack.Depth-2].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal as IdNode, null);}
        break;
      case 37: // var_decl -> ident, ident, ASSIGN, expr
{CurrentSemanticValue.stVal =  new VarNode(ValueStack[ValueStack.Depth-4].eVal as IdNode, ValueStack[ValueStack.Depth-3].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal);}
        break;
    }
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }


}
}