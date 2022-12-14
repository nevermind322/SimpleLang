// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.3.6
// Machine:  LAPTOP-9M8I2R2U
// DateTime: 04.12.2022 17:38:56
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
  private static Rule[] rules = new Rule[70];
  private static State[] states = new State[118];
  private static string[] nonTerms = new string[] {
      "progr", "expr", "ident", "T", "F", "func_call", "valueParam", "bool_expr", 
      "disjunction", "conjunction", "additive_expr", "statement", "assign", "cycle", 
      "empty", "if", "return", "while", "declaration", "var_decl", "func_decl", 
      "param", "write", "loop", "stlist", "block", "params", "valueParams", "bool_literal", 
      "$accept", };

  static Parser() {
    states[0] = new State(new int[]{3,4},new int[]{-1,1,-26,3});
    states[1] = new State(new int[]{2,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{43,39,3,4,5,74,28,78,24,86,21,101,26,108,27,111,4,-14,10,-14},new int[]{-25,5,-12,117,-13,9,-3,10,-26,71,-24,72,-14,73,-18,77,-19,83,-20,84,-21,85,-16,100,-17,107,-23,110,-15,115,-6,116});
    states[5] = new State(new int[]{4,6,10,7});
    states[6] = new State(-47);
    states[7] = new State(new int[]{43,39,3,4,5,74,28,78,24,86,21,101,26,108,27,111,4,-14,10,-14},new int[]{-12,8,-13,9,-3,10,-26,71,-24,72,-14,73,-18,77,-19,83,-20,84,-21,85,-16,100,-17,107,-23,110,-15,115,-6,116});
    states[8] = new State(-3);
    states[9] = new State(-5);
    states[10] = new State(new int[]{6,11,16,25,43,39},new int[]{-3,68});
    states[11] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-2,12,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[12] = new State(-17);
    states[13] = new State(new int[]{38,14,4,-18,10,-18,23,-18,17,-18,18,-18,43,-18,3,-18,5,-18,28,-18,24,-18,21,-18,26,-18,27,-18,22,-18});
    states[14] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-10,15,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[15] = new State(new int[]{39,16,38,-19,4,-19,10,-19,23,-19,17,-19,18,-19,43,-19,3,-19,5,-19,28,-19,24,-19,21,-19,26,-19,27,-19,22,-19});
    states[16] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-8,17,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[17] = new State(new int[]{32,18,33,33,34,58,35,60,36,62,37,64,39,-21,38,-21,4,-21,10,-21,23,-21,17,-21,18,-21,43,-21,3,-21,5,-21,28,-21,24,-21,21,-21,26,-21,27,-21,22,-21});
    states[18] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-11,19,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[19] = new State(new int[]{12,20,13,35,32,-26,33,-26,34,-26,35,-26,36,-26,37,-26,39,-26,38,-26,4,-26,10,-26,23,-26,17,-26,18,-26,43,-26,3,-26,5,-26,28,-26,24,-26,21,-26,26,-26,27,-26,22,-26});
    states[20] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-4,21,-5,57,-3,24,-6,49,-29,50});
    states[21] = new State(new int[]{14,22,15,37,20,47,19,55,12,-23,13,-23,32,-23,33,-23,34,-23,35,-23,36,-23,37,-23,39,-23,38,-23,4,-23,10,-23,23,-23,17,-23,18,-23,43,-23,3,-23,5,-23,28,-23,24,-23,21,-23,26,-23,27,-23,22,-23});
    states[22] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-5,23,-3,24,-6,49,-29,50});
    states[23] = new State(-33);
    states[24] = new State(new int[]{16,25,14,-38,15,-38,20,-38,19,-38,12,-38,13,-38,32,-38,33,-38,34,-38,35,-38,36,-38,37,-38,39,-38,38,-38,4,-38,10,-38,23,-38,17,-38,18,-38,43,-38,3,-38,5,-38,28,-38,24,-38,21,-38,26,-38,27,-38,22,-38});
    states[25] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53,17,-14,18,-14},new int[]{-28,26,-7,66,-2,30,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50,-15,67});
    states[26] = new State(new int[]{17,27,18,28});
    states[27] = new State(-65);
    states[28] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-7,29,-2,30,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[29] = new State(-67);
    states[30] = new State(-69);
    states[31] = new State(new int[]{39,16,38,-20,4,-20,10,-20,23,-20,17,-20,18,-20,43,-20,3,-20,5,-20,28,-20,24,-20,21,-20,26,-20,27,-20,22,-20});
    states[32] = new State(new int[]{32,18,33,33,34,58,35,60,36,62,37,64,39,-22,38,-22,4,-22,10,-22,23,-22,17,-22,18,-22,43,-22,3,-22,5,-22,28,-22,24,-22,21,-22,26,-22,27,-22,22,-22});
    states[33] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-11,34,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[34] = new State(new int[]{12,20,13,35,32,-27,33,-27,34,-27,35,-27,36,-27,37,-27,39,-27,38,-27,4,-27,10,-27,23,-27,17,-27,18,-27,43,-27,3,-27,5,-27,28,-27,24,-27,21,-27,26,-27,27,-27,22,-27});
    states[35] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-4,36,-5,57,-3,24,-6,49,-29,50});
    states[36] = new State(new int[]{14,22,15,37,20,47,19,55,12,-24,13,-24,32,-24,33,-24,34,-24,35,-24,36,-24,37,-24,39,-24,38,-24,4,-24,10,-24,23,-24,17,-24,18,-24,43,-24,3,-24,5,-24,28,-24,24,-24,21,-24,26,-24,27,-24,22,-24});
    states[37] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-5,38,-3,24,-6,49,-29,50});
    states[38] = new State(-34);
    states[39] = new State(-16);
    states[40] = new State(-39);
    states[41] = new State(-40);
    states[42] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-2,43,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[43] = new State(new int[]{17,44});
    states[44] = new State(-41);
    states[45] = new State(new int[]{12,20,13,35,32,-32,33,-32,34,-32,35,-32,36,-32,37,-32,39,-32,38,-32,4,-32,10,-32,23,-32,17,-32,18,-32,43,-32,3,-32,5,-32,28,-32,24,-32,21,-32,26,-32,27,-32,22,-32});
    states[46] = new State(new int[]{14,22,15,37,20,47,19,55,12,-25,13,-25,32,-25,33,-25,34,-25,35,-25,36,-25,37,-25,39,-25,38,-25,4,-25,10,-25,23,-25,17,-25,18,-25,43,-25,3,-25,5,-25,28,-25,24,-25,21,-25,26,-25,27,-25,22,-25});
    states[47] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-5,48,-3,24,-6,49,-29,50});
    states[48] = new State(-35);
    states[49] = new State(-42);
    states[50] = new State(-43);
    states[51] = new State(-45);
    states[52] = new State(-46);
    states[53] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-5,54,-3,24,-6,49,-29,50});
    states[54] = new State(-44);
    states[55] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-5,56,-3,24,-6,49,-29,50});
    states[56] = new State(-36);
    states[57] = new State(-37);
    states[58] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-11,59,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[59] = new State(new int[]{12,20,13,35,32,-28,33,-28,34,-28,35,-28,36,-28,37,-28,39,-28,38,-28,4,-28,10,-28,23,-28,17,-28,18,-28,43,-28,3,-28,5,-28,28,-28,24,-28,21,-28,26,-28,27,-28,22,-28});
    states[60] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-11,61,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[61] = new State(new int[]{12,20,13,35,32,-29,33,-29,34,-29,35,-29,36,-29,37,-29,39,-29,38,-29,4,-29,10,-29,23,-29,17,-29,18,-29,43,-29,3,-29,5,-29,28,-29,24,-29,21,-29,26,-29,27,-29,22,-29});
    states[62] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-11,63,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[63] = new State(new int[]{12,20,13,35,32,-30,33,-30,34,-30,35,-30,36,-30,37,-30,39,-30,38,-30,4,-30,10,-30,23,-30,17,-30,18,-30,43,-30,3,-30,5,-30,28,-30,24,-30,21,-30,26,-30,27,-30,22,-30});
    states[64] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-11,65,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[65] = new State(new int[]{12,20,13,35,32,-31,33,-31,34,-31,35,-31,36,-31,37,-31,39,-31,38,-31,4,-31,10,-31,23,-31,17,-31,18,-31,43,-31,3,-31,5,-31,28,-31,24,-31,21,-31,26,-31,27,-31,22,-31});
    states[66] = new State(-66);
    states[67] = new State(-68);
    states[68] = new State(new int[]{6,69,4,-56,10,-56,23,-56});
    states[69] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-2,70,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[70] = new State(-57);
    states[71] = new State(-6);
    states[72] = new State(-7);
    states[73] = new State(-50);
    states[74] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-2,75,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[75] = new State(new int[]{43,39,3,4,5,74,28,78,24,86,21,101,26,108,27,111,4,-14,10,-14,23,-14},new int[]{-12,76,-13,9,-3,10,-26,71,-24,72,-14,73,-18,77,-19,83,-20,84,-21,85,-16,100,-17,107,-23,110,-15,115,-6,116});
    states[76] = new State(-52);
    states[77] = new State(-51);
    states[78] = new State(new int[]{16,79});
    states[79] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-2,80,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[80] = new State(new int[]{17,81});
    states[81] = new State(new int[]{43,39,3,4,5,74,28,78,24,86,21,101,26,108,27,111,4,-14,10,-14,23,-14},new int[]{-12,82,-13,9,-3,10,-26,71,-24,72,-14,73,-18,77,-19,83,-20,84,-21,85,-16,100,-17,107,-23,110,-15,115,-6,116});
    states[82] = new State(-53);
    states[83] = new State(-8);
    states[84] = new State(-54);
    states[85] = new State(-55);
    states[86] = new State(new int[]{43,39},new int[]{-3,87});
    states[87] = new State(new int[]{16,88});
    states[88] = new State(new int[]{43,39,17,-14,18,-14},new int[]{-27,89,-22,98,-3,96,-15,99});
    states[89] = new State(new int[]{17,90,18,94});
    states[90] = new State(new int[]{25,91});
    states[91] = new State(new int[]{43,39},new int[]{-3,92});
    states[92] = new State(new int[]{3,4},new int[]{-26,93});
    states[93] = new State(-58);
    states[94] = new State(new int[]{43,39},new int[]{-22,95,-3,96});
    states[95] = new State(-60);
    states[96] = new State(new int[]{43,39},new int[]{-3,97});
    states[97] = new State(-62);
    states[98] = new State(-59);
    states[99] = new State(-61);
    states[100] = new State(-9);
    states[101] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-2,102,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[102] = new State(new int[]{22,103});
    states[103] = new State(new int[]{43,39,3,4,5,74,28,78,24,86,21,101,26,108,27,111,4,-14,10,-14,23,-14},new int[]{-12,104,-13,9,-3,10,-26,71,-24,72,-14,73,-18,77,-19,83,-20,84,-21,85,-16,100,-17,107,-23,110,-15,115,-6,116});
    states[104] = new State(new int[]{23,105,4,-48,10,-48});
    states[105] = new State(new int[]{43,39,3,4,5,74,28,78,24,86,21,101,26,108,27,111,4,-14,10,-14,23,-14},new int[]{-12,106,-13,9,-3,10,-26,71,-24,72,-14,73,-18,77,-19,83,-20,84,-21,85,-16,100,-17,107,-23,110,-15,115,-6,116});
    states[106] = new State(-49);
    states[107] = new State(-10);
    states[108] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53,4,-64,10,-64,23,-64},new int[]{-2,109,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[109] = new State(-63);
    states[110] = new State(-11);
    states[111] = new State(new int[]{16,112});
    states[112] = new State(new int[]{43,39,41,40,42,41,16,42,31,51,30,52,40,53},new int[]{-2,113,-9,13,-10,31,-8,32,-11,45,-4,46,-5,57,-3,24,-6,49,-29,50});
    states[113] = new State(new int[]{17,114});
    states[114] = new State(-15);
    states[115] = new State(-12);
    states[116] = new State(-13);
    states[117] = new State(-4);

    rules[1] = new Rule(-30, new int[]{-1,2});
    rules[2] = new Rule(-1, new int[]{-26});
    rules[3] = new Rule(-25, new int[]{-25,10,-12});
    rules[4] = new Rule(-25, new int[]{-12});
    rules[5] = new Rule(-12, new int[]{-13});
    rules[6] = new Rule(-12, new int[]{-26});
    rules[7] = new Rule(-12, new int[]{-24});
    rules[8] = new Rule(-12, new int[]{-19});
    rules[9] = new Rule(-12, new int[]{-16});
    rules[10] = new Rule(-12, new int[]{-17});
    rules[11] = new Rule(-12, new int[]{-23});
    rules[12] = new Rule(-12, new int[]{-15});
    rules[13] = new Rule(-12, new int[]{-6});
    rules[14] = new Rule(-15, new int[]{});
    rules[15] = new Rule(-23, new int[]{27,16,-2,17});
    rules[16] = new Rule(-3, new int[]{43});
    rules[17] = new Rule(-13, new int[]{-3,6,-2});
    rules[18] = new Rule(-2, new int[]{-9});
    rules[19] = new Rule(-9, new int[]{-9,38,-10});
    rules[20] = new Rule(-9, new int[]{-10});
    rules[21] = new Rule(-10, new int[]{-10,39,-8});
    rules[22] = new Rule(-10, new int[]{-8});
    rules[23] = new Rule(-11, new int[]{-11,12,-4});
    rules[24] = new Rule(-11, new int[]{-11,13,-4});
    rules[25] = new Rule(-11, new int[]{-4});
    rules[26] = new Rule(-8, new int[]{-8,32,-11});
    rules[27] = new Rule(-8, new int[]{-8,33,-11});
    rules[28] = new Rule(-8, new int[]{-8,34,-11});
    rules[29] = new Rule(-8, new int[]{-8,35,-11});
    rules[30] = new Rule(-8, new int[]{-8,36,-11});
    rules[31] = new Rule(-8, new int[]{-8,37,-11});
    rules[32] = new Rule(-8, new int[]{-11});
    rules[33] = new Rule(-4, new int[]{-4,14,-5});
    rules[34] = new Rule(-4, new int[]{-4,15,-5});
    rules[35] = new Rule(-4, new int[]{-4,20,-5});
    rules[36] = new Rule(-4, new int[]{-4,19,-5});
    rules[37] = new Rule(-4, new int[]{-5});
    rules[38] = new Rule(-5, new int[]{-3});
    rules[39] = new Rule(-5, new int[]{41});
    rules[40] = new Rule(-5, new int[]{42});
    rules[41] = new Rule(-5, new int[]{16,-2,17});
    rules[42] = new Rule(-5, new int[]{-6});
    rules[43] = new Rule(-5, new int[]{-29});
    rules[44] = new Rule(-5, new int[]{40,-5});
    rules[45] = new Rule(-29, new int[]{31});
    rules[46] = new Rule(-29, new int[]{30});
    rules[47] = new Rule(-26, new int[]{3,-25,4});
    rules[48] = new Rule(-16, new int[]{21,-2,22,-12});
    rules[49] = new Rule(-16, new int[]{21,-2,22,-12,23,-12});
    rules[50] = new Rule(-24, new int[]{-14});
    rules[51] = new Rule(-24, new int[]{-18});
    rules[52] = new Rule(-14, new int[]{5,-2,-12});
    rules[53] = new Rule(-18, new int[]{28,16,-2,17,-12});
    rules[54] = new Rule(-19, new int[]{-20});
    rules[55] = new Rule(-19, new int[]{-21});
    rules[56] = new Rule(-20, new int[]{-3,-3});
    rules[57] = new Rule(-20, new int[]{-3,-3,6,-2});
    rules[58] = new Rule(-21, new int[]{24,-3,16,-27,17,25,-3,-26});
    rules[59] = new Rule(-27, new int[]{-22});
    rules[60] = new Rule(-27, new int[]{-27,18,-22});
    rules[61] = new Rule(-27, new int[]{-15});
    rules[62] = new Rule(-22, new int[]{-3,-3});
    rules[63] = new Rule(-17, new int[]{26,-2});
    rules[64] = new Rule(-17, new int[]{26});
    rules[65] = new Rule(-6, new int[]{-3,16,-28,17});
    rules[66] = new Rule(-28, new int[]{-7});
    rules[67] = new Rule(-28, new int[]{-28,18,-7});
    rules[68] = new Rule(-28, new int[]{-15});
    rules[69] = new Rule(-7, new int[]{-2});
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
				CurrentSemanticValue.blVal = new BlockNode(ValueStack[ValueStack.Depth-1].stVal, CurrentLocationSpan); 
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
{CurrentSemanticValue.stVal = new FuncCallStmntNode(ValueStack[ValueStack.Depth-1].eVal as FuncCallNode, CurrentLocationSpan); }
        break;
      case 14: // empty -> /* empty */
{ CurrentSemanticValue.stVal = new EmptyNode( CurrentLocationSpan); }
        break;
      case 15: // write -> WRITE, LPAREN, expr, RPAREN
{ CurrentSemanticValue.stVal = new WriteNode(ValueStack[ValueStack.Depth-2].eVal, CurrentLocationSpan); }
        break;
      case 16: // ident -> ID
{CurrentSemanticValue.eVal = new IdNode(ValueStack[ValueStack.Depth-1].sVal, CurrentLocationSpan);}
        break;
      case 17: // assign -> ident, ASSIGN, expr
{ CurrentSemanticValue.stVal = new AssignNode(ValueStack[ValueStack.Depth-3].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal, CurrentLocationSpan); }
        break;
      case 18: // expr -> disjunction
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal; }
        break;
      case 19: // disjunction -> disjunction, OR, conjunction
{CurrentSemanticValue.eVal = new LogicBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "||", CurrentLocationSpan);}
        break;
      case 20: // disjunction -> conjunction
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 21: // conjunction -> conjunction, AND, bool_expr
{CurrentSemanticValue.eVal = new LogicBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "&&", CurrentLocationSpan);}
        break;
      case 22: // conjunction -> bool_expr
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 23: // additive_expr -> additive_expr, PLUS, T
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"+", CurrentLocationSpan); }
        break;
      case 24: // additive_expr -> additive_expr, MINUS, T
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"-", CurrentLocationSpan); }
        break;
      case 25: // additive_expr -> T
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 26: // bool_expr -> bool_expr, L, additive_expr
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "<", CurrentLocationSpan);}
        break;
      case 27: // bool_expr -> bool_expr, LE, additive_expr
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "<=", CurrentLocationSpan);}
        break;
      case 28: // bool_expr -> bool_expr, G, additive_expr
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, ">", CurrentLocationSpan);}
        break;
      case 29: // bool_expr -> bool_expr, GE, additive_expr
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, ">=", CurrentLocationSpan);}
        break;
      case 30: // bool_expr -> bool_expr, EQ, additive_expr
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "==", CurrentLocationSpan);}
        break;
      case 31: // bool_expr -> bool_expr, NEQ, additive_expr
{CurrentSemanticValue.eVal = new BoolBinOpNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].eVal, "!=", CurrentLocationSpan);}
        break;
      case 32: // bool_expr -> additive_expr
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 33: // T -> T, MULT, F
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"*", CurrentLocationSpan); }
        break;
      case 34: // T -> T, DIV, F
{ CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"/", CurrentLocationSpan); }
        break;
      case 35: // T -> T, MOD, F
{CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"%", CurrentLocationSpan);}
        break;
      case 36: // T -> T, DIVI, F
{CurrentSemanticValue.eVal = new BinOpNode(ValueStack[ValueStack.Depth-3].eVal,ValueStack[ValueStack.Depth-1].eVal,"//", CurrentLocationSpan);}
        break;
      case 37: // T -> F
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal; }
        break;
      case 38: // F -> ident
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal as IdNode; }
        break;
      case 39: // F -> INUM
{ CurrentSemanticValue.eVal = new IntNumNode(ValueStack[ValueStack.Depth-1].iVal, CurrentLocationSpan); }
        break;
      case 40: // F -> RNUM
{CurrentSemanticValue.eVal = new RealNumNode(ValueStack[ValueStack.Depth-1].dVal, CurrentLocationSpan);}
        break;
      case 41: // F -> LPAREN, expr, RPAREN
{ CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-2].eVal; }
        break;
      case 42: // F -> func_call
{CurrentSemanticValue.eVal = ValueStack[ValueStack.Depth-1].eVal;}
        break;
      case 43: // F -> bool_literal
{CurrentSemanticValue.eVal = new BoolNode(ValueStack[ValueStack.Depth-1].bVal, CurrentLocationSpan);}
        break;
      case 44: // F -> NOT, F
{CurrentSemanticValue.eVal = new UnaryOpNode(ValueStack[ValueStack.Depth-1].eVal, "!", CurrentLocationSpan);}
        break;
      case 45: // bool_literal -> TRUE
{CurrentSemanticValue.bVal = true;}
        break;
      case 46: // bool_literal -> FALSE
{CurrentSemanticValue.bVal = false;}
        break;
      case 47: // block -> BEGIN, stlist, END
{ CurrentSemanticValue.blVal = ValueStack[ValueStack.Depth-2].blVal; }
        break;
      case 48: // if -> IF, expr, THEN, statement
{CurrentSemanticValue.stVal  = new IfNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].stVal, null, CurrentLocationSpan);}
        break;
      case 49: // if -> IF, expr, THEN, statement, ELSE, statement
{CurrentSemanticValue.stVal = new IfNode(ValueStack[ValueStack.Depth-5].eVal, ValueStack[ValueStack.Depth-3].stVal, ValueStack[ValueStack.Depth-1].stVal, CurrentLocationSpan);}
        break;
      case 50: // loop -> cycle
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 51: // loop -> while
{CurrentSemanticValue.stVal =ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 52: // cycle -> CYCLE, expr, statement
{ CurrentSemanticValue.stVal = new CycleNode(ValueStack[ValueStack.Depth-2].eVal,ValueStack[ValueStack.Depth-1].stVal, CurrentLocationSpan); }
        break;
      case 53: // while -> WHILE, LPAREN, expr, RPAREN, statement
{CurrentSemanticValue.stVal = new WhileNode(ValueStack[ValueStack.Depth-3].eVal, ValueStack[ValueStack.Depth-1].stVal, CurrentLocationSpan);}
        break;
      case 54: // declaration -> var_decl
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 55: // declaration -> func_decl
{CurrentSemanticValue.stVal = ValueStack[ValueStack.Depth-1].stVal;}
        break;
      case 56: // var_decl -> ident, ident
{CurrentSemanticValue.stVal =  new VarNode(ValueStack[ValueStack.Depth-2].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal as IdNode, null, CurrentLocationSpan);}
        break;
      case 57: // var_decl -> ident, ident, ASSIGN, expr
{CurrentSemanticValue.stVal =  new VarNode(ValueStack[ValueStack.Depth-4].eVal as IdNode, ValueStack[ValueStack.Depth-3].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal, CurrentLocationSpan);}
        break;
      case 58: // func_decl -> FUN, ident, LPAREN, params, RPAREN, COLON, ident, block
{CurrentSemanticValue.stVal = new FuncNode(ValueStack[ValueStack.Depth-7].eVal as IdNode, ValueStack[ValueStack.Depth-5].parList, ValueStack[ValueStack.Depth-2].eVal as IdNode,new FuncBodyNode(ValueStack[ValueStack.Depth-1].blVal, CurrentLocationSpan), CurrentLocationSpan);}
        break;
      case 59: // params -> param
{CurrentSemanticValue.parList = new List<ParamNode>(); CurrentSemanticValue.parList.Add(ValueStack[ValueStack.Depth-1].stVal as ParamNode);}
        break;
      case 60: // params -> params, COLUMN, param
{CurrentSemanticValue.parList = ValueStack[ValueStack.Depth-3].parList; CurrentSemanticValue.parList.Add(ValueStack[ValueStack.Depth-1].stVal as ParamNode);}
        break;
      case 61: // params -> empty
{CurrentSemanticValue.parList = new List<ParamNode>();}
        break;
      case 62: // param -> ident, ident
{CurrentSemanticValue.stVal = new ParamNode(ValueStack[ValueStack.Depth-2].eVal as IdNode, ValueStack[ValueStack.Depth-1].eVal as IdNode, CurrentLocationSpan);}
        break;
      case 63: // return -> RETURN, expr
{CurrentSemanticValue.stVal = new ReturnNode(ValueStack[ValueStack.Depth-1].eVal, CurrentLocationSpan);}
        break;
      case 64: // return -> RETURN
{CurrentSemanticValue.stVal = new ReturnNode(null, CurrentLocationSpan);}
        break;
      case 65: // func_call -> ident, LPAREN, valueParams, RPAREN
{CurrentSemanticValue.eVal = new FuncCallNode(ValueStack[ValueStack.Depth-4].eVal as IdNode, ValueStack[ValueStack.Depth-2].exprList, CurrentLocationSpan);}
        break;
      case 66: // valueParams -> valueParam
{CurrentSemanticValue.exprList = new List<ExprNode>(); CurrentSemanticValue.exprList.Add(ValueStack[ValueStack.Depth-1].eVal);}
        break;
      case 67: // valueParams -> valueParams, COLUMN, valueParam
{CurrentSemanticValue.exprList = ValueStack[ValueStack.Depth-3].exprList; CurrentSemanticValue.exprList.Add(ValueStack[ValueStack.Depth-1].eVal);}
        break;
      case 68: // valueParams -> empty
{CurrentSemanticValue.exprList = new List<ExprNode>();}
        break;
      case 69: // valueParam -> expr
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
