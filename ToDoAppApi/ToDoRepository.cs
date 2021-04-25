using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAppApi
{
    public class ToDoRepository
    {
        private static readonly string _connectionString = @"Server=DESKTOP-4AK6IC5\SQLEXPRESS;Database=ToDoDB;Trusted_Connection=True;";
        //private static List<ToDo> AllTasks = new();
        private class Task
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Done { get; set; }
            public DateTime CreationDate { get; set; }
            //public Task(int id, string name, bool done)
            //{
            //    Id = id;
            //    Name = name;
            //    Done = done;
            //    CreationDate = DateTime.Now;
            //}
        }
        //private int GetId() => AllTasks.Count() > 0 ? AllTasks.Max(x => x.Id) + 1 : 1;
        public List<TaskDto> GetAll()
        {
            List<TaskDto> tasks = new();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT * 
                    FROM [tasks];";
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TaskDto task = new()
                    {
                        Id = (int)reader["id"],
                        Name = (string)reader["name"],
                        Done = (bool)reader["is_done"]
                    };
                    tasks.Add(task);
                }
            }

            return tasks;
        }
        public int Create(TaskDto taskDto)
        {
            int id;
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO [tasks]
                        ([name],
                         [creation_date])
                    VALUES
                        (@name,
                         @creation_date)
                    SELECT SCOPE_IDENTITY();";
                command.Parameters.Add("@name", SqlDbType.Text).Value = taskDto.Name;
                command.Parameters.Add("@creation_date", SqlDbType.Text).Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                id = (int)command.ExecuteScalar();
            }

            return id;
        }
        public void Update(int id, TaskDto taskDto)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE [tasks]
                        SET [is_done] = @is_done
                    WHERE id = @id;";
                command.Parameters.Add("@is_done", SqlDbType.Bit).Value = taskDto.Done;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteScalar();
            }
        }
    }
}
