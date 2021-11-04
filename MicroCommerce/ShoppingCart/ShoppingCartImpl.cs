using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Enums;
using Interfaces.Interfaces;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart
{
    public class ShoppingCartImpl : IShoppingCart
    {
        private static List<Order> _orders = new ();
        private static List<CartEvent> _events = new ();
        private IProductCatalog _catalog;
        
        public ShoppingCartImpl(IProductCatalog catalog)
        {
            _catalog = catalog;
        }

        public string Test()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://ProductCatalog");
            var request = new HttpRequestMessage(HttpMethod.Get, "/get")
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            var response = client.Send(request);
            var result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
        
        public Cart AddOrder(int productId, int qty)
        {
            var order = _orders.FirstOrDefault(i => i.Product.Id == productId);
            if(order != null)
            {
                order.Quantity += qty;
                CreateEvent(CartEventTypeEnum.OrderChanged, order);
            }
            else
            {
                var product = _catalog.Get(productId);
                if (product != null)
                {
                    order = new Order
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Quantity = qty
                    };

                    _orders.Add(order);
                    CreateEvent(CartEventTypeEnum.OrderAdded, order);
                }
            }

            return Get();
        }

        public Cart DeleteOrder(Guid orderId)
        {
            var order = _orders.FirstOrDefault(i => i.Id == orderId);
            if(order != null)
            {
                _orders.Remove(order);
                CreateEvent(CartEventTypeEnum.OrderRemoved, order);
            }

            return Get();
        }

        public Cart Get()
        {
            return new Cart
            {
                Orders = _orders
            };
        }

        public IEnumerable<CartEvent> GetCartEvents(long timestamp)
        {
            return _events.Where(e => e.Timestamp > timestamp);
        }

        private void CreateEvent(CartEventTypeEnum type, Order order)
        {
            _events.Add(new CartEvent
            {
                Timestamp = DateTime.Now.Ticks,
                Time = DateTime.Now,
                Order = order.Clone(),
                Type = type
            });
        }
    }
}