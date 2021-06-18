using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        private static string current_passage = "landing_screen.txt";
        private static bool should_exit = false;
        private static ArrayList linked_passages = new ArrayList();

        static void Main(string[] args)
        {
            bool first = true;
            while (!Program.should_exit)
            {
                if (!first)
                {
                    var select = Console.ReadKey(true).KeyChar - '1';
                    if (Program.linked_passages[select] is string)
                    {
                        Program.current_passage = Program.linked_passages[select] as string;
                    }
                }
                HandlePassage(Program.current_passage);
                first = false;
            }
        }

        static void HandlePassage(string passage_text)
        {
            var lines_of_code = File.ReadAllLines(passage_text);
            Program.linked_passages.Clear();
            foreach (var line in lines_of_code)
            {
                var linked_entry = Regex.Match(line, @"\[(.*)\]").Groups[1];
                if (linked_entry.Success)
                {
                    if (linked_entry.ToString().Equals("end"))
                    {
                        Console.WriteLine("\n---- GAME OVER ----");
                        Program.should_exit = true;
                    }
                    else
                    {
                        Program.linked_passages.Add(linked_entry.ToString());
                    }
                    Console.WriteLine(Regex.Replace(line,@"\[.*\]", ""));
                }
                else
                {
                    Console.WriteLine(line);
                }
            }
            if (Program.linked_passages.Count == 0 && Program.should_exit == false)
            {
                Program.should_exit = true;
                Console.WriteLine("You Win!!!!");
            }
        }
    }
}