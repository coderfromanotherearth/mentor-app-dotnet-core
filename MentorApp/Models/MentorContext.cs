using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace MentorApp.Models
{
    public class MentorContext
    {
        public string ConnectionString { get; set; }
        public MentorContext(string connectionString)
        {
            //this.ConnectionString = "mysql://u5c8mdm77sanvn2y:lqofsXnsBPbGFdMOxhhx@btihduuk0jiokjjk9e67-mysql.services.clever-cloud.com:3306/btihduuk0jiokjjk9e67";
            this.ConnectionString = "server=btihduuk0jiokjjk9e67-mysql.services.clever-cloud.com;port=3306;database=btihduuk0jiokjjk9e67;user=u5c8mdm77sanvn2y;password=lqofsXnsBPbGFdMOxhhx;";
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public List<Mentor> GetAllMentors()
        {
            try
            {
                List<Mentor> list = new List<Mentor>();
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from Mentor", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new Mentor()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                });
                            }
                            return list;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            { return null; }
        }
        public Mentor GetMentorById(int id)
        {
            try
            {
                Mentor mentor = new Mentor();
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from Mentor where id = " + id, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                mentor = new Mentor()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                };
                            }
                            return mentor;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            { return null; }

        }
        public bool AddMentor(Mentor mentor)
        {
            try
            {
                var query = @" CREATE TABLE IF NOT EXISTS Mentor(
                                                Id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                                                Name VARCHAR(30) NOT NULL,
                                                reg_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                                                ); 
                            INSERT INTO Mentor (Name) VALUES (@v_Name);";
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = query;
                    comm.Parameters.AddWithValue("@v_Name", mentor.Name);
                    comm.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteMentorById(int id)
        {
            try
            {
                var query = @"DELETE FROM Mentor WHERE Id = @v_Id";
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = query;
                    comm.Parameters.AddWithValue("@v_Id", id);
                    comm.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            { return false; }
        }
    }
}
