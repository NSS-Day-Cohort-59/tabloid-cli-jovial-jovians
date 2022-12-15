using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            throw new NotImplementedException();
        }
        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }
        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, URL )
                                                     VALUES (@blogTitle, @URL)";
                    cmd.Parameters.AddWithValue("@blogTitle", blog.Title);
                    cmd.Parameters.AddWithValue("@URL", blog.Url);
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }
       
        public void Update(Blog entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Update Blog
                                         SET Title = @title,
                                         URL = @url                                      
                                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@url", entry.Url);
                    cmd.Parameters.AddWithValue("@id", entry.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


       
    }
}
