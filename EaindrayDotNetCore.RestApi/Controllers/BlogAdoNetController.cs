using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using EaindrayDotNetCore.RestApi.Models;
using AKKLTZDotNetCore.RestApi.Models;

namespace EaindrayDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoNetController : ControllerBase
    {
        private readonly string connectionString;

        public BlogAdoNetController()
        {
            // Set up the connection string
            connectionString = "Data Source=.;Initial Catalog=ALTDotNetCore;User ID=sa;Password=sa@123";
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM tbl_blog";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        List<BlogDataModel> lst = new List<BlogDataModel>();
                        while (reader.Read())
                        {
                            BlogDataModel blog = new BlogDataModel
                            {
                                Blog_Id = Convert.ToInt32(reader["Blog_Id"]),
                                Blog_Title = reader["Blog_Title"].ToString(),
                                Blog_Author = reader["Blog_Author"].ToString(),
                                Blog_Content = reader["Blog_Content"].ToString()
                            };
                            lst.Add(blog);
                        }

                        reader.Close();

                        BlogListResponseModel model = new BlogListResponseModel
                        {
                            IsSuccess = true,
                            Message = "Success",
                            Data = lst
                        };
                        return Ok(model);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogDataModel item = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM tbl_blog WHERE Blog_Id = @Blog_Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Blog_Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new BlogDataModel
                            {
                                Blog_Id = Convert.ToInt32(reader["Blog_Id"]),
                                Blog_Title = reader["Blog_Title"].ToString(),
                                Blog_Author = reader["Blog_Author"].ToString(),
                                Blog_Content = reader["Blog_Content"].ToString()
                            };
                        }
                    }
                }
            }

            if (item == null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO tbl_blog (Blog_Title, Blog_Author, Blog_Content)
                                    VALUES (@Blog_Title, @Blog_Author, @Blog_Content)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
                        command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
                        command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);

                        int result = command.ExecuteNonQuery();

                        BlogResponseModel model = new BlogResponseModel()
                        {
                            IsSuccess = result > 0,
                            Message = result > 0 ? "Saving Successful." : "Saving Failed.",
                            Data = blog
                        };
                        return Ok(model);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"UPDATE tbl_blog
                                    SET Blog_Title = @Blog_Title, Blog_Author = @Blog_Author, Blog_Content = @Blog_Content
                                    WHERE Blog_Id = @Blog_Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Blog_Id", id);
                        command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
                        command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
                        command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);

                        int result = command.ExecuteNonQuery();

                        BlogResponseModel model = new BlogResponseModel()
                        {
                            IsSuccess = result > 0,
                            Message = result > 0 ? "Updating Successful." : "Updating Failed.",
                            Data = blog
                        };
                        return Ok(model);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch the existing blog entry
                    string selectQuery = "SELECT * FROM tbl_blog WHERE Blog_Id = @Blog_Id";
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@Blog_Id", id);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update only the non-null properties using SqlCommand parameters
                                string updateQuery = "UPDATE tbl_blog " +
                                                     "SET Blog_Title = @Blog_Title, Blog_Author = @Blog_Author, Blog_Content = @Blog_Content " +
                                                     "WHERE Blog_Id = @Blog_Id";

                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    // Add parameters for the update command
                                    updateCommand.Parameters.AddWithValue("@Blog_Id", id);

                                    if (!string.IsNullOrEmpty(blog.Blog_Title))
                                    {
                                        updateCommand.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
                                    }
                                    else
                                    {
                                        updateCommand.Parameters.AddWithValue("@Blog_Title", reader["Blog_Title"]);
                                    }

                                    if (!string.IsNullOrEmpty(blog.Blog_Author))
                                    {
                                        updateCommand.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
                                    }
                                    else
                                    {
                                        updateCommand.Parameters.AddWithValue("@Blog_Author", reader["Blog_Author"]);
                                    }

                                    if (!string.IsNullOrEmpty(blog.Blog_Content))
                                    {
                                        updateCommand.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
                                    }
                                    else
                                    {
                                        updateCommand.Parameters.AddWithValue("@Blog_Content", reader["Blog_Content"]);
                                    }

                                    // Execute the update
                                    int result = updateCommand.ExecuteNonQuery();

                                    BlogResponseModel model = new BlogResponseModel()
                                    {
                                        IsSuccess = result > 0,
                                        Message = result > 0 ? "Updating Successful." : "Updating Failed.",
                                        Data = blog
                                    };
                                    return Ok(model);
                                }
                            }
                        }
                    }

                    // No data found for the specified ID
                    var response = new { IsSuccess = false, Message = "No data found." };
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSuccess = false, Message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM tbl_blog WHERE Blog_Id = @Blog_Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Blog_Id", id);

                        int result = command.ExecuteNonQuery();

                        BlogResponseModel model = new BlogResponseModel()
                        {
                            IsSuccess = result > 0,
                            Message = result > 0 ? "Deleting Successful." : "Deleting Failed."
                        };
                        return Ok(model);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
