namespace NorthwindOrdersAPI.Models
{
    public class TempOrder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int? ShipperID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderDetailsJson { get; set; }

        public void UpdateTempOrderValues(TempOrder tempOrder)
        {
            CustomerID = tempOrder.CustomerID;
            EmployeeID = tempOrder.EmployeeID;
            OrderDate = tempOrder.OrderDate;
            ShipperID = tempOrder.ShipperID;
            OrderDate = tempOrder.OrderDate;
            OrderDetailsJson = tempOrder.OrderDetailsJson;
        }
    }
}
