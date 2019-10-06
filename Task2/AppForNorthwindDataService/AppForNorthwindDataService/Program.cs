using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForNorthwindDataService
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
            var customers = entities.Customers.ToArray();
            Console.WriteLine("{0} customers in the service found.", customers.Length);
        }
    }
}
