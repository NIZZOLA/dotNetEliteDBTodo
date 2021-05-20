using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi.Models
{
    public class TaskToDoModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Active { get; set; }
    }
}
