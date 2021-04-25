using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAppApi
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
    }
}
