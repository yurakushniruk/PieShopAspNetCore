using BethanysPieShop.Models;
using BethanysPieShop.Models.ViewModels;
using BethanysPieShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Areas.Administration.Controllers
{
    [Authorize]
    [Area("Administration")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAllOrders();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            
            return View(order.OrderDetails);
        }
        public IActionResult Approve(int id)
        {
            var orderFromDb = _orderRepository.GetOrderById(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            _orderRepository.ChangeOrderStatus(id, StaticDetails.StatusApproved);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Reject(int id)
        {
            var orderFromDb = _orderRepository.GetOrderById(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            _orderRepository.ChangeOrderStatus(id, StaticDetails.StatusRejected);
            return RedirectToAction(nameof(Index));
        }
    }
}
