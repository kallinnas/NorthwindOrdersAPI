﻿namespace NorthwindOrdersAPI.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public Order? Order { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        public OrderDetail() { }
        public OrderDetail(int OrderID, int ProductID, int Quantity)
        {
            this.OrderID = OrderID;
            this.ProductID = ProductID;
            this.Quantity = Quantity;
        }


    }
}
