// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.3.6
// Machine:  LAPTOP-9M8I2R2U
// DateTime: 28.11.2022 15:19:55
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
    COLON=25,RETURN=26,WRITE=27,WHILE=28,FOR=29,FALSE=30,
    TRUE=31,L=32,LE=33,G=34,GE=35,EQ=36,
    NEQ=37,OR=38,AND=39,NOT=40,INUM=41,RNUM=42,
    ID=43};

public struct ValueType
{ 
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
  private static Rule[] rules = new Rule[68];
  private static State[] states = new State[116];
  private static string[] nonTerms = new string[] {
      "progr", "expr", "ident", "T", "F", "func_call", "valueParam", "bool_expr", 
      "logic_expr", "statement", "assign", "cycle", "empty", "if", "return", 
      "while", "declaration", "var_decl", "func_decl", "param", "write", "loop", 
      "stlist", "block", "params", "valueParams", "bool_literal", "$accept", 
      };

  static Parser() {
    states[0] = new State(new int[]{3,4},new int[]{-1,1,-24,3});
    states[1] = new State(new int[]{2,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{43,28,3,4,5,72,28,76,24,84,21,99,26,106,27,109,4,-14,10,-14},new int[]{-23,5,-10,115,-11,9,-3,10,-24,69,-22,70,-12,71,-16,75,-17,81,-18,82,-19,83,-14,98,-15,105,-21,108,-13,113,-6,114});
    states[5] = new State(new int[]{4,6,10,7});
    states[6] = new State(-45);
    states[7] = new State(new int[]{43,28,3,4,5,72,28,76,24,84,21,99,26,106,27,109,4,-14,10,-14},new int[]{-10,8,-11,9,-3,10,-24,69,-22,70,-12,71,-16,75,-17,81,-18,82,-19,83,-14,98,-15,105,-21,108,-13,113,-6,114});
    states[8] = new State(-3);
    states[9] = new State(-5);
    states[10] = new State(new int[]{6,11,16,18,43,28},new int[]{-3,66});
    states[11] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-2,12,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[12] = new State(new int[]{12,13,13,24,4,-17,10,-17,23,-17});
    states[13] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-4,14,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[14] = new State(new int[]{14,15,15,26,20,42,19,44,32,46,33,48,34,50,35,52,36,54,37,56,12,-18,13,-18,4,-18,10,-18,23,-18,17,-18,18,-18,43,-18,3,-18,5,-18,28,-18,24,-18,21,-18,26,-18,27,-18,22,-18});
    states[15] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,16,-3,17,-6,38,-27,39});
    states[16] = new State(-31);
    states[17] = new State(new int[]{16,18,38,-37,14,-37,15,-37,20,-37,19,-37,32,-37,33,-37,34,-37,35,-37,36,-37,37,-37,39,-37,12,-37,13,-37,4,-37,10,-37,23,-37,17,-37,18,-37,43,-37,3,-37,5,-37,28,-37,24,-37,21,-37,26,-37,27,-37,22,-37});
    states[18] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62,17,-14,18,-14},new int[]{-26,19,-7,64,-2,23,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61,-13,65});
    states[19] = new State(new int[]{17,20,18,21});
    states[20] = new State(-63);
    states[21] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-7,22,-2,23,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[22] = new State(-65);
    states[23] = new State(new int[]{12,13,13,24,17,-67,18,-67});
    states[24] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-4,25,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[25] = new State(new int[]{14,15,15,26,20,42,19,44,32,46,33,48,34,50,35,52,36,54,37,56,12,-19,13,-19,4,-19,10,-19,23,-19,17,-19,18,-19,43,-19,3,-19,5,-19,28,-19,24,-19,21,-19,26,-19,27,-19,22,-19});
    states[26] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,27,-3,17,-6,38,-27,39});
    states[27] = new State(-32);
    states[28] = new State(-16);
    states[29] = new State(-38);
    states[30] = new State(-39);
    states[31] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-2,32,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[32] = new State(new int[]{17,33,12,13,13,24});
    states[33] = new State(-40);
    states[34] = new State(-20);
    states[35] = new State(new int[]{38,36,14,15,15,26,20,42,19,44,32,46,33,48,34,50,35,52,36,54,37,56,39,58,12,-24,13,-24,4,-24,10,-24,23,-24,17,-24,18,-24,43,-24,3,-24,5,-24,28,-24,24,-24,21,-24,26,-24,27,-24,22,-24});
    states[36] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,37,-3,17,-6,38,-27,39});
    states[37] = new State(-21);
    states[38] = new State(-41);
    states[39] = new State(-42);
    states[40] = new State(-43);
    states[41] = new State(-44);
    states[42] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,43,-3,17,-6,38,-27,39});
    states[43] = new State(-33);
    states[44] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,45,-3,17,-6,38,-27,39});
    states[45] = new State(-34);
    states[46] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,47,-3,17,-6,38,-27,39});
    states[47] = new State(-25);
    states[48] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,49,-3,17,-6,38,-27,39});
    states[49] = new State(-26);
    states[50] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,51,-3,17,-6,38,-27,39});
    states[51] = new State(-27);
    states[52] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,53,-3,17,-6,38,-27,39});
    states[53] = new State(-28);
    states[54] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,55,-3,17,-6,38,-27,39});
    states[55] = new State(-29);
    states[56] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,57,-3,17,-6,38,-27,39});
    states[57] = new State(-30);
    states[58] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-5,59,-3,17,-6,38,-27,39});
    states[59] = new State(-22);
    states[60] = new State(-35);
    states[61] = new State(-36);
    states[62] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41},new int[]{-4,63,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[63] = new State(new int[]{14,15,15,26,20,42,19,44,32,46,33,48,34,50,35,52,36,54,37,56,12,-23,13,-23,4,-23,10,-23,23,-23,17,-23,18,-23,43,-23,3,-23,5,-23,28,-23,24,-23,21,-23,26,-23,27,-23,22,-23});
    states[64] = new State(-64);
    states[65] = new State(-66);
    states[66] = new State(new int[]{6,67,4,-54,10,-54,23,-54});
    states[67] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-2,68,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[68] = new State(new int[]{12,13,13,24,4,-55,10,-55,23,-55});
    states[69] = new State(-6);
    states[70] = new State(-7);
    states[71] = new State(-48);
    states[72] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-2,73,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[73] = new State(new int[]{12,13,13,24,43,28,3,4,5,72,28,76,24,84,21,99,26,106,27,109,4,-14,10,-14,23,-14},new int[]{-10,74,-11,9,-3,10,-24,69,-22,70,-12,71,-16,75,-17,81,-18,82,-19,83,-14,98,-15,105,-21,108,-13,113,-6,114});
    states[74] = new State(-50);
    states[75] = new State(-49);
    states[76] = new State(new int[]{16,77});
    states[77] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-2,78,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[78] = new State(new int[]{17,79,12,13,13,24});
    states[79] = new State(new int[]{43,28,3,4,5,72,28,76,24,84,21,99,26,106,27,109,4,-14,10,-14,23,-14},new int[]{-10,80,-11,9,-3,10,-24,69,-22,70,-12,71,-16,75,-17,81,-18,82,-19,83,-14,98,-15,105,-21,108,-13,113,-6,114});
    states[80] = new State(-51);
    states[81] = new State(-8);
    states[82] = new State(-52);
    states[83] = new State(-53);
    states[84] = new State(new int[]{43,28},new int[]{-3,85});
    states[85] = new State(new int[]{16,86});
    states[86] = new State(new int[]{43,28,17,-14,18,-14},new int[]{-25,87,-20,96,-3,94,-13,97});
    states[87] = new State(new int[]{17,88,18,92});
    states[88] = new State(new int[]{25,89});
    states[89] = new State(new int[]{43,28},new int[]{-3,90});
    states[90] = new State(new int[]{3,4},new int[]{-24,91});
    states[91] = new State(-56);
    states[92] = new State(new int[]{43,28},new int[]{-20,93,-3,94});
    states[93] = new State(-58);
    states[94] = new State(new int[]{43,28},new int[]{-3,95});
    states[95] = new State(-60);
    states[96] = new State(-57);
    states[97] = new State(-59);
    states[98] = new State(-9);
    states[99] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-2,100,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[100] = new State(new int[]{22,101,12,13,13,24});
    states[101] = new State(new int[]{43,28,3,4,5,72,28,76,24,84,21,99,26,106,27,109,4,-14,10,-14,23,-14},new int[]{-10,102,-11,9,-3,10,-24,69,-22,70,-12,71,-16,75,-17,81,-18,82,-19,83,-14,98,-15,105,-21,108,-13,113,-6,114});
    states[102] = new State(new int[]{23,103,4,-46,10,-46});
    states[103] = new State(new int[]{43,28,3,4,5,72,28,76,24,84,21,99,26,106,27,109,4,-14,10,-14,23,-14},new int[]{-10,104,-11,9,-3,10,-24,69,-22,70,-12,71,-16,75,-17,81,-18,82,-19,83,-14,98,-15,105,-21,108,-13,113,-6,114});
    states[104] = new State(-47);
    states[105] = new State(-10);
    states[106] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62,4,-62,10,-62,23,-62},new int[]{-2,107,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[107] = new State(new int[]{12,13,13,24,4,-61,10,-61,23,-61});
    states[108] = new State(-11);
    states[109] = new State(new int[]{16,110});
    states[110] = new State(new int[]{43,28,41,29,42,30,16,31,31,40,30,41,40,62},new int[]{-2,111,-9,34,-4,35,-5,60,-3,17,-6,38,-27,39,-8,61});
    states[111] = new State(new int[]{17,112,12,13,13,24});
    states[112] = new State(-15);
    states[113] = new State(-12);
    states[114] = new State(-13);
    states[115] = new State(-4);

    rules[1] = new Rule(-28, new int[]{-1,2});
    rules[2] = new Rule(-1, new int[]{-24});
    rules[3] = new Rule(-23, new int[]{-23,10,-10});
    rules[4] = new Rule(-23, new int[]{-10});
    rules[5] = new Rule(-10, new int[]{-11});
    rules[6] = new Rule(-10, new int[]{-24});
    rules[7] = new Rule(-10, new int[]{-22});
    rules[8] = new Rule(-10, new int[]{-17});
    rules[9] = new Rule(-10, new int[]{-14});
    rules[10] = new Rule(-10, new int[]{-15});
    rules[11] = new Rule(-10, new int[]{-21});
    rules[12] = new Rule(-10, new int[]{-13});
    rules[13] = new Rule(-10, new int[]{-6});
    rules[14] = new Rule(-13, new int[]{});
    rules[15] = new Rule(-21, new int[]{27,16,-2,17});
    rules[16] = new Rule(-3, new int[]{43});
    rules[17] = new Rule(-11, new int[]{-3,6,-2});
    rules[18] = new Rule(-2, new int[]{-2,12,-4});
    rules[19] = new Rule(-2, new int[]{-2,13,-4});
    rules[20] = new Rule(-2, new int[]{-9});
    rules[21] = new Rule(-9, new int[]{-4,38,-5});
    rules[22] = new Rule(-9, new int[]{-4,39,-5});
    rules[23] = new Rule(-9, new int[]{40,-4});
    rules[24] = new Rule(-9, new int[]{-4});
    rules[25] = new Rule(-8, new int[]{-4,32,-5});
    rules[26] = new Rule(-8, new int[]{-4,33,-5});
    rules[27] = new Rule(-8, new int[]{-4,34,-5});
    rules[28] = new Rule(-8, new int[]{-4,35,-5});
    rules[29] = new Rule(-8, new int[]{-4,36,-5});
    rules[30] = new Rule(-8, new int[]{-4,37,-5});
    rules[31] = new Rule(-4, new int[]{-4,14,-5});
    rules[32] = new Rule(-4, new int[]{-4,15,-5});
    rules[33] = new Rule(-4, new int[]{-4,20,-5});
    rules[34] = new Rule(-4, new int[]{-4,19,-5});
    rules[35] = new Rule(-4, new int[]{-5});
    rules[36] = new Rule(-4, new int[]{-8});
    rules[37] = new Rule(-5, new int[]{-3});
    rules[38] = new Rule(-5, new int[]{41});
    rules[39] = new Rule(-5, new int[]{42});
    rules[40] = new Rule(-5, new int[]{16,-2,17});
    rules[41] = new Rule(-5, new int[]{-6});
    rules[42] = new Rule(-5, new int[]{-27});
    rules[43] = new Rule(-27, new int[]{31});
    rules[44] = new Rule(-27, new int[]{30});
    rules[45] = new Rule(-24, new int[]{3,-23,4});
    rules[46] = new Rule(-14, new int[]{21,-2,22,-10});
    rules[47] = new Rule(-14, new int[]{21,-2,22,-10,23,-10});
    rules[48] = new Rule(-22, new int[]{-12});
    rules[49] = new Rule(-22, new int[]{-16});
    rules[50] = new Rule(-12, new int[]{5,-2,-10});
    rules[51] = new Rule(-16, new int[]{28,16,-2,17,-10});
    rules[52] = new Rule(-17, new int[]{-18});
    rules[53] = new Rule(-17, new int[]{-19});
    rules[54] = new Rule(-18, new int[]{-3,-3});
    rules[55] = new Rule(-18, new int[]{-3,-3,6,-2});
    rules[56] = new Rule(-19, new int[]{24,-3,16,-25,17,25,-3,-24});
    rules[57] = new Rule(-25, new int[]{-20});
    rules[58] = new Rule(-25, new int[]{-25,18,-20});
    rules[59] = new Rule(-25, new int[]{-13});
    rules[60] = new Rule(-20, new int[]{-3,-3});
    rules[61] = new Rule(-15, new int[]{26,-2});
    rules[62] = new Rule(-15, new int[]{26});
    rules[63] = new Rule(-6, new int[]{-3,16,-26,17});
    rules[64] = new Rule(-26, new int[]{-7});
    rules[65] = new Rule(-26, new int[]{-26,18,-7});
    rules[66] = new Rule(-26, new int[]{-13});
    rules[67] = new Rule(-7, new int[]{-2});
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
      case 7: // statement -> loop
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
      case 13: // statement -> func_call
{CurrentSemanticValue.stVal = new FuncCallStmntNode(ValueStack[ValueStack.Depth-1].eVal as FuncCallNode); }
        break;
      case 14: // empty -> /* empty */
{ CurrentSemanticValue.stVal = new EmptyNode(); }
        break;
      case 15: // write -> WRITE, LPAREN, expr, RPAREN
{ CurrentSemanticValue.stVal = new WriteNode(ValueStack[ValueStack.Depth-2].eVal); }
        break;
      case 16: // ident -> ID
{CurrentSemanticValue.eVal = new IdNode(ValueStack[ValueStack.Depth-1].sVal);}
        break;
      case 17: // assign -> ident, ASSIGN, expr
{ CurrentSemanticValue.stVal = new AssignNode(ValueStack[ValueStack.Depth-3].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal); }
        break;
      case 18: // expr -> expr, PLUS, T
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"+"); }
        break;
      case 19: // expr -> expr, MINUS, T
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"-"); }
        break;
      case 20: // expr -> logic_expr
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal; }
        break;
      case 21: // logic_expr -> T, OR, F
{CurrentSemanticValue.eVal = new LogicBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "||");}
        break;
      case 22: // logic_expr -> T, AND, F
{CurrentSemanticValue.eVal = new LogicBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "&&");}
        break;
      case 23: // logic_expr -> NOT, T
{CurrentSemanticValue.eVal = new UnaryOpNode(ValueStack[ValueStack.Depth-1].eVal, "!");}
        break;
      case 24: // logic_expr -> T
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 25: // bool_expr -> T, L, F
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "<");}
        break;
      case 26: // bool_expr -> T, LE, F
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "<=");}
        break;
      case 27: // bool_expr -> T, G, F
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, ">");}
        break;
      case 28: // bool_expr -> T, GE, F
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, ">=");}
        break;
      case 29: // bool_expr -> T, EQ, F
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "==");}
        break;
      case 30: // bool_expr -> T, NEQ, F
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "!=");}
        break;
      case 31: // T -> T, MULT, F
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"*"); }
        break;
      case 32: // T -> T, DIV, F
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"/"); }
        break;
      case 33: // T -> T, MOD, F
{CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"%");}
        break;
      case 34: // T -> T, DIVI, F
{CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"//");}
        break;
      case 35: // T -> F
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal; }
        break;
      case 36: // T -> bool_expr
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 37: // F -> ident
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal as IdNode; }
        break;
      case 38: // F -> INUM
{ CurrentSemanticValue.eVal = new IntNumNode(ValueStack[ValueStack.Depth-1].iVal); }
        break;
      case 39: // F -> RNUM
{CurrentSemanticValue.eVal = new RealNumNode(ValueStack[ValueStack.Depth-1].dVal);}
        break;
      case 40: // F -> LPAREN, expr, RPAREN
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-2].eVal; }
        break;
      case 41: // F -> func_call
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 42: // F -> bool_literal
{CurrentSemanticValue.eVal = new BoolNode(ValueStack[ValueStack.Depth-1].bVal);}
        break;
      case 43: // bool_literal -> TRUE
{CurrentSemanticValue.bVal = true;}
        break;
      case 44: // bool_literal -> FALSE
{CurrentSemanticValue.bVal = false;}
        break;
      case 45: // block -> BEGIN, stlist, END
{ CurrentSemanticValue.blVal = ValueStack[ValueStack.Depth-2].blVal; }
        break;
      case 46: // if -> IF, expr, THEN, statement
{CurrentSemanticValue.stVal  = new IfNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].stVal, null);}
        break;
      case 47: // if -> IF, expr, THEN, statement, ELSE, statement
{CurrentSemanticValue.stVal = new IfNode(ValueStack[ValueStack.Depth-5].eVal, ValueStack[ValueStack.Depth-3].stVal, ValueStack[ValueStack.Depth-1].stVal);}
        break;
      case 48: // loop -> cycle
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 49: // loop -> while
{CurrentSemanticValue.stVal =ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 50: // cycle -> CYCLE, expr, statement
{ CurrentSemanticValue.stVal = new CycleNode(ValueStack[ValueStack.Depth-2].eVal,ValueStack[ValueStack.Depth-1].stVal); }
        break;
      case 52: // declaration -> var_decl
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 53: // declaration -> func_decl
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 54: // var_decl -> ident, ident
{CurrentSemanticValue.stVal =  new VarNode(ValueStack[ValueStack.Depth-2].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal as IdNode, null);}
        break;
      case 55: // var_decl -> ident, ident, ASSIGN, expr
{CurrentSemanticValue.stVal =  new VarNode(ValueStack[ValueStack.Depth-4].eVal as IdNode, ValueStack[ValueStack.Depth-3].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal);}
        break;
      case 56: // func_decl -> FUN, ident, LPAREN, params, RPAREN, COLON, ident, block
{CurrentSemanticValue.stVal = new FuncNode(ValueStack[ValueStack.Depth-7].eVal as IdNode, ValueStack[ValueStack.Depth-5].parList, ValueStack[ValueStack.Depth-2].eVal as IdNode,new FuncBodyNode(ValueStack[ValueStack.Depth-1].blVal));}
        break;
      case 57: // params -> param
{CurrentSemanticValue.parList = new List<ParamNode>(); CurrentSemanticValue.parList.Add(ValueStack[ValueStack.Depth-1].stVal as ParamNode);}
        break;
      case 58: // params -> params, COLUMN, param
{CurrentSemanticValue.parList = ValueStack[ValueStack.Depth-3].parList; CurrentSemanticValue.parList.Add(ValueStack[ValueStack.Depth-1].stVal as ParamNode);}
        break;
      case 59: // params -> empty
{CurrentSemanticValue.parList = new List<ParamNode>();}
        break;
      case 60: // param -> ident, ident
{CurrentSemanticValue.stVal = new ParamNode(ValueStack[ValueStack.Depth-2].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal as IdNode);}
        break;
      case 61: // return -> RETURN, expr
{CurrentSemanticValue.stVal = new ReturnNode(ValueStack[ValueStack.Depth-1].eVal);}
        break;
      case 62: // return -> RETURN
{CurrentSemanticValue.stVal = new ReturnNode(null);}
        break;
      case 63: // func_call -> ident, LPAREN, valueParams, RPAREN
{CurrentSemanticValue.eVal = new FuncCallNode(ValueStack[ValueStack.Depth-4].eVal as IdNode, ValueStack[ValueStack.Depth-2].exprList);}
        break;
      case 64: // valueParams -> valueParam
{CurrentSemanticValue.exprList = new List<ExprNode>(); CurrentSemanticValue.exprList.Add(ValueStack[ValueStack.Depth-1].eVal);}
        break;
      case 65: // valueParams -> valueParams, COLUMN, valueParam
{CurrentSemanticValue.exprList = ValueStack[ValueStack.Depth-3].exprList; CurrentSemanticValue.exprList.Add(ValueStack[ValueStack.Depth-1].eVal);}
        break;
      case 66: // valueParams -> empty
{CurrentSemanticValue.exprList = new List<ExprNode>();}
        break;
      case 67: // valueParam -> expr
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
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
