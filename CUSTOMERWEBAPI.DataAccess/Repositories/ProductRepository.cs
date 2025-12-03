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
    public class ProductRepository : IProductRepository
    {
        private readonly sqlDBConnectionFactory _connfactory;
        public ProductRepository(sqlDBConnectionFactory sqlDBConnectionFactory)
        {
                _connfactory = sqlDBConnectionFactory;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            using var conn=_connfactory.CreateConnection();
            using SqlCommand cmd = new SqlCommand("AddProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@prod_desc", product.prod_desc);
            cmd.Parameters.AddWithValue("@prod_cd", product.prod_cd);
            cmd.Parameters.AddWithValue("@rate", product.Rate);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            using var conn = _connfactory.CreateConnection();
            using SqlCommand cmd = new SqlCommand("DeleteProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@prod_id", id);
            await conn.OpenAsync();
            var i= await cmd.ExecuteNonQueryAsync();
            return i > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();

            using var conn = _connfactory.CreateConnection();
            using var cmd = new SqlCommand("SelectAllProduct", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    prod_id = reader.GetInt32(reader.GetOrdinal("prod_id")),
                    prod_cd = reader.GetString(reader.GetOrdinal("prod_cd")),
                    prod_desc = reader.GetString(reader.GetOrdinal("prod_desc")),
                    Rate = reader.GetDecimal(reader.GetOrdinal("rate")) // FIX HERE
                });
            }

            return products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
           
            using var conn = _connfactory.CreateConnection();
            using SqlCommand cmd = new SqlCommand("SelectOneProduct", conn);
            cmd.Parameters.Add("@prod_id",SqlDbType.Int).Value=id;
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Product
                {
                    prod_id = reader.GetInt32(reader.GetOrdinal("prod_id")),
                    prod_cd = reader.GetString(reader.GetOrdinal("prod_cd")),
                    prod_desc = reader.GetString(reader.GetOrdinal("prod_desc")),
                    Rate = reader.GetDecimal(reader.GetOrdinal("rate"))
                };
            }
           return null;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            using var conn = _connfactory.CreateConnection();
            using SqlCommand cmd = new SqlCommand("UpdateProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@prod_id", product.prod_id);
            cmd.Parameters.AddWithValue("@prod_desc", product.prod_desc);
            cmd.Parameters.AddWithValue("@prod_cd", product.prod_cd);
            cmd.Parameters.AddWithValue("@rate", product.Rate);
            await conn.OpenAsync();
            var i =await cmd.ExecuteNonQueryAsync();
            return i > 0;
        }
    }
}
