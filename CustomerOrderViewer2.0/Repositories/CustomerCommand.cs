using CustomerOrderViewer2._0.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderViewer2._0.Repositories
{
    internal class CustomerCommand
    {
        private readonly string _connectionString;

        public CustomerCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<CustomerModel> GetList()
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            var selectCommand = "Customer_GetList";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                customers = connection.Query<CustomerModel>(selectCommand).ToList();
            }

            return customers;
        }
    }
}
