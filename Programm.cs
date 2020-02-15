using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Task_1
{   /// <summary>
    /// Основной Класс для Входа в программу и вывода текста в консоль  
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleSpeaker UI = new ConsoleSpeaker(ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green);
            FileData Files = new FileData(@".\input.txt", @".\output.txt");
            if (Files.CheckForErrors())
            {
                UI.WriteErrorMsg(Files.GetErrorMsg());
                Files.WriteToFile(Files.GetErrorMsg());
            }
            else
            {
                DataParser Task = new DataParser(Files.GetFileData());

                Files.WriteToFile(Task.GetResult());
                if (Task.CheckForError())
                {
                    UI.WriteErrorMsg(Task.GetErrorMsg());
                    Files.WriteToFile(Task.GetErrorMsg());
                }
                else
                {
                    UI.WriteSystemMsg("Input :");
                    UI.WriteNormalMsg(Files.GetFileData());
                    UI.WriteSystemMsg("Output :");
                    UI.WriteTask(Task.GetResult());
                }
            }
            UI.WriteNormalMsg("Press any key to exit.");
            //Console.ReadKey();
        }
          

    }


    /// <summary>
    /// Класс производящий общение с консолью и отвечающий за красивый вывод данных
    /// </summary>
    class ConsoleSpeaker
    {
        public static ConsoleColor NormalTextColor;
        public static ConsoleColor ErrorTextColor;
        public static ConsoleColor WarningTextColor;
        public static ConsoleColor SystemTextColor;


        /// <summary>
        /// Метод для записи данных в файл Результата
        /// </summary>
        /// <param name="data">Данные для записи </param>
        public ConsoleSpeaker(ConsoleColor Normal, ConsoleColor Error, ConsoleColor Warning, ConsoleColor System)
        {
            NormalTextColor = Normal;
            ErrorTextColor = Error;
            WarningTextColor = Warning;
            SystemTextColor = System;
        }
        /// <summary>
        /// Метод для вывода сообщения об ошибке
        /// </summary>
        /// <param name="text">Текст сообщения </param>
        public void WriteErrorMsg(string text)
        {
            Console.ForegroundColor = ErrorTextColor;
            Console.WriteLine(text);
            Console.ForegroundColor = NormalTextColor;
        }
        /// <summary>
        /// Метод для вывода сообщении о предупреждении
        /// </summary>
        /// <param name="text">Текст сообщения  </param>
        public void WriteWarningMsg(string text)
        {
            Console.ForegroundColor = WarningTextColor;
            Console.WriteLine(text);
            Console.ForegroundColor = NormalTextColor;
        }
        /// <summary>
        /// Метод для вывода обычного сообщения
        /// </summary>
        /// <param name="text">Текст сообщения  </param>
        public void WriteNormalMsg(string text)
        {
            Console.ForegroundColor = NormalTextColor;
            Console.WriteLine(text);
        }
        /// <summary>
        /// Метод для вывода системного сообщения
        /// </summary>
        /// <param name="text">Текст сообщения  </param>
        public void WriteSystemMsg(string text)
        {
            Console.ForegroundColor = SystemTextColor;
            Console.WriteLine(text);
            Console.ForegroundColor = NormalTextColor;
        }
        /// <summary>
        /// Метод для вывода результата
        /// </summary>
        /// <param name="text">Строка результата</param>
        public void WriteTask(string res)
        {
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
            Console.WriteLine();

        }
    }
    /// <summary>
    /// Класс производящий чтение/запись в файл
    /// </summary>
    class FileData
    {

        private string fileData;
        private string filePathInput;
        private string filePathOutput;
        private bool Error;
        private string ErrorMsg;
        /// <summary>
        /// Проверка на наличие ошибки
        /// </summary>
        /// <returns>true при наличии ошибки</returns>
        public bool CheckForErrors()
        {
            return Error;
        }
        /// <summary>
        /// Вывод сообщения ошибки
        /// </summary>
        /// <returns>Сообщение Ошибки</returns>
        public string GetErrorMsg()
        {
            return ErrorMsg;
        }
        /// <summary>
        /// Конструктор Класса, задающий расположение файлов ввода / вывода данных
        /// </summary>
        /// <param name="input">Путь к файлу содержащий входные данные</param>
        /// <param name="output">Путь к файлу, который будет исползоваться для вывода результата</param>
        public FileData(string input, string output)
        {
            filePathInput = input;
            filePathOutput = output;
            ReadFileData();
        }


        /// <summary>
        /// Метод возвращает последние данные, которые были считаны с файла
        /// </summary>
        /// <returns>Возвращает данные считанные с файла</returns>
        public string GetFileData()
        {
            return fileData;
        }

        /// <summary>
        /// Метод для первичного/повторного чтения данных из файла
        /// </summary>
        /// <returns>Возвращает данные считанные с файла</returns>
        public string ReadFileData()
        {

            if (File.Exists(filePathInput))
            {
                FileAttributes originFileAtt = File.GetAttributes(filePathInput);
                try
                {
                    File.SetAttributes(filePathInput, FileAttributes.Normal);
                    fileData = File.ReadAllText(filePathInput);
                    File.SetAttributes(filePathInput, originFileAtt);
                }
                catch (System.OutOfMemoryException)
                {
                    ErrorMsg = "Data is too long";
                    Error = true;
                    return null;
                }
            }
            else
            {
                ErrorMsg = "File Is Missing. Creating empty file in user path";
                Error = true;
                File.WriteAllText(filePathInput, "");
                return null;
            }
            return fileData;
        }
        /// <summary>
        /// Метод для записи данных в файл Результата
        /// </summary>
        /// <param name="data">Данные для записи </param>

        public void WriteToFile(string data)
        {
            FileAttributes outputFileAtt = FileAttributes.Normal;
            if (File.Exists(filePathOutput))
            {
                outputFileAtt = File.GetAttributes(filePathOutput);
                File.SetAttributes(filePathOutput, FileAttributes.Normal);
            }
            File.WriteAllText(filePathOutput, data);
            File.SetAttributes(filePathOutput, outputFileAtt);
        }
    }
    /// <summary>
    /// Класс Шифратор, производящий группировку для подряд идущих повторяющихся символов
    /// </summary>
    class DataParser
    {
        private string originData;
        private string parseResult;
        private bool Error;
        private string ErrorMsg;

        /// <summary>
        /// Метод для проверки наличия ошибки при выполнении Шифра
        /// </summary>
        /// <returns>Возвращает true в случаии ошибки</returns>
        public bool CheckForError()
        {
            return Error;
        }
        public string GetErrorMsg()
        {
            return ErrorMsg;
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="Data">Данные для шифровки</param>
        public DataParser(string Data)
        {
            NewParse(Data);
        }

        /// <summary>
        /// Метод для получения последнего результата шифровки
        /// </summary>
        /// <returns>Возвращает зашифрованую строку</returns>
        public string GetResult()
        {
            return parseResult;
        }
        /// <summary>
        /// Метод для получения входных данных
        /// </summary>
        /// <returns>Возвращает входную незашифрованную строку</returns>
        public string GetOriginData()
        {
            return originData;
        }
        /// <summary>
        /// Метод для создание новой шифровки
        /// </summary>
        /// <param name="Data">Данные для шифровки</param>
        /// <returns>Возвращает зашифрованную строку</returns>
        public string NewParse(string Data)
        {
            if (String.IsNullOrEmpty(Data))
            {
                ErrorMsg = "Input File is empty";
                Error = true;
                return null;
            }
            originData = Data;
            parseResult = Parse(originData);
            return parseResult;
        }
        /// <summary>
        /// Метод производящий шифровку
        /// </summary>
        /// <param name="Data">Данные для шифровки</param>
        /// <returns>Возвращает зашифрованную строку</returns>
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
