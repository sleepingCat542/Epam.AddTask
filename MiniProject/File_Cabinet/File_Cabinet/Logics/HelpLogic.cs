using File_Cabinet.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Cabinet.Logics
{
    public class HelpLogic
    {
        public static List<string> WriteNames()
        {
            List<string> list = new List<string>();
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
                if (lastName.Length > 60 || firstName.Length > 60)
                    throw new Exception("Line length cannot exceed 60 characters");
                list.Add(firstName);
                list.Add(lastName);
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                return null;
            }

        }
    }

}
