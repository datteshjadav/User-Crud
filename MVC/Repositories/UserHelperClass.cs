using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;
using MVC.Repositories;
using Npgsql;

namespace WebApi.Repositories
{
    public class UserHelperClass : IUserInterface
    {
        private readonly string? conn;
        public UserHelperClass(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("ConStr");
        }
        public void Register(Register register)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conn))
            {
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO mvc_repo.t_register(c_username, c_email, c_password, c_gender, c_hobby, c_image)VALUES (@username, @email, @password, @gender, @hobby, @image)", con))
                    {
                        cmd.Parameters.AddWithValue("@username", register.c_username);
                        cmd.Parameters.AddWithValue("@email", register.c_email);
                        cmd.Parameters.AddWithValue("@password", register.c_password);
                        cmd.Parameters.AddWithValue("@gender", register.c_gender);
                        cmd.Parameters.AddWithValue("@hobby", register.c_hobby);
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