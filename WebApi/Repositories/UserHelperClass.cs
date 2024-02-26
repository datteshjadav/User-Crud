using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories;
using Npgsql;

namespace WebApi.Repositories
{
    public class UserHelperClass : IUserInterface
    {
        private readonly string? _conn;
    
        private NpgsqlConnection connection;
        public UserHelperClass(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("ConStr");
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
                            // isUserAuthenticated = true;
                            // var session = _httpContextAccessor.HttpContext.Session;
                            // session.SetString("username", reader["c_username"].ToString());
                            // session.SetInt32("userid", (int)reader["c_userid"]);

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
    
        public Register ApiLogin(LoginModel user)
        {
            Register userDetails = new Register();
            bool isUserAuthenticated = false;
            try
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select * from mvc_repo.t_register Where c_email=@email and c_password=@password",connection))
                {
                    cmd.Parameters.AddWithValue("@email", user.c_email);
                    cmd.Parameters.AddWithValue("@password", user.c_password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDetails.c_userid = (int)reader["c_userid"];
                            userDetails.c_username = (string)reader["c_username"];
                            userDetails.c_email = (string)reader["c_email"];
                            userDetails.c_password = (string)reader["c_password"];
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
            return userDetails;
        }
    
        public void SignOut(){
            // var session = _httpContextAccessor.HttpContext.Session;
            // session.Clear();
        }

        public void Register(Register register)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conn))
            {
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO mvc_repo.t_register(c_username, c_email, c_password, c_gender, c_hobbytravel, c_hobbycricket, c_hobbycoding, c_image)VALUES (@username, @email, @password, @gender, @hobbytravel, @hobbycricket, @hobbycoding, @image)", con))
                    {
                        cmd.Parameters.AddWithValue("@username", register.c_username);
                        cmd.Parameters.AddWithValue("@email", register.c_email);
                        cmd.Parameters.AddWithValue("@password", register.c_password);
                        cmd.Parameters.AddWithValue("@gender", register.c_gender);
                        cmd.Parameters.AddWithValue("@hobbytravel", register.c_hobbytravel);
                        cmd.Parameters.AddWithValue("@hobbycricket", register.c_hobbycricket);
                        cmd.Parameters.AddWithValue("@hobbycoding", register.c_hobbycoding);
                        cmd.Parameters.AddWithValue("@image", register.c_image);

                        //image processing

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("#### Register Helper Error #### " + e);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}