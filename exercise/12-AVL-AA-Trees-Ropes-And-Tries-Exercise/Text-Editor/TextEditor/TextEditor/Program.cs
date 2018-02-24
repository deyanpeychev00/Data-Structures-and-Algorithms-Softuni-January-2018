using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {

        ITextEditor editor = new Text_Editor();

        string line = string.Empty;
        Regex splitterRegex = new Regex("\"(.*)\"");
        Dictionary<string, bool> users = new Dictionary<string, bool>();

        while ((line = Console.ReadLine()) != "end")
        {
            Match quotesMatch = splitterRegex.Match(line);
            string[] commands = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                switch (commands[0])
                {
                    case "login":
                        {
                            users[commands[1]] = true;
                            editor.Login(commands[1]);
                            break;
                        }
                    case "logout":
                        {
                            users[commands[1]] = false;
                            editor.Logout(commands[1]);
                            break;
                        }
                    case "users":
                        {
                            if (commands.Length > 1)
                            {
                                editor.Users();
                                break;
                            }
                            editor.Users();
                            break;
                        }
                }

                if (!users.ContainsKey(commands[0]) && users[commands[0]])
                {
                    continue;
                }

                // commands[0] stand mainly for username
                switch (commands[1])
                {
                    case "insert":
                        {
                            editor.Insert(commands[0], int.Parse(commands[2]), quotesMatch.Groups[1].Value);
                            break;
                        }
                    case "prepend":
                        {
                            editor.Prepend(commands[0], quotesMatch.Groups[1].Value);
                            break;
                        }
                    case "substring":
                        {
                            editor.Substring(commands[0], int.Parse(commands[2]), int.Parse(commands[3]));
                            break;
                        }
                    case "delete":
                        {
                            editor.Delete(commands[0], int.Parse(commands[2]), int.Parse(commands[3]));
                            break;
                        }
                    case "clear":
                        {
                            editor.Clear(commands[0]);
                            break;
                        }
                    case "length":
                        {
                            Console.WriteLine(editor.Length(commands[0]));
                            break;
                        }
                    case "print":
                        {
                            Console.WriteLine(editor.Print(commands[0]));
                            break;
                        }
                    case "undo":
                        {
                            editor.Undo(commands[0]);
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

