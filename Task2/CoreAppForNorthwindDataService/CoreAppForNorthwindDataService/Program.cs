using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreAppForNorthwindDataService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //1

            //NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
            //IAsyncResult asyncResult;
            //asyncResult = entities.Customers.BeginExecute((ar) =>
            //{
            //    var customers = entities.Customers.EndExecute(ar).ToArray();
            //    Console.WriteLine("{0} customers in the service found.", customers.Length);
            //}, null);
            //WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
            //Console.ReadLine();



            //2

            //NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
            //var taskFactory = new TaskFactory<IEnumerable<NorthwindModel.Customer>>();
            //var customers = (await taskFactory.FromAsync(entities.Customers.BeginExecute(null, null), iar => entities.Customers.EndExecute(iar))).ToArray();
            //Console.WriteLine("{0} customers in the service found.", customers.Length);
            //Console.ReadLine();



            //3
            NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
            IAsyncResult asyncResult;
            asyncResult = entities.Customers.BeginExecute((ar) => // breakpoint #1.1
            {
                var customers = entities.Customers.EndExecute(ar).ToArray(); // breakpoint #1.2
                Console.WriteLine("{0} customers in the service found.", customers.Length);
            }, null);
            WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
            Console.ReadLine(); // breakpoint #1.3



            //4
            //NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
            //var taskFactory = new TaskFactory<IEnumerable<NorthwindModel.Customer>>();
            //var customers = (await taskFactory.FromAsync(entities.Customers.BeginExecute(null, null), (iar) => // breakpoint #2.1
            //{
            //    return entities.Customers.EndExecute(iar); // breakpoint #2.2
            //})).ToArray();
            //Console.WriteLine("{0} customers in the service found.", customers.Length);
            //Console.ReadLine(); // breakpoint #2.3

        }
    }
}
