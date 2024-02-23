using Homework2Library.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2Library
{
    public class BasicCRUD
    {
        public void CreateStudent(string filePath, string tableName, StudentModel student) 
        {
            using (SQLiteConnection connection = new SQLiteConnection(filePath)) 
            {
                connection.Open();
                string query = $"INSERT INTO {tableName} (Id, Name, Points) VALUES (@NewId, @NewName, @NewPoints)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@NewId", student.Id);
                    command.Parameters.AddWithValue("@NewName", student.Name);
                    command.Parameters.AddWithValue("@NewPoints", student.Points);

                    // Execute the CREATE command
                    command.ExecuteNonQuery();
                    Console.WriteLine("Created a new row!");
                }

                connection.Close();
            }
        }

        public List<StudentModel> ReadAllStudents(string filePath, string tableName, List<StudentModel> studentList) 
        {
            studentList.Clear();
            using (SQLiteConnection connection = new SQLiteConnection(filePath)) 
            {
                connection.Open();
                string query = $"SELECT * FROM {tableName}";

                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader()) 
                {
                    while (reader.Read()) 
                    {
                        StudentModel student = new StudentModel 
                        {
                            Id = reader.GetInt32(0), // GetInt(column #)
                            Name = reader.GetString(1), // GetString(column #)
                            Points = reader.GetInt32(2)
                    };

                        studentList.Add(student);
                    }
                }
            }

            return studentList;
        }

        public void UpdateName(string filePath, string tableName, int id, string newName) 
        {
            using (SQLiteConnection connection = new SQLiteConnection(filePath))
            {
                connection.Open();
                string query = $"UPDATE {tableName} SET Name=@NewName WHERE Id=@FindId";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@FindId", id);

                    // Execute the CREATE command
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Updated Name at {id}!");
                }

                connection.Close();
            }
        }

        public void UpdatePoints(string filePath, string tableName, int id, int newPoints)
        {
            using (SQLiteConnection connection = new SQLiteConnection(filePath))
            {
                connection.Open();
                string query = $"UPDATE {tableName} SET Points=@NewPoints WHERE Id=@FindId";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewPoints", newPoints);
                    command.Parameters.AddWithValue("@FindId", id);

                    // Execute the CREATE command
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Updated Points at {id}!");
                }

                connection.Close();
            }
        }

        public void DeleteStudent(string filePath, string tableName, int id) 
        {
            using (SQLiteConnection connection = new SQLiteConnection(filePath))
            {
                connection.Open();
                string query = $"DELETE FROM {tableName} WHERE Id = @Id";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id); // deleting a certain row

                    // Execute the DELETE command
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Rows deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("No rows deleted.");
                    }
                }

                connection.Close();
            }
        }
    }
}
