using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebApi.Models;
using Npgsql;

namespace WebApi.Repositories
{
    public class StudentRepo : IStudentInterface
    {
        private readonly string? conn;
        public StudentRepo(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("ConStr");
        }
        
        // Insert Student
        public string AddStudent(StudentModel student)
        {
            var rowseffected = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(conn))
            {
                try
                {
                    var qry = "INSERT INTO mvc_repo.t_student (c_studname, c_studage, c_studphone, c_studcourse, c_studaddress) VALUES (@c_studname, @c_studage, @c_studphone, @c_studcourse, @c_studaddress);";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        cmd.Parameters.AddWithValue("@c_studname", student.c_studname);
                        cmd.Parameters.AddWithValue("@c_studage", student.c_studage);
                        cmd.Parameters.AddWithValue("@c_studphone", student.c_studphone);
                        cmd.Parameters.AddWithValue("@c_studcourse", student.c_studcourse);
                        cmd.Parameters.AddWithValue("@c_studaddress", student.c_studaddress);
                        con.Open();
                        rowseffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Add Student Helper : " + e);
                }
                finally
                {
                    con.Close();
                }
            }
            if (rowseffected > 0)
            {
                return "Student is Added Successfully!!!!!";
            }
            else
            {
                return "There was some issue adding the Student";
            }
        }

        //Get Course In Dropdwn
        public string[] GetCourse()
        {
            var course = new List<string>();
            using (NpgsqlConnection con = new NpgsqlConnection(conn))
            {
                try
                {
                    con.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_studcourse FROM mvc_repo.t_course;", con))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            course.Add((string)reader["c_studcourse"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("#### Get Course helper error ##### " + e);
                }
                finally
                {
                    con.Close();
                }
            }
            return course.ToArray();
        }


        // Get List of Student
        public List<StudentModel> GetStudent(){
            var studList = new List<StudentModel>();

            using(NpgsqlConnection con = new NpgsqlConnection(conn)){
                try{
                    var qry = "SELECT c_studid, c_studname, c_studage, c_studphone, c_studcourse,c_studaddress FROM mvc_repo.t_student ORDER BY c_studid";
                    using(NpgsqlCommand cmd = new NpgsqlCommand(qry,con))
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();

                        while(reader.Read()){

                            var studs = new StudentModel{
                                c_studid = (int)reader["c_studid"],
                                c_studname = (string)reader["c_studname"],
                                c_studage = (int)reader["c_studage"],
                                c_studphone = (long)reader["c_studphone"], //Changed (int)reader["c_studphone"] To (long)reader["c_studphone"]
                                c_studcourse = (string)reader["c_studcourse"], //Changed (string[])reader["c_studcourse"] To (string)reader["c_studcourse"]
                                c_studaddress = (string)reader["c_studaddress"]
                            };
                            studList.Add(studs);
                        }
                    }
                }catch(Exception e){
                    Console.WriteLine("######nGet Student Helper error: "+e);
                }finally{
                    con.Close();
                }
            }
            return studList;
        }

        //Get Specific Student
        public StudentModel GetOneStudent(int id)
        {
            var student = new StudentModel();
            using(NpgsqlConnection con = new NpgsqlConnection(conn))
            {
                try
                {
                    var qry = "SELECT * FROM mvc_repo.t_student WHERE c_studid = @id";
                    using(NpgsqlCommand cmd = new NpgsqlCommand(qry,con))
                    {
                        cmd.Parameters.AddWithValue("@id",id);
                        con.Open();
                        var reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            student = new StudentModel
                            {
                                c_studid = (int)reader["c_studid"],
                                c_studname = (string)reader["c_studname"],
                                c_studage = (int)reader["c_studage"],
                                c_studphone = (long)reader["c_studphone"], //Changed (int)reader["c_studphone"] To (long)reader["c_studphone"]
                                c_studcourse = (string)reader["c_studcourse"], //Changed (string[])reader["c_studcourse"] To (string)reader["c_studcourse"]
                                c_studaddress = (string)reader["c_studaddress"]
                            };
                        }
                    }
                }catch(Exception e){
                    Console.WriteLine("######nGet Specific Student Helper error: "+e);
                }finally{
                    con.Close();
                }
            }
            return student;
        }

        //Update Student
        public void UpdateStudent(StudentModel student)
        {
            using(NpgsqlConnection con = new NpgsqlConnection(conn))
            {
                try
                {
                    var qry = "UPDATE mvc_repo.t_student SET c_studname=@c_studname, c_studage=@c_studage, c_studphone=@c_studphone, c_studcourse=@c_studcourse, c_studaddress=@c_studaddress WHERE c_studid=@id;";
                    using(NpgsqlCommand cmd = new NpgsqlCommand(qry,con))
                    {
                        cmd.Parameters.AddWithValue("@id",student.c_studid);
                        cmd.Parameters.AddWithValue("@c_studname", student.c_studname);
                        cmd.Parameters.AddWithValue("@c_studage", student.c_studage);
                        cmd.Parameters.AddWithValue("@c_studphone", student.c_studphone);
                        cmd.Parameters.AddWithValue("@c_studcourse", student.c_studcourse);
                        cmd.Parameters.AddWithValue("@c_studaddress", student.c_studaddress);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }catch(Exception e){
                    Console.WriteLine("#### Update Student Helper Class SQL : "+e);
                }finally{
                    con.Close();
                }
            }
        }

        //Delete Student
        public void DeleteStudent(int id)
        {
            using(NpgsqlConnection con = new NpgsqlConnection(conn))
            {
                try
                {
                    var qry = "DELETE FROM mvc_repo.t_student WHERE c_studid=@id;";
                    using(NpgsqlCommand cmd = new NpgsqlCommand(qry,con))
                    {
                        cmd.Parameters.AddWithValue("@id",id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }catch(Exception e){
                    Console.WriteLine("#### Student Delete Helper Class SQL : "+e);
                }finally{
                    con.Close();
                }
            }
        }

    }
}