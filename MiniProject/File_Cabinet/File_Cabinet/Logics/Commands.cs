using CsvHelper;
using File_Cabinet.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace File_Cabinet
{
    static class Commands
    {        
        public static Dictionary<string, string> CommandsDictionary;
        public static List<Record> Records;
        private static string connectionString= @"Data Source=.\SQLEXPRESS;Initial Catalog=miniProject;Integrated Security=True";

        static Commands()
        {

            CommandsDictionary = new Dictionary<string, string>
            {                
            { "create", "CreateRecord" },
                {"list", "ShowList"},
                {"list ", "ShowListWithArg"},
                {"stat", "GetStat"},
                {"find", "Find"},
                {"edit", "EditRecord"},
                {"remove", "RemoveRecord"},
                {"export csv", "ExportCSV"},
                {"export xml", "ExportXML"},
                {"import csv", "ImportCSV"},
                {"import xml", "ImportXML"},
                {"purge", "ClearTable"},
                {"help", "ShowHelp"},
                {"exit", "Exit"}
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
            try
            {
                if (firstName == String.Empty)
                throw new Exception("You forgot to enter a name");
                if (lastName == String.Empty)
                throw new Exception("You forgot to enter a surname");
                if (lastName.Length>60 || firstName.Length > 60)
                throw new Exception("Line length cannot exceed 60 characters");

                Console.Write("Date of birth: ");
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   connection.Open();
                   string sqlString = $"INSERT INTO Records([FirstName], [LastName], [Date]) VALUES(N'{firstName}', N'{lastName}', N'{dateOfBirth.ToString("s")}')";
                   SqlCommand command = new SqlCommand(sqlString, connection);
                   command.ExecuteNonQuery();
                   sqlString = $"SELECT TOP 1 Id FROM Records ORDER BY Id DESC;";
                   command = new SqlCommand(sqlString, connection);
                    
                   Console.WriteLine($"Record #{command.ExecuteScalar()} is created!");
                        
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Format Exception {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            
        }
        #endregion

        #region Show
        /// <summary>
        /// Prints all records in file 
        /// </summary>
        public static void ShowList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = $"Select * from Records";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        Console.WriteLine($"#{Convert.ToInt32(reader1.GetValue(0))}, {reader1.GetValue(1).ToString()}, {reader1.GetValue(2).ToString()}");
                    }
                }
                reader1.Close();
            }
        }


        /// <summary>
        /// Prints all records in file 
        /// </summary>
        public static void ShowListWithArg()
        {
            string param = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = $"Select * from Records";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        string s=null;
                        if(param.Contains("id"))
                        {
                            s += reader1.GetValue(0).ToString()+" ";
                        }
                        if (param.Contains("firstname"))
                        {
                            s += reader1.GetValue(1).ToString() + " ";
                        }
                        if (param.Contains("lastname"))
                        {
                            s += reader1.GetValue(2).ToString() + " ";
                        }
                        if (param.Contains("date"))
                        {
                            s += reader1.GetValue(3).ToString() + " ";
                        }
                        Console.WriteLine(s);
                    }
                }
                reader1.Close();
            }
        }
        #endregion

        #region Stat
        /// <summary>
        /// Prints current number of records
        /// </summary>
        public static void GetStat()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = $"Select count (*) from Records";
                SqlCommand command = new SqlCommand(sqlString, connection);
                Console.WriteLine($"{command.ExecuteScalar().ToString()} records");
            }
        }
        #endregion

        #region Find
        /// <summary>
        /// Find a record by argument 
        /// </summary>
        /// <param name="argument">What parameter will be searched</param>
        /// <param name="parameter">Search Parameter Name</param>
        /// <returns name="answers">Array of found lines</returns>
        public static List<string> Finds(string argument, string parameter)
        {
            List<string> answers = new List<string>();
            try {
                switch (argument.ToLowerInvariant())
                {
                    case "firstname":
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                string sqlString = $"Select * from Records";
                                SqlCommand command = new SqlCommand(sqlString, connection);
                                SqlDataReader reader1 = command.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        if (parameter == reader1.GetValue(1).ToString())
                                        {
                                            answers.Add($"#{reader1.GetValue(0).ToString()}");
                                        }
                                    }
                                }
                                reader1.Close();
                            }
                            return answers;
                        }

                    case "lastname":
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                string sqlString = $"Select * from Records";
                                SqlCommand command = new SqlCommand(sqlString, connection);
                                SqlDataReader reader1 = command.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        if (parameter == reader1.GetValue(2).ToString())
                                        {
                                            answers.Add($"#{reader1.GetValue(0).ToString()}");
                                        }
                                    }
                                }
                                reader1.Close();
                            }
                            return answers;
                        }
                    case "dateofbirth":
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                string sqlString = $"Select * from Records";
                                SqlCommand command = new SqlCommand(sqlString, connection);
                                SqlDataReader reader1 = command.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        if (parameter == reader1.GetValue(3).ToString())
                                        {
                                            answers.Add($"#{reader1.GetValue(0).ToString()}");
                                        }
                                    }
                                }
                                reader1.Close();
                            }
                            return answers;
                        }
                    default:
                        throw new ArgumentException($"Incorrect argument {argument}");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            return answers;

        }

        /// <summary>
        /// Find a record
        /// </summary>
        public static void Find()
        {
            string arg = Console.ReadLine();
            string param = Console.ReadLine();
            List<string> answers = Finds(arg, param);
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
            public static void EditRecord()
            {
            Console.WriteLine("#");
            int ind = Convert.ToInt32(Console.ReadLine());
            Console.Write("First name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Date of birth: ");
            try
            {
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlString = $"UPDATE Records SET FirstName = {firstName}, LastName = {lastName}, Date={dateOfBirth.ToString("s")} WHERE index = {ind};";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Changes saved");
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong time format!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Edit Error\n{e.Message}");
            }
            
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes record
        /// </summary>
        public static void RemoveRecord()
        {
            try
            {
                Console.Write("#: ");
                string ind = Console.ReadLine();
                Regex regex = new Regex(@"\d");
                if (regex.IsMatch(ind))
                {
                    int id = Convert.ToInt32(ind);
                    RemoveRecordById(id);
                }
                else
                    throw new Exception("You must enter a number");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        /// <summary>
        /// Removes record by index
        /// </summary>
        /// <param name="id">index of deleted row</param>
            public static void RemoveRecordById(int id)
            {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    string sqlString = $"DELETE FROM Records WHERE Id={id}; ";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    if(command.ExecuteNonQuery()!=0)
                        Console.WriteLine($"Record #{id} is removed");
                    else
                        Console.WriteLine($"No such #{id}");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }               
            }


        }
        #endregion

        #region Import and Export
        /// <summary>
        /// Creates xml file with 
        /// and fills it with records
        /// </summary>
        public static void ExportXML()
        {
            string fileName = "exportXML.xml";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = $"Select * from Records";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.HasRows)
                {
                    XElement root = new XElement("Root");
                    while (reader1.Read())
                    {
                        XElement xml = new XElement("Record",
                                                new XElement("Index", reader1.GetValue(0)),
                                                new XElement("FirstName", reader1.GetValue(1)),
                                                new XElement("LastName", reader1.GetValue(2)),
                                                new XElement("DateOfBirth", reader1.GetValue(3)));
                        root.Add(xml);
                    }
                    root.Save(fileName);
                }               
                Console.WriteLine("Export completed");
            }
        }

        /// <summary>
        /// Creates txt file with given name
        /// and fills it
        /// </summary>
        public static void ImportXML()
        {
            string fileName = "exportXML.xml";
            using (FileStream str = new FileStream(fileName, FileMode.Open))
            {
                XElement recordsXml = XElement.Load(str);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                            foreach (var recordXml in recordsXml.Elements())
                                {
                                        string sqlString = $"INSERT INTO Records([FirstName], [LastName], [Date])" +
                                            $" VALUES( N'{recordXml.Element("FirstName").Value}'," +
                                            $" N'{recordXml.Element("LastName").Value}'," +
                                            $" '{DateTime.Parse(recordXml.Element("DateOfBirth").Value)}')";
                                        command = new SqlCommand(sqlString, connection);
                                        command.ExecuteNonQuery();                                   
                                }

                            }
                        }
            Console.WriteLine("Import sucsess");
                               
            
        }

        /// <summary>
        /// Creates cvs file with given name
        /// and fills it with records
        /// </summary>
        public static void ExportCSV()
        {
            try
            {
                GetList();
                string fileName = "exportCSV.csv";
                using (var csv = new CsvWriter(new StreamWriter(fileName)))
                {
                    csv.WriteRecords(Records);
                }
                Console.WriteLine("Export completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to CSV file \n" + e.Message);
            }
        }

        /// <summary>
        /// Creates txt file with given name
        /// and fills it
        /// </summary>
        public static void ImportCSV()
        {
            string fileName = "exportCSV.csv";
                using (var csv = new CsvReader(File.OpenText(fileName)))
                {
                    IEnumerable<Record> newRecords = csv.GetRecords<Record>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                            foreach (Record record in newRecords)
                            {
                                    string sqlString = $"INSERT INTO Records([FirstName], [LastName], [Date])" +
                                        $" VALUES( N'{record.FirstName}', N'{record.LastName}', N'{record.DateOfBirth.ToString("s")}')";
                                   command = new SqlCommand(sqlString, connection);
                                    command.ExecuteNonQuery();
                                }
                            }                                                  
                }
            
        }
        #endregion

        #region Purge
        /// <summary>
        /// Deleteы all rows in the table
        /// </summary>
        public static void ClearTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = $"TRUNCATE TABLE Records";
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("All delete");
            }
        }

        #endregion

        #region ListOfRecords
        /// <summary>
        /// Gets list of records from DB
        /// </summary>
        public static void GetList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = $"Select * from Records";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        string s = reader1.GetValue(3).ToString();
                        s.ToString(CultureInfo.InvariantCulture);
                        Records.Add(new Record(Convert.ToInt32(reader1.GetValue(0)),
                                                  reader1.GetValue(1).ToString(),
                                                  reader1.GetValue(2).ToString(),
                                                  DateTime.Parse(s)));
                    }
                }
                reader1.Close();
            }
        }
        #endregion


        /// <summary>
        /// Show list of commands
        /// </summary>
        public static void ShowHelp()
        {
           foreach(string s in CommandsDictionary.Keys)
            {
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }




}

