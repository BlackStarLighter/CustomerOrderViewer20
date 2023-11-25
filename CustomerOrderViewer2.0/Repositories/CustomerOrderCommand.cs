using CustomerOrderViewer2._0.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CustomerOrderViewer2._0.Repositories
{
    internal class CustomerOrderCommand
    {
        private readonly string _connectionString;

        public CustomerOrderCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void Update(int customerOrderId, int customerId, int itemId, string userId)
        {
            var upsertStatement = "CustomerOrderDetail_Upsert";

            var dataTable = new DataTable();
            dataTable.Columns.Add("CustomerOrderId", typeof(int));
            dataTable.Columns.Add("CustomerId", typeof(int));
            dataTable.Columns.Add("ItemId", typeof(int));
            dataTable.Rows.Add(customerOrderId, customerId, itemId);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(upsertStatement, new
                {
                    @CustomerOrderType = dataTable.AsTableValuedParameter("CustomerOrderType"),
                    @UserId = userId
                },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(int customerOrderId, string userId)
        {
            var upsertStatament = "CustomerOrderDetail_Delete";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(upsertStatament, new 
                { 
                    @CustomerOrderId = customerOrderId, @UserId = userId }, 
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IList<CustomerOrderDetailModel> GetList()
        {
            List<CustomerOrderDetailModel> customerOrderDetails = new List<CustomerOrderDetailModel>();

            var selectStatement = "CustomerOrderDetail_GetList";

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                customerOrderDetails = connection.Query<CustomerOrderDetailModel>(selectStatement).ToList();
            }

            return customerOrderDetails;
        }
    }
}
