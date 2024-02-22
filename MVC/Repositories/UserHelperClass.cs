using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;
using Npgsql;

namespace WebApi.Repositories
{
    public class UserHelperClass : IUserInterface
    {
        private readonly string? _conn;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private NpgsqlConnection connection;
        public UserHelperClass(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _conn = configuration.GetConnectionString("ConStr");
            _httpContextAccessor = httpContextAccessor;
            connection = new NpgsqlConnection(_conn);
        }
        public bool Login(LoginModel user)
        {
            bool isUserAuthenticated = false;
            try
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select * from mvc_repo.t_register  Where c_email=@email and c_password=@password",connection))
                {
                    cmd.Parameters.AddWithValue("@email", user.c_email);
                    cmd.Parameters.AddWithValue("@password", user.c_password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isUserAuthenticated = true;
                            var session = _httpContextAccessor.HttpContext.Session;
                            session.SetString("username", reader["c_username"].ToString());
                            session.SetInt32("userid", (int)reader["c_userid"]);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at Login" + e);
            }
            finally
            {
                connection.Close();
            }
            return isUserAuthenticated;
        }
    
        public void SignOut(){
            var session = _httpContextAccessor.HttpContext.Session;
            session.Clear();
        }
    }
}