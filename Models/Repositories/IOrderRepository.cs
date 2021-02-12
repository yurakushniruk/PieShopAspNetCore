using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        void RemoveOrder(int id);
        void ApproveOrder(int id);
        Order GetOrderById(int id);
        IEnumerable<Order> GetAllOrders();
        public void ChangeOrderStatus(int orderId, string status);
    }
}
