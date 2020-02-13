using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            FileData Files = new FileData(@".\input.txt", @".\output.txt");
            DataParser Task = new DataParser(Files.GetFileData());

            Files.WriteToFile(Task.GetResult());
            if (Task.CheckErr()) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error has occured");
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Input :");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Files.GetFileData());

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Result :");
                Console.ForegroundColor = ConsoleColor.White;
                string res = Task.GetResult();


                for (int i = 0; i < res.Length; i++)
                {
                    if (i % 2 != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        if (Char.IsUpper(res[i]))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                        }
                    }
                    Console.Write(res[i]);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    class FileData
    {

        private string fileData;
        private string filePathInput;
        private string filePathOutput;

        public FileData(string input, string output)
        {
            filePathInput = input;
            filePathOutput = output;
            ReadFileData();
        }

        //public void ChangeFilePath(string path) { }

        public string GetFileData()
        {
            return fileData;
        }

        public string ReadFileData()
        {
            if (System.IO.File.Exists(filePathInput))
            {
                fileData = System.IO.File.ReadAllText(filePathInput);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File :" + filePathInput + "is missing.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Creating empty input file in path");
                System.IO.File.WriteAllText(filePathInput, "");
            }
            return fileData;
        }
        public void WriteToFile(string data)
        {
            System.IO.File.WriteAllText(filePathOutput, data);
        }

        private string InitFile(string path)
        {
            string context = null;
            return context;
        }
    }

    class DataParser
    {
        private string originData;
        private string parseResult;
        private bool Error;

        public bool CheckErr()
        {
            return Error;
        }

        public DataParser(string Data)
        {
            NewParse(Data);
        }

        public string GetResult()
        {
            return parseResult;
        }

        public string GetOriginData()
        {
            return originData;
        }

        public string NewParse(string newData)
        {
            if (String.IsNullOrEmpty(newData))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("String is empty or Null");
                Error = true;
                return null;
            }
            originData = newData;
            parseResult = Parse(originData);
            return parseResult;
        }

        private string Parse(string Data)
        {
            string parseRes = "";

            int cnt = 1;
            for (int i = 0; i < Data.Length; i++)
            {
                if (i + 1 == Data.Length)
                {
                    parseRes += Data[i];
                    parseRes += cnt;
                }
                else
                {
                    if (Data[i] != Data[i + 1])
                    {
                        parseRes += Data[i];
                        parseRes += cnt;
                        cnt = 1;
                    }
                    else
                    {
                        cnt++;
                    }
                }
            }
            return parseRes;
        }
    }
}



//Зачем 2 цикла если можно сделать 1?
//for (int i = 0; i < Data.Length; i++)
//{
//    parseRes += Data[i];
//    for (int j = i + 1; j <= Data.Length; j++)
//    {
//        if (j == Data.Length)
//        {
//            parseRes += (j - i);
//            i = j - 1;
//            break;
//        }
//        if (Data[i] != Data[j])
//        {
//            parseRes += (j - i);
//            i = j - 1;
//            break;
//        }
//    }
//}