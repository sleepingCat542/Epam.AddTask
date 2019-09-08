using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace File_Cabinet
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">");
                string commandString = Console.ReadLine();
                if (Commands.CommandsDictionary.ContainsKey(commandString))
                {
                    var command = Commands.CommandsDictionary[commandString];
                    MethodInfo methodInfo = typeof(Commands).GetMethod(command);
                    methodInfo.Invoke(null, null);
                }

            }

        }
                      
    }
}

