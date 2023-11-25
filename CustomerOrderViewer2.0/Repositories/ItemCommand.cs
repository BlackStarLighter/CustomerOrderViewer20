using CustomerOrderViewer2._0.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderViewer2._0.Repositories
{
    internal class ItemCommand
    {
        private readonly string _connectionString;

        public ItemCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<ItemModel> GetList()
        {
            List<ItemModel> items = new List<ItemModel>();

            var selectStatement = "Item_GetList";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                items = connection.Query<ItemModel>(selectStatement).ToList();
            }

            return items;
        }
    }
}
