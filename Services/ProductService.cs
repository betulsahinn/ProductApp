using ProductApp.Data;
using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ProductApp.Services
{
    public class ProductService
    {
        public bool Add(Product product)
        {
            if (ProductExists(product.Name))
                return false;

            using var conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "INSERT INTO Products (Name, Price) VALUES (@name, @price)", conn);

            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);

            cmd.ExecuteNonQuery();
            return true;
        }


        public List<Product> GetAll()
        {
            var list = new List<Product>();

            using var conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM Products", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Product
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Price = (decimal)reader["Price"]
                });
            }

            return list;
        }

        public void Update(Product product)
        {
            using var conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "UPDATE Products SET Name=@name, Price=@price WHERE Id=@id", conn);

            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@id", product.Id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id) 
        {
            using var conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "DELETE FROM Products WHERE Id = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public bool ProductExists(string name)
        {
            using var conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Products WHERE Name = @name", conn);

            cmd.Parameters.AddWithValue("@name", name);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }


    }
}
