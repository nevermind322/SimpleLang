using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using SimpleScanner;
using SimpleParser;
using SimpleLang.Visitors;
using System.Xml.Linq;
using ProgramTree;

namespace SimpleCompiler
{

    public class SimpleCompilerMain
    {
        public static void Main()
        {
           
            string FileName = @"..\..\a.txt";

            try
            {
                string Text = File.ReadAllText(FileName);

                Scanner scanner = new Scanner();
                scanner.SetSource(Text, 0);

                Parser parser = new Parser(scanner);

                var b = parser.Parse();
                if (!b)
                    Console.WriteLine("Ошибка");
                else
                {
                    Console.WriteLine("Синтаксическое дерево построено");

                    var avis = new AssignCountVisitor();
                    parser.root.Invite(avis);
                    Console.WriteLine("Количество присваиваний = {0}", avis.Count);
                    Console.WriteLine("-------------------------------");

                       
                   
                    var pp = new PrettyPrintVisitor();
                    parser.root.Invite(pp);
                    Console.WriteLine(pp.Text);
                    Console.WriteLine("-------------------------------");
                    

                    var scv = new SymbolCreatorVisitor();
                    parser.root.Invite(scv); 
                    
                    var tc = new TypeCheckingVisitor();
                    parser.root.Invite(tc);

                    var code = new GenCodeVisitor();
                    parser.root.Invite(code);
                    code.EndProgram();
                    code.PrintCommands();
                    Console.WriteLine("-------------------------------");

                    code.RunProgram();
                    Console.WriteLine("End");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл {0} не найден", FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
            }

            Console.ReadLine();
        }


    }
}
