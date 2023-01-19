using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string path = $"C:\\Users\\{Environment.UserName}\\Desktop\\Students";
            CreateDir(path);
            DeserializeStudent(path);

        }

        static void CreateDir(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void DeserializeStudent(string path)
        {
            ;
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists("Students.dat"))
            {
                using (var fs = new FileStream("Students.dat", FileMode.Open))
                {
                    Student[] students = (Student[])formatter.Deserialize(fs);
                    foreach (Student student in students)
                    {
                        var fileInfo = new FileInfo(path + $"\\{student.Group}.txt");
                        if (!fileInfo.Exists)
                            fileInfo.Create();
                        try
                        {
                            using (StreamWriter sw = fileInfo.AppendText())
                            {
                                sw.WriteLine(student.Name + ", " + student.DateOfBirth);
                            }
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                    }
                }
                
            }
            else Console.WriteLine("Необходим файл \"Studendts.dat\"");

            Console.ReadLine();
        }

    }
}
