using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_1;
using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace ParamonovTest
{
    [TestClass]
    public class ParamonovTest
    {
        private static Stopwatch stopWatch = new Stopwatch();
        public TestContext TestContext { get; set; }
        private static string output = @"output.txt";
        private static string input = @"input.txt";

        private void createLongFile(string path)
        {
            File.WriteAllText(path, "");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                for (int i = 1; i <= 100000000; i++)
                {
                    file.Write("TTAACCAAPP");
                }
            }
        }
        private static void writeToFile(String text)
        {
            using (StreamWriter streamWriter = new StreamWriter(input))
            {
                streamWriter.WriteLine(text);
            }
        }
        private static String readFromFile()
        {
            using (StreamReader streamReader = new StreamReader(output))
            {
                return streamReader.ReadLine();
            }
        }
        private static void clean()
        {
            if (File.Exists(output))
            {
                File.SetAttributes(output, FileAttributes.Normal);
                File.Delete(output);
            }
            if (File.Exists(input))
            {
                File.SetAttributes(input, FileAttributes.Normal);
                File.Delete(input);
            }
        }
        private static String timeProgram(String str)
        {
            writeToFile(str);
            stopWatch.Reset();
            stopWatch.Start();
            Program.Main(null);
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            return timeSpan.TotalSeconds.ToString();
        }

        private static String timeProgram()
        {
            stopWatch.Reset();
            stopWatch.Start();
            Program.Main(null);
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            return timeSpan.TotalSeconds.ToString();
        }
        [TestMethod]
        public void EngTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram("aaabbbccAa"));
            Assert.AreEqual("a3b3c2A1a1", readFromFile());
            clean();
        }

        [TestMethod]
        public void RusTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram("ôôôÔÔçïïïå¸åå"));
            Assert.AreEqual("ô3Ô2ç1ï3å1¸1å2", readFromFile());
            clean();
        }
        [TestMethod]
        public void SpaceTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram(" TEST  TEST   "));
            Assert.AreEqual(" 1T1E1S1T1 2T1E1S1T1 3", readFromFile());
            clean();
        }
        [TestMethod]
        public void SimbolsTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram("\"\"\"))\\''%!!!.-"));
            Assert.AreEqual("\"3)2\\1'2%1!3.1-1", readFromFile());
            clean();
        }
        [TestMethod]
        public void NullTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram(null));
            Assert.AreEqual("", readFromFile());
            clean();
        }
        [TestMethod]
        public void EmptyTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram(""));
            Assert.AreEqual("", readFromFile());
            clean();
        }
        [TestMethod]
        public void NumberTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram("22222550"));
            Assert.AreEqual("255201", readFromFile());
            clean();
        }
        [TestMethod]
        public void EntrTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram("\n222"));
            Assert.AreEqual("", readFromFile());
            clean();
        }
        [TestMethod]
        public void HieroglyphTest()
        {
            clean();
            TestContext.WriteLine("Time: " + timeProgram("\u9997\u9997\u9998\u9999"));
            Assert.AreEqual("\u99972\u99981\u99991", readFromFile());
            clean();
        }
        [TestMethod]
        public void LongStringTest()
        {
            clean();
            createLongFile(input);
            TestContext.WriteLine("Time: " + timeProgram());
            Assert.AreEqual("Data is too long", readFromFile());
            clean();
        }
        [TestMethod]
        public void VeryLongStringTest()
        {
            clean();
            createLongFile(input);
            TestContext.WriteLine("Time: " + timeProgram());
            Assert.AreEqual("Data is too long", readFromFile());
            clean();
        }
        [TestMethod]
        public void NoFileTest()
        {
            clean();
            stopWatch.Reset();
            stopWatch.Start();
            Program.Main(null);
            stopWatch.Stop();
            TestContext.WriteLine("Time: " + stopWatch.Elapsed.TotalSeconds);
            Assert.AreEqual("File Is Missing. Creating empty file in user path", readFromFile());
            clean();
        }
        [TestMethod]
        public void ReadOnlyTest()
        {
            clean();
            writeToFile("");
            File.WriteAllText(output, "");
            File.SetAttributes(output, FileAttributes.ReadOnly);
            TestContext.WriteLine("Time: " + timeProgram("sss"));
            Assert.AreEqual("s3", readFromFile());
            clean();
        }
        [TestMethod]
        public void HiddenTest()
        {
            clean();
            writeToFile("");
            File.WriteAllText(output, "");
            File.SetAttributes(output, FileAttributes.Hidden);
            TestContext.WriteLine("Time: " + timeProgram("sss"));
            Assert.AreEqual("s3", readFromFile());
            clean();
        }
        [TestMethod]
        public void CompressedTest()
        {
            clean();
            writeToFile("");
            File.WriteAllText(output, "");

            File.SetAttributes(output, FileAttributes.Compressed);
            TestContext.WriteLine("Time: " + timeProgram("sss"));
            Assert.AreEqual("s3", readFromFile());
            clean();
        }
        [TestMethod]
        public void EncryptedTest()
        {
            clean();
            writeToFile("");
            File.WriteAllText(output, "");

            File.SetAttributes(output, FileAttributes.Encrypted);
            TestContext.WriteLine("Time: " + timeProgram("sss"));
            Assert.AreEqual("s3", readFromFile());
            clean();
        }
    }
}
