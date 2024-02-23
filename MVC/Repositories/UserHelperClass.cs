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
        private readonly string? conn;
        public UserHelperClass(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("ConStr");
        }
        public List<Register> GetUserRegister()
        {
            var userList = new List<Register>();

            using (NpgsqlConnection con = new NpgsqlConnection(conn))
            {
                try
                {
                    var qry = "SELECT c_userid,c_username,c_email,c_password,c_gender,c_hobby,c_image FROM city_kendo.t_register ORDER BY c_userid";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {

                            var user = new Register
                            {
                                c_userid = (int)reader["c_userid"],
                                c_username = (string)reader["c_username"],
                                c_email = (string)reader["c_email"],
                                c_password = (string)reader["c_password"],
                                c_gender = (string)reader["c_gender"],
                                c_hobby = (string)reader["c_hobby"],
                                c_image = (string)reader["c_image"]
                            };
                            userList.Add(user);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("######nGet User Repo error: " + e);
                }
                finally
                {
                    con.Close();
                }
            }
            return userList;
        }
    }
}