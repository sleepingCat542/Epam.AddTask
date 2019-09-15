using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace File_Cabinet
{
    static class Commands
    {
        public static Dictionary<string, string> CommandsDictionary;
        public static List<Record> Records;
        static FileInfo file;

        static Commands()
        {
            file = new FileInfo("file_cabinet.dat");

            CommandsDictionary = new Dictionary<string, string>
            {                
            { "create", "CreateRecord" },
                {"list", "ShowList"},
                {"stat", "GetStat"},
                {"find", "Find"},
                {"edit", "EditRecord"},
                {"remove", "RemoveRecord"}
            };

            Records = new List<Record>();
        }

        #region Create
        /// <summary>
        /// Command for creating record
        /// </summary>
        public static void  CreateRecord()
        {
            Console.Write("First name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Date of birth: ");
            try
            {
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
                Record recordTemp = new Record(firstName, lastName, dateOfBirth);
                Records.Add(recordTemp);

                using (BinaryWriter writer = new BinaryWriter(file.Open(FileMode.Append,
                            FileAccess.Write, FileShare.None)))
                {
                    writer.Write(recordTemp.Index);
                    writer.Write(recordTemp.FirstName);
                    writer.Write(recordTemp.LastName);
                    writer.Write(recordTemp.DateOfBirth.ToString("dd.MM.yyyy"));
                }
                Console.WriteLine($"Record #{recordTemp.Index} is created!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong time format!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"AddError\n{e.Message}");
            }
        }
        #endregion

        #region Show
        /// <summary>
        /// Prints all records in file 
        /// </summary>
        public static void ShowList()
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(file.OpenRead()))
                {
                    while (reader.PeekChar() > -1)
                    {
                        Console.WriteLine($"#{reader.ReadInt32()}, {reader.ReadString()}, {reader.ReadString()}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Stat
        /// <summary>
        /// Prints current number of records
        /// </summary>
        public static void GetStat()
        {
            Console.WriteLine($"{System.IO.File.ReadAllLines(file.Name).Length} records");
        }
        #endregion

        #region Find
        /// <summary>
        /// Find a record by argument 
        /// </summary>
        /// <param name="argument">What parameter will be searched</param>
        /// <param name="parameter">Search Parameter Name</param>
        /// <returns name="answers">Array of found lines</returns>
        public static List<string> Find(string argument, string parameter)
        {
            List<string> answers = new List<string>();
            switch (argument.ToLowerInvariant())
            {
                case "firstname":
                    {
                        using (BinaryReader reader = new BinaryReader(file.OpenRead()))
                        {
                            while (reader.PeekChar() > -1)
                            {
                                int index = reader.ReadInt32();
                                string firstname = reader.ReadString();
                                string lastname = reader.ReadString();
                                string date = reader.ReadString();
                                if (firstname == parameter)
                                    answers.Add($"#{index}");
                            }
                        }
                        return answers;
                    }
                    
                case "lastname":
                    {
                        using (BinaryReader reader = new BinaryReader(file.OpenRead()))
                        {
                            while (reader.PeekChar() > -1)
                            {
                                int index = reader.ReadInt32();
                                string firstname = reader.ReadString();
                                string lastname = reader.ReadString();
                                string date = reader.ReadString();
                                if (lastname == parameter)
                                    answers.Add($"#{index}");
                            }
                        }
                        return answers;
                    }
                case "dateofbirth":
                    {
                        using (BinaryReader reader = new BinaryReader(file.OpenRead()))
                        {
                            while (reader.PeekChar() > -1)
                            {
                                int index = reader.ReadInt32();
                                string firstname = reader.ReadString();
                                string lastname = reader.ReadString();
                                string date = reader.ReadString();
                                if (date == parameter)
                                    answers.Add($"#{index}");
                            }
                        }
                        return answers;
                    }
                default:
                    throw new ArgumentException($"Incorrect argument {argument}");
            }
        }

        /// <summary>
        /// Find a record
        /// </summary>
        public static void Find()
        {
            string arg = Console.ReadLine();
            string param = Console.ReadLine();
            List<string> answers = Find(arg, param);
            if (answers.Count == 0)
                Console.WriteLine("No results found!");
            else
                foreach(string resultString in answers)
                {
                    Console.WriteLine(resultString);
                }
            
        }

        #endregion

        #region Edit
        /// <summary>
        /// Parses given id and
        /// replaces old record with given id with created record
        /// </summary>
        /// <param name="ind">Index of the record to replace</param>
        public static void EditRecord(string ind)
        {
            int index = Convert.ToInt32(ind);
            RemoveRecord(index);

            Console.Write("First name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Date of birth: ");
            try
            {
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
                Record recordTemp = new Record(index, firstName, lastName, dateOfBirth);
                Records.Add(recordTemp);

                using (BinaryWriter writer = new BinaryWriter(file.Open(FileMode.Append,
                            FileAccess.Write, FileShare.None)))
                {
                    writer.Write(recordTemp.Index);
                    writer.Write(recordTemp.FirstName);
                    writer.Write(recordTemp.LastName);
                    writer.Write(recordTemp.DateOfBirth.ToString("dd.MM.yyyy"));
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong time format!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"AddError\n{e.Message}");
            }
            Console.WriteLine("Changes saved");
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes book
        /// </summary>
        public static void RemoveRecord()
        {
            Console.Write("#: ");
            int id = Convert.ToInt32(Console.ReadLine());
            RemoveRecord(id);
        }


        /// <summary>
        /// Removes book by index
        /// </summary>
        /// <param name="id">index of deleted row</param>
            public static void RemoveRecord(int id)
            {
            string lineToDelete="delete";
            using (BinaryReader reader = new BinaryReader(file.OpenRead()))
            {
                while (reader.PeekChar() > -1)
                {
                    int index = reader.ReadInt32();
                    string firstname = reader.ReadString();
                    string lastname = reader.ReadString();
                    string date = reader.ReadString();
                    if (id == index)
                        lineToDelete=$"#{index} {firstname} {lastname} {date}";
                }
            }

            string line = null;

            using (StreamReader reader = new StreamReader(file.Name))
            {
                using (StreamWriter writer = new StreamWriter(file.Name))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (String.Compare(line, lineToDelete) == 0)
                            continue;

                        writer.WriteLine(line);
                    }
                }
            }

        }
        #endregion

        #region Import and Export
        /// <summary>
        /// Creates xml file with given name
        /// and fills it with records
        /// </summary>
        /// <param name="fileName">Name of the xml file</param>
        public static void ExportXML(string fileName = "exportXML.xml")
        {
            try
            {
                string[] data = File.ReadAllLines(file.Name);
                foreach (string item in data)
                {
                    string[] items = item.Split(' ');
                    XElement xml = new XElement("Record",
                                                new XElement("Index", items[0]),
                                                new XElement("FirstName", items[1]),
                                                new XElement("LastName", items[2]),
                                                new XElement("DateOfBirth", items[3]));
                    xml.Save(fileName);

                }
                
                
                Console.WriteLine("Export completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to XML file \n" + e.Message);
            }
        }

        /// <summary>
        /// Creates txt file with given name
        /// and fills it
        /// </summary>
        /// <param name="fileName">Name of the xml file</param>
        public static void ImportXML(string fileName = "importXML.txt")
        {
            //try
            //{
            //    string[] data = File.ReadAllLines(file.Name);
            //    foreach (string item in data)
            //    {
            //        string[] items = item.Split(' ');
            //        XElement xml = new XElement("Record",
            //                                    new XElement("Index", items[0]),
            //                                    new XElement("FirstName", items[1]),
            //                                    new XElement("LastName", items[2]),
            //                                    new XElement("DateOfBirth", items[3]));
            //        xml.Save(fileName);

            //    }


            //    Console.WriteLine("Export completed");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error exporting to XML file \n" + e.Message);
            //}
        }

        /// <summary>
        /// Creates cvs file with given name
        /// and fills it with records
        /// </summary>
        /// <param name="fileName">Name of the cvs file</param>
        public static void ExportCSV(string fileName = "exportCSV.csv")
        {
            //try
            //{
            //    string[] data = File.ReadAllLines(file.Name);
            //    foreach (string item in data)
            //    {
            //        string[] items = item.Split(' ');
            //        XElement xml = new XElement("Record",
            //                                    new XElement("Index", items[0]),
            //                                    new XElement("FirstName", items[1]),
            //                                    new XElement("LastName", items[2]),
            //                                    new XElement("DateOfBirth", items[3]));
            //        xml.Save(fileName);

            //    }


            //    Console.WriteLine("Export completed");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error exporting to XML file \n" + e.Message);
            //}
        }

        /// <summary>
        /// Creates txt file with given name
        /// and fills it
        /// </summary>
        /// <param name="fileName">Name of the cvs file</param>
        public static void ImportCSV(string fileName = "importCSV.txt")
        {
            try
            {
                string[] data = File.ReadAllLines(file.Name);
                foreach (string item in data)
                {
                    string[] items = item.Split(' ');
                    XElement xml = new XElement("Record",
                                                new XElement("Index", items[0]),
                                                new XElement("FirstName", items[1]),
                                                new XElement("LastName", items[2]),
                                                new XElement("DateOfBirth", items[3]));
                    xml.Save(fileName);

                }


                Console.WriteLine("Export completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to XML file \n" + e.Message);
            }
        }
        #endregion

        Purge


    }




}

