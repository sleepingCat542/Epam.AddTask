using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindWebApiApp.Models;
using NorthwindWebApiApp.Services;

namespace NorthwindWebApiApp.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/orders")]
    public class OrdersVersion2Controller : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public OrdersVersion2Controller(IOrderService orderService, ILogger<OrdersController> logger, IMapper mapper)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a full list of orders.
        /// </summary>
        /// <returns>A brief model.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BriefOrderVersion2Model>>> GetOrders()
        {
            this.logger.LogInformation("Calling OrdersVersion2Controller.GetOrders");
            try
            {
                var result = await this.orderService.GetExtendedOrdersAsync();
                return this.Ok(this.mapper.Map<BriefOrderVersion2Model[]>(result));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Exception in OrdersController.GetOrders.");
                throw;
            }
        }

        /// <summary>
        /// Gets an order by ID.
        /// </summary>
        /// <param name="orderId">An order ID.</param>
        /// <returns>A full model.</returns>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<FullOrderModel>> GetOrder(int orderId)
        {
            this.logger.LogInformation("Calling OrdersVersion2Controller.GetOrder");
            try
            {
                var result = await this.orderService.GetOrderAsync(orderId);
                return this.Ok(this.mapper.Map<FullOrderModel>(result));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Exception in OrdersController.GetOrders.");
                throw;
            }
        }
    }
}
