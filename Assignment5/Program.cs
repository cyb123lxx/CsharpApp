using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement
{
    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderTime { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public double TotalPrice
        {
            get
            {
                return OrderDetails.Sum(d => d.Price * d.Quantity);
            }
        }

        public Order()
        {
            OrderId = -1;
            OrderTime = DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null && OrderId == order.OrderId && Customer.Equals(order.Customer)
                && OrderTime == order.OrderTime && OrderDetails.SequenceEqual(order.OrderDetails);
        }

        public override int GetHashCode()
        {
            int hashCode = -1552858886;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Customer>.Default.GetHashCode(Customer);
            hashCode = hashCode * -1521134295 + OrderTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<OrderDetail>>.Default.GetHashCode(OrderDetails);
            return hashCode;
        }

        public override string ToString()
        {
            string str = $"OrderID: {OrderId}, Customer: {Customer}, OrderTime: {OrderTime}, TotalPrice: {TotalPrice}\n";
            foreach (OrderDetail detail in OrderDetails)
            {
                str += detail + "\n";
            }
            return str;
        }
    }

    public class OrderDetail
    {
        public int DetailId { get; set; }
        public Goods Goods { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public OrderDetail()
        {
            DetailId = -1;
            Goods = new Goods();
            Price = 0.0;
            Quantity = 0;
        }

        public override bool Equals(object obj)
        {
            var detail = obj as OrderDetail;
            return detail != null && DetailId == detail.DetailId && Goods.Equals(detail.Goods)
                && Price == detail.Price && Quantity == detail.Quantity;
        }

        public override int GetHashCode()
        {
            int hashCode = -675903661;
            hashCode = hashCode * -1521134295 + DetailId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Goods>.Default.GetHashCode(Goods);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"DetailID: {DetailId}, Goods: {Goods}, Price: {Price}, Quantity: {Quantity}";
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public Customer()
        {
            CustomerId = -1;
            Name = "";
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null && CustomerId == customer.CustomerId && Name == customer.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = -1145661355;
            hashCode = hashCode * -1521134295 + CustomerId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Goods
    {
        public int GoodsId { get; set; }
        public string Name { get; set; }

        public Goods()
        {
            GoodsId = -1;
            Name = "";
        }

        public override bool Equals(object obj)
        {
            var goods = obj as Goods;
            return goods != null && GoodsId == goods.GoodsId && Name == goods.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = 245766140;
            hashCode = hashCode * -1521134295 + GoodsId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class OrderService
    {
        private List<Order> orders;

        public OrderService()
        {
            orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
            {
                throw new ApplicationException($"The order with ID {order.OrderId} already exists!");
            }
            orders.Add(order);
        }

        public void RemoveOrder(int orderId)
        {
            Order order = GetOrderById(orderId);
            if (order == null)
            {
                throw new ApplicationException($"The order with ID {orderId} is not found!");
            }
            orders.Remove(order);
        }

        public void UpdateOrder(Order newOrder)
        {
            Order oldOrder = GetOrderById(newOrder.OrderId);
            if (oldOrder == null)
            {
                throw new ApplicationException($"The order with ID {newOrder.OrderId} is not found!");
            }
            orders.Remove(oldOrder);
            orders.Add(newOrder);
        }

        public List<Order> Query(Func<Order, bool> condition)
        {
            return orders.Where(condition).OrderBy(order => order.TotalPrice).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public void SortOrders(Comparison<Order> comparison)
        {
            orders.Sort(comparison);
        }

        public void SortOrders()
        {
            orders.Sort((o1, o2) => o1.OrderId.CompareTo(o2.OrderId));
        }

        public void PrintAllOrders()
        {
            Console.WriteLine("All orders:");
            foreach (Order order in orders)
            {
                Console.WriteLine(order);
            }
            Console.WriteLine();
        }

        public void Test()
        {
            Console.WriteLine("Test AddOrder()");
            Order order1 = new Order()
            {
                OrderId = 1,
                Customer = new Customer() { CustomerId = 1, Name = "Alice" },
                OrderDetails = new List<OrderDetail>() { new OrderDetail() { DetailId = 1, Goods = new Goods() { GoodsId = 1, Name = "book" }, Price = 10.0, Quantity = 10 } }
            };
            AddOrder(order1);
            PrintAllOrders();

            Console.WriteLine("Test RemoveOrder()");
            RemoveOrder(1);
            PrintAllOrders();

            Console.WriteLine("Test UpdateOrder()");
            Order order2 = new Order()
            {
                OrderId = 2,
                Customer = new Customer() { CustomerId = 2, Name = "Bob" },
                OrderDetails = new List<OrderDetail>() { new OrderDetail() { DetailId = 2, Goods = new Goods() { GoodsId = 2, Name = "computer" }, Price = 1000.0, Quantity = 1 },
                    new OrderDetail() { DetailId = 3, Goods = new Goods() { GoodsId = 1, Name = "book" }, Price = 20.0, Quantity = 5 } }
            };
            AddOrder(order2);
            UpdateOrder(new Order()
            {
                OrderId = 2,
                Customer = new Customer() { CustomerId = 2, Name = "Bob" },
                OrderDetails = new List<OrderDetail>() { new OrderDetail() { DetailId = 2, Goods = new Goods() { GoodsId = 2, Name = "computer" }, Price = 999.0, Quantity = 1 },
                    new OrderDetail() { DetailId = 3, Goods = new Goods() { GoodsId = 1, Name = "book" }, Price = 21.0, Quantity = 5 } }
            });
            PrintAllOrders();

            Console.WriteLine("Test GetOrderById()");
            Console.WriteLine(GetOrderById(2));

            Console.WriteLine("Test Query()");
            List<Order> result = Query(order => order.TotalPrice > 500.0);
            foreach (Order order in result)
            {
                Console.WriteLine(order);
            }

            Console.WriteLine("Test SortOrders()");
            SortOrders((o1, o2) => o2.TotalPrice.CompareTo(o1.TotalPrice));
            PrintAllOrders();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            orderService.Test();
        }
    }
}