using System;
using System.IO;
using SimpleScanner;
using SimpleParser;
using SimpleLang.Visitors;

namespace SimpleCompiler
{

    public class SimpleCompilerMain
    {
        public static void Main()
        {
           
            string FileName = @"a.txt";
            try
            {
                Compile(FileName);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл {0} не найден", FileName);
            }
            catch (CompilerException e )
            {
                Console.WriteLine("Строка:{0}: {1}",e.location.StartLine, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
            }

            Console.ReadLine();
        }

        private static void Compile(string FileName)
        {
            string Text = File.ReadAllText(FileName);

            Scanner scanner = new Scanner();
            scanner.SetSource(Text, 0);

            Parser parser = new(scanner);

            var b = parser.Parse();
            if (!b)
                Console.WriteLine("Ошибка");
            else
            {
                Console.WriteLine("Синтаксическое дерево построено");
                var table_creator = new SymbolTableCreatorVisitor();
                parser.root.Invite(table_creator);

                var avis = new AssignCountVisitor();
                parser.root.Invite(avis);
                Console.WriteLine("Количество присваиваний = {0}", avis.Count);
                Console.WriteLine("-------------------------------");



                var pp = new PrettyPrintVisitor();
                parser.root.Invite(pp);
                Console.WriteLine(pp.Text);
                Console.WriteLine("-------------------------------");



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
    }
}
