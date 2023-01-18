using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;

namespace Lab11_LiliyaBuhktik
{
    internal class Program
    {      
        static void Main()
        {

            //var squad = new Squad();

            //  File.WriteAllText(@"D:\Microsoft Visual Studio\Projects\Lab11_LiliyaBuhktik\JsonLab11\Squad.json", JsonConvert.SerializeObject(squad));

            // @"D:\Microsoft Visual Studio\Projects\Lab11_LiliyaBuhktik\JsonLab11"; 

            string yesno;
            string nameFile;
            string filePath = "";


            while (Directory.Exists(filePath) != true)
            {
                Console.WriteLine("Enter the path to the folder with files");
                filePath = Console.ReadLine();

                if (Directory.Exists(filePath) != true)
                    Console.WriteLine("The directory for the entered path does not exist.");
            }

            string[] fileEntries = Directory.GetFiles(filePath, "*.json");

            if (fileEntries.Length == 0)  
                Console.WriteLine("There are no json files in the folder.");
            
            if (fileEntries.Length == 1) 
            { 
                
                nameFile = fileEntries[0].Substring((fileEntries[0].LastIndexOf("\\") + 1), (fileEntries[0].Length - 1) - fileEntries[0].LastIndexOf("\\"));
                
                Console.WriteLine();
                Console.WriteLine($"Deserialize {nameFile} document? Y|N");
                yesno = Console.ReadLine().ToUpper();

                if (yesno == "Y")
                {
                    Squad squad2 = JsonConvert.DeserializeObject<Squad>(File.ReadAllText(fileEntries[0]));

                    //FieldInfo[] fields = typeof(Squad).GetFields();

                    IFormatter formatter = new BinaryFormatter();

                    using (Stream stream = new FileStream(filePath + "\\Squad.bin",
                    FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(stream, squad2);
                    }

                    //foreach (FieldInfo fieldInfo in fields.Where(field => field.Name != "id"))
                    //{
                    //    string fieldName = fieldInfo.Name;
                    //    Console.WriteLine(fieldName);
                    //    // здесь с полученным значением поля можете делать что угодно
                    //}
                  
                }               
                    

            }


            if (fileEntries.Length > 1)
            {
                foreach (string fileName in fileEntries)
                {
                    Console.WriteLine(fileName.Substring((fileName.LastIndexOf("\\") + 1), (fileName.Length - 1) - fileName.LastIndexOf("\\")));

                }

                Console.WriteLine();
                Console.WriteLine($"Введите название документа");
                nameFile = Console.ReadLine();
                int k=-1;

                for (int i = 0; i < fileEntries.Length; i++)
                    if (fileEntries[i].EndsWith(nameFile) == true) k = i;


                if (k != -1)
                {
                    Squad squad2 = JsonConvert.DeserializeObject<Squad>(File.ReadAllText(fileEntries[k]));

                   
                    IFormatter formatter = new BinaryFormatter();

                    using (Stream stream = new FileStream(filePath + "\\Squad.bin",
                    FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(stream, squad2);
                    }

                }
                else Console.WriteLine("No such file exists.");

            }

        }
    }
}