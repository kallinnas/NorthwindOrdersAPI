using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data.DTO;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Repositories;
using NorthwindOrdersAPI.Services.Interfaces;
using System.Text.Json;

namespace NorthwindOrdersAPI.BL
{
    public class OrderService : IOrderService, IUploadFileService
    {
        private readonly OrderRepository orderRepository;
        private readonly OrderDetailsRepository orderDetailsRepository;

        public OrderService(OrderRepository orderRepository, OrderDetailsRepository orderDetailsRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<bool> CreateOrderWithFileAsync(string newOrder, IFormFile file)
        {
            Order order = DeserializeOrder(newOrder);

            if (order == null || order.OrderDetails == null || order.OrderDetails.Count == 0)
            {
                return false;
            }

            if (await orderRepository.CreateOrderAsync(order))
            {
                List<OrderDetail> orderDetails = prepareOrderDetails(order);

                if (await orderDetailsRepository.AddOrderDetails(orderDetails))
                {
                    return await UploadFileAsync(file, order.OrderID);
                }
            }

            return false;
        }

        public async Task<bool> UploadFileAsync(IFormFile file, int id)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileData = memoryStream.ToArray();

                    OrderDocument document = new OrderDocument(id, file.FileName, fileData, DateTime.Now);

                    return await orderRepository.UploadOrderDocumentAsync(document);
                }
            }

            return false;
        }

        public async Task<bool> UpdateOrderAndDetailsAsync(Order order)
        {
            return await orderRepository.UpdateOrderAndDetailsAsync(order);
        }

        public async Task<OrderDTO> GetOrderAsync(int id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                throw new InvalidOperationException($"Searched order with ID {id} not found.");
            }

            return MapOrderToOrderDTO(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersWithDetailsAsync()
        {
            var orders = await orderRepository.GetAllOrdersAsync().ToListAsync();

            return MapOrdersToOrderDTO(orders);
        }

        private IEnumerable<OrderDTO> MapOrdersToOrderDTO(IEnumerable<Order> orders)
        {
            return orders.Select(o => new OrderDTO
            {
                OrderID = o.OrderID,
                EmployeeID = o.EmployeeID,
                EmployeeFirstName = o.Employee?.FirstName,
                EmployeeLastName = o.Employee?.LastName,
                EmployeeFullName = o.Employee?.FirstName + " " + o.Employee?.LastName,
                CustomerID = o.CustomerID,
                CustomerName = o.Customer?.CustomerName,
                ShipperID = o.ShipperID,
                ShipperName = o.Shipper?.ShipperName,
                OrderDate = o.OrderDate,
                OrderTotalPrice = (double)o.OrderDetails.Sum(od => od.Quantity * (od.Product?.Price ?? 0))
            });
        }

        private OrderDTO MapOrderToOrderDTO(Order order)
        {
            return new OrderDTO
            {
                OrderID = order.OrderID,
                EmployeeID = order.EmployeeID,
                EmployeeFirstName = order.Employee?.FirstName,
                EmployeeLastName = order.Employee?.LastName,
                EmployeeFullName = order.Employee != null ? order.Employee.FirstName + " " + order.Employee.LastName : null,
                CustomerID = order.CustomerID,
                CustomerName = order.Customer?.CustomerName,
                CustomerContactName = order.Customer?.ContactName,
                ShipperID = order.ShipperID,
                ShipperName = order.Shipper?.ShipperName,
                OrderDate = order.OrderDate,
                OrderTotalPrice = (double)order.OrderDetails.Sum(od => od.Quantity * (od.Product?.Price ?? 0)),
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailID = od.OrderDetailID,
                    OrderID = od.OrderID,
                    ProductID = od.ProductID,
                    ProductName = od.Product?.ProductName ?? string.Empty,
                    Quantity = od.Quantity,
                    UnitPrice = od.Product?.Price ?? 0
                }).ToList()
            };
        }

        private List<OrderDetail> prepareOrderDetails(Order order)
        {
            return order.OrderDetails
                .Select(detail => new OrderDetail(order.OrderID, detail.ProductID, detail.Quantity))
                .ToList();
        }

        private Order DeserializeOrder(string newOrder)
        {
            using (JsonDocument doc = JsonDocument.Parse(newOrder))
            {
                JsonElement root = doc.RootElement;

                return new Order
                {
                    CustomerID = root.GetProperty("customerID").GetInt32(),
                    EmployeeID = root.GetProperty("employeeID").GetInt32(),
                    OrderDate = root.GetProperty("orderDate").GetDateTime(),
                    ShipperID = root.GetProperty("shipperID").GetInt32(),
                    OrderDetails = root.GetProperty("orderDetails").EnumerateArray()
                                      .Select(detail => new OrderDetail
                                      {
                                          ProductID = detail.GetProperty("productID").GetInt32(),
                                          Quantity = detail.GetProperty("quantity").GetInt32()
                                      }).ToList()
                };
            }
        }


    }
}
