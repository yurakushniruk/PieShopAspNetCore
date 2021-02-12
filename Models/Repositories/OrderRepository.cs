using BethanysPieShop.Utilities;
using BethanysPieShop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public void ApproveOrder(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();
            //adding the order with its details

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }
            order.Status = StaticDetails.StatusSubmitted;
            _appDbContext.Orders.Add(order);

            _appDbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _appDbContext.Orders;
        }

        public Order GetOrderById(int id)
        {
            var result = _appDbContext.Orders.Where(x => x.OrderId == id).Include(p => p.OrderDetails).ThenInclude(p=>p.Pie).First();
            return result;
        }

        public void RemoveOrder(int id)
        {
            var orderToRemove = _appDbContext.Orders.Where(x => x.OrderId == id).First();
            _appDbContext.Orders.Remove(orderToRemove);
            _appDbContext.SaveChanges();
        }

        public void ChangeOrderStatus(int orderId, string status)
        {
            var orderFromDb = _appDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            orderFromDb.Status = status;
            _appDbContext.SaveChanges();
        }
    }
}
