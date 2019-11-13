namespace FastFood.Web.ViewModels.Orders
{
    using FastFood.Models;
    using FastFood.Models.Enums;
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public Dictionary<int, string> Employees { get; set; }
        public Dictionary<int, string> Items { get; set; }
    }
}
