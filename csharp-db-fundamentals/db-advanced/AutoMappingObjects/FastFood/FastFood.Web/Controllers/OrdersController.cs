namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using FastFood.Models;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = context.Items
                .ToDictionary(x => x.Id, x => x.Name),

                Employees = context.Employees
                .ToDictionary(x => x.Id, x => x.Name),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var order = this.mapper.Map<Order>(model);
            var orderItem = new OrderItem()
            {
                Order = order,
                ItemId = model.ItemId,
                Quantity = model.Quantity
            };
            this.context.OrderItems.Add(orderItem);
            order.OrderItems.Add(orderItem);
            order.DateTime = DateTime.Now;

            this.context.Orders.Add(order);

            this.context.SaveChanges();

            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = this.context.Orders
                .ProjectTo<OrderAllViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(orders);
        }
    }
}
