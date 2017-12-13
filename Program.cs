using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace OSOS
{
    class Program
    {
        static void Main(string[] args)
        {
            /*GC.Collect();
            GC.WaitForPendingFinalizers();*/
            string input = null;
            string directory = @"C:\Users\MeCano\";
            string[] splitedinput;
            string first = "", second = "", three = "";


            Directory.SetCurrentDirectory(directory);
            while (true)
            {
                input = Console.ReadLine();
                input = input.ToLower();
                input = input.Trim(); //remove the first space if exsit
                string temp = null;
                string pattern = "[ ]{1,}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";  // splite string into words and don't splite the words between " "
                splitedinput = Regex.Split(input, pattern);
                string command = splitedinput[0];
                if (splitedinput.Length > 1)
                { first = splitedinput[1]; }
                if (splitedinput.Length > 2)
                { second = splitedinput[2]; }
                if (splitedinput.Length > 3)
                { three = splitedinput[3]; }
                Console.WriteLine(first);
                Console.WriteLine(second);
                command = RemoveQoutes(command);
                first = RemoveQoutes(first);
                second = RemoveQoutes(second);
                Console.WriteLine(command);
                Console.WriteLine(first);
                Console.WriteLine(second);

                //char[] chsecond = splitedinput[2].ToCharArray();
                //char[] chfirst = splitedinput[1].ToCharArray();
                char[] chcomand = command.ToCharArray();
                if (chcomand[0] == 'c' && chcomand[1] == 'd' && ((chcomand[2] == '\\') || (chcomand[2] == '.')) && (chcomand[3] == '.'))
                {
                    temp = command;
                    command = "cd";
                }
                switch (command)
                {
                    case "cls":
                        Console.Clear();
                        break;
                    case "dir":
                        break;
                    case "cd":
                        //try {
                        if (first == "/d")
                        {
                            Directory.SetCurrentDirectory(second);
                            directory = second;
                            Console.Write(directory);
                        }
                        else if (temp == "cd..")
                        {
                            int ss = directory.LastIndexOf("\\");
                            directory = directory.Substring(0, ss); //lesaaaaa 
                        }
                        else if (temp == "cd\\")
                        {
                            if (temp[3] == 0)
                            {

                            }
                            else
                            {
                                temp = temp.Remove(0, 3);
                                Directory.SetCurrentDirectory(temp);
                            }
                        }
                        else
                        { }



                        //} 
                        //  catch (ArgumentOutOfRangeException)
                        //{
                        //    Console.WriteLine("Enter a valid Command");
                        //}

                        break;
                    case "color":
                        break;
                    case "copy":
                        break;
                    case "help":
                        break;
                    case "del":
                        break;
                    case "exit":
                        {
                            Environment.Exit(0);
                        }
                        break;
                    case "type":
                        break;
                    case "rd":
                        {
                            char[] ch = first.ToCharArray();
                            string path;
                            if (ch[1] == ':')
                            {
                                path = first;
                            }
                            else
                            {
                                path = directory + first;
                            }
                            if (splitedinput.Length < 3)
                            {
                                try
                                {
                                    Directory.Delete(path);
                                }
                                catch (IOException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            else if (splitedinput.Length > 2)
                            {
                                char[] ch2 = second.ToCharArray();
                                if (ch2[0] == '/' && (ch2[1] == 's'))
                                {
                                    Console.WriteLine(first + " Are you sure(Y / N)");
                                    String inp = Console.ReadLine();
                                    if (inp == "y")
                                    {
                                        try
                                        {
                                            Directory.Delete(path, true);
                                        }

                                        catch (IOException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                    }
                                    else if (inp == "n")
                                    {
                                        Console.Write(directory);
                                    }
                                }

                                else if (ch2[0] == '/' && (ch2[1] == 'q' || ch2[1] == 'Q'))
                                {
                                    try
                                    {
                                        Directory.Delete(path);
                                    }
                                    catch (IOException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }

                            }


                        }
                        break;
                    case "md":
                        {

                            string path;
                            char[] ch = splitedinput[1].ToCharArray();
                            if (ch[1] == ':')
                            {
                                path = first;
                            }
                            else
                            {
                                path = directory + first;
                            }

                            try
                            {

                                if (Directory.Exists(path))
                                {
                                    Console.WriteLine("That path exists already.");
                                    continue;
                                }

                                DirectoryInfo di = Directory.CreateDirectory(path);
                                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("The process failed: {0}", e.ToString());
                            }
                            finally { }

                        }
                        break;
                    case "ren":
                        {
                            int index = first.LastIndexOf('\\') + 1;              // index the last slash in the second split
                            string d = first.Substring(0, index);                // return all words befor the last slash 
                            string thenew = d + second;                                     //but the new name a
                            char[] array = first.ToCharArray();

                            //Console.WriteLine(second);
                            //Console.WriteLine(splitedinput[1]);
                            //Console.WriteLine(d + splitedinput[2]);
                            if (array[1] == ':')
                            {
                                Directory.GetDirectories(d);
                                Directory.Move(first, thenew);

                            }
                            else
                            {
                                Console.WriteLine(directory);
                                Directory.GetDirectories(directory);
                                Directory.Move(directory, directory + second);
                                File.Delete(directory);
                            }
                        }
                        break;
                        //case "fc":
                        //    break;
                }

            }
        }


            public static string RemoveQoutes(string s)
            {
                foreach (var item in s)
                {
                    s = s.Replace("\"", "");
                }
                return s;
            }

        }
    }


