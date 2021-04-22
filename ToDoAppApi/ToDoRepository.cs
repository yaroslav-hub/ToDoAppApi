using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAppApi
{
    public class ToDoRepository
    {
        private static List<ToDo> Database = new();
        private class ToDo
        {
            public int Id { get; }
            public string Name { get; set; }
            public bool Done { get; set; }
            public DateTime CreationDate { get; }
            public ToDo(int id, string name, bool done)
            {
                Id = id;
                Name = name;
                Done = done;
                CreationDate = DateTime.Now;
            }
        }
        private int GetId() => Database.Count > 0 ? Database.Max(x => x.Id) + 1 : 1;
        public List<ToDoDto> GetAll()
        {
            return Database.ConvertAll(x => new ToDoDto
            {
                Id = x.Id,
                Name = x.Name,
                Done = x.Done
            });
        }
        public int Create(ToDoDto toDoDto)
        {
            int id = GetId();
            ToDo todo = new(id, toDoDto.Name, false);
            Database.Add(todo);
            return id;
        }
        public void Update(int id, ToDoDto toDoDto)
        {
            ToDo finded = Database.Find(x => x.Id == id);
            if (finded == null) return;
            finded.Name = toDoDto.Name;
            finded.Done = toDoDto.Done;
        }
    }
}
