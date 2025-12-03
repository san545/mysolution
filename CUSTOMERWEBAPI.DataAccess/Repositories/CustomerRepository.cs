using CUSTOMERWEBAPI.DataAccess.Data;
using CUSTOMERWEBAPI.DataAccess.Entities;
using CUSTOMERWEBAPI.DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMERWEBAPI.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly sqlDBConnectionFactory _factory;
        public CustomerRepository(sqlDBConnectionFactory sqlDBConnectionFactory)
        {
            _factory = sqlDBConnectionFactory;
        }
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("AddCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cust_name", customer.cust_name);
            cmd.Parameters.AddWithValue("@gst_no", customer.gst_no);
            cmd.Parameters.AddWithValue("@mob_no", customer.mob_no);
            cmd.Parameters.AddWithValue("@address", customer.address);
            cmd.Parameters.AddWithValue("@country", customer.country);
            cmd.Parameters.AddWithValue("@city", customer.city);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("DeleteCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cust_id", id);
            await con.OpenAsync();
            int i = await cmd.ExecuteNonQueryAsync();
            return i > 0;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var Customers = new List<Customer>();
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("SelectAllCustomer", con);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Customers.Add(new Customer
                {
                    cust_id = reader.GetInt32(reader.GetOrdinal("cust_id")),
                    cust_name = reader.GetString(reader.GetOrdinal("cust_name")),
                    gst_no = reader.GetString(reader.GetOrdinal("gst_no")),
                    mob_no = reader.GetString(reader.GetOrdinal("mob_no")),
                    address = reader.GetString(reader.GetOrdinal("address")),
                    country = reader.GetString(reader.GetOrdinal("country")),
                    city = reader.GetString(reader.GetOrdinal("city"))

                });
            }
            return Customers;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("SelectOneCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cust_id", id);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
               return new Customer
                {
                    cust_id = reader.GetInt32(reader.GetOrdinal("cust_id")),
                    cust_name = reader.GetString(reader.GetOrdinal("cust_name")),
                    gst_no = reader.GetString(reader.GetOrdinal("gst_no")),
                    mob_no = reader.GetString(reader.GetOrdinal("mob_no")),
                    address = reader.GetString(reader.GetOrdinal("address")),
                    country = reader.GetString(reader.GetOrdinal("country")),
                    city = reader.GetString(reader.GetOrdinal("city"))

                };
            }
            return null;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("UpdateCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cust_name", customer.cust_name);
            cmd.Parameters.AddWithValue("@cust_id", customer.cust_id);
            cmd.Parameters.AddWithValue("@gst_no", customer.gst_no);
            cmd.Parameters.AddWithValue("@mob_no", customer.mob_no);
            cmd.Parameters.AddWithValue("@address", customer.address);
            cmd.Parameters.AddWithValue("@country", customer.country);
            cmd.Parameters.AddWithValue("@city", customer.city);
            await con.OpenAsync();
            int i = await cmd.ExecuteNonQueryAsync();
            return i > 0;
        }
    }
}
