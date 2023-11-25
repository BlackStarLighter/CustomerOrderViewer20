using CustomerOrderViewer2._0.Models;
using CustomerOrderViewer2._0.Repositories;

namespace CustomerOrderViewer2._0
{
    internal class Program
    {
        #region PrivateFields
        private static string _connectionString = "Data Source=localhost;Initial Catalog=CustomerOrderViewer;Integrated Security=True;Encrypt=False";
        private static readonly CustomerOrderCommand _customerOrderCommand = new CustomerOrderCommand(_connectionString);
        private static readonly CustomerCommand _customerCommand = new CustomerCommand(_connectionString);
        private static readonly ItemCommand _itemCommand = new ItemCommand(_connectionString);
        #endregion

        static void Main(string[] args)
        {
            try
            {
                bool continueManaging = true;
                var userId = string.Empty;

                Console.WriteLine("What is your username?");
                userId = Console.ReadLine();

                do
                {
                    Console.WriteLine("1 - Show All | 2 - Update Customer Order | 3 - Delete Customer Order | 4 - Exit");
                    int option = Convert.ToInt32(Console.ReadLine());

                    if (option == 1)
                    {
                        ShowAll();
                    }
                    else if (option == 2)
                    {
                        UpdateCustomerOrder(userId);
                    }
                    else if (option == 3)
                    {
                        DeleteCustomerOrder(userId);
                    }
                    else if (option == 4)
                    {
                        continueManaging = false;
                    }
                    else
                    {
                        Console.WriteLine("Option not found");
                    }

                } while (continueManaging == true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }

        private static void ShowAll()
        {
            Console.WriteLine($"{Environment.NewLine}All Customer Orders{Environment.NewLine}");
            DisplayCustomerOrders();

            Console.WriteLine($"{Environment.NewLine}All Customers{Environment.NewLine}");
            DisplayCustomers();

            Console.WriteLine($"{Environment.NewLine}All Items{Environment.NewLine}");
            DisplayItems();

            Console.WriteLine();
        }

        private static void DisplayCustomerOrders()
        {
            IList<CustomerOrderDetailModel> customerOrderDetails = _customerOrderCommand.GetList();

            if (customerOrderDetails.Any())
            {
                foreach (CustomerOrderDetailModel customerOrderDetail in customerOrderDetails)
                {
                    Console.WriteLine($"CustomerOrderID: {customerOrderDetail.CustomerOrderId}, " +
                        $"Fullname: {customerOrderDetail.FirstName}, {customerOrderDetail.LastName}, " +
                        $"CustID: {customerOrderDetail.CustomerId} " +
                        $"- purchased  {customerOrderDetail.Description} {customerOrderDetail.Price} " +
                        $"- ItemID: {customerOrderDetail.ItemId}");
                }
            }
        }

        private static void DisplayCustomers()
        {
            IList<CustomerModel> customers = _customerCommand.GetList();

            if (customers.Any())
            {
                foreach (CustomerModel customer in customers)
                {
                    Console.WriteLine($"{customer.CustomerId} First Name: {customer.FirstName}, " +
                        $"Middle Name: {customer.MiddleName ?? "N/A"}, Last Name: {customer.LastName}, " +
                        $"Age: {customer.Age}");
                }
            }
        }

        private static void DisplayItems()
        {
            IList<ItemModel> items = _itemCommand.GetList();

            if (items.Any())
            {
                foreach  (ItemModel item in items)
                {
                    Console.WriteLine($"{item.ItemId}: Description {item.Description}, {item.Price}");
                }
            }
        }

        private static void UpdateCustomerOrder(string? userId)
        {
            Console.WriteLine("\nEnter CustomerOrderId:");
            int newCustomerOrderId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter CustomerId:");
            int newCustomerId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter ItemId:");
            int newItemId = Convert.ToInt32(Console.ReadLine());

            _customerOrderCommand.Update(newCustomerOrderId, newCustomerId, newItemId, userId);

            Console.WriteLine();
        }

        private static void DeleteCustomerOrder(string? userId)
        {
            Console.WriteLine("Enter CustomerOrderId:");
            int customerOrderId = Convert.ToInt32(Console.ReadLine());
            _customerOrderCommand.Delete(customerOrderId, userId);
            Console.WriteLine();
        }
    }
}
