using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NorthwindWebApiApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly NorthwindModel.NorthwindEntities entities;
        private readonly ILogger<OrderService> logger;
        private readonly Uri uri;

        public OrderService(IOptions<Configuration.NorthwindServiceConfiguration> northwindServiceConfiguration, ILogger<OrderService> logger)
        {
            this.uri = northwindServiceConfiguration == null ? throw new ArgumentNullException(nameof(northwindServiceConfiguration)) : northwindServiceConfiguration.Value.Uri;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.entities = new NorthwindModel.NorthwindEntities(this.uri);
        }

        public async Task<IEnumerable<BriefOrderDescription>> GetOrdersAsync()
        {
            var orderTaskFactory = new TaskFactory<IEnumerable<NorthwindModel.Order>>();

            this.logger.LogDebug($"Getting data from {this.uri.AbsoluteUri}.");

            var orders = await orderTaskFactory.FromAsync(
                this.entities.Orders.BeginExecute(null, null),
                iar => this.entities.Orders.EndExecute(iar));

            return orders.Select(o => new BriefOrderDescription
            {
                OrderId = o.OrderID,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
            }).ToArray();
        }

        public async Task<IEnumerable<BriefOrderVersion2Description>> GetExtendedOrdersAsync()
        {
            var orderTaskFactory = new TaskFactory<IEnumerable<NorthwindModel.Order>>();

            this.logger.LogDebug($"Getting data from {this.uri.AbsoluteUri}.");

            var orders = await orderTaskFactory.FromAsync(
                this.entities.Orders.BeginExecute(null, null),
                iar => this.entities.Orders.EndExecute(iar));

            return orders.Select(o => new BriefOrderVersion2Description
            {
                OrderId = o.OrderID,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                CustomerId = o.CustomerID,
                EmployeeId = o.EmployeeID,
            }).ToArray();
        }

        public async Task<FullOrderDescription> GetOrderAsync(int orderId)
        {
            var orderQueryTaskFactory = new TaskFactory<IEnumerable<NorthwindModel.Orders_Qry>>();
            var query = this.entities.Orders_Qries.AddQueryOption("$filter", $"OrderID eq {orderId}");

            this.logger.LogDebug($"Getting data from {this.uri.AbsoluteUri}.");

            var orders = (await orderQueryTaskFactory.FromAsync(
                query.BeginExecute(null, null),
                iar => query.EndExecute(iar))).ToArray();

            var order = orders.FirstOrDefault();

            if (order == null)
            {
                return null;
            }

            return new FullOrderDescription
            {
                OrderId = order.OrderID,
                CustomerId = order.CustomerID,
                EmployeeId = order.EmployeeID,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShipVia = order.ShipVia,
            };
        }
    }
}
