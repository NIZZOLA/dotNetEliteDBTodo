using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoTaskApi.Models;

namespace TodoTaskApi.Repository
{
    public class TaskToDoRepository
    {
        private string DatabaseName = "banco.db";
        private string TableName = "tasktodo";

        public IEnumerable<TaskToDoModel> Listar()
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var taskCollection = db.GetCollection<TaskToDoModel>(TableName);

                var resultCollection = taskCollection.FindAll();

                return resultCollection.ToList();
            }
        }

        public TaskToDoModel Consultar(Guid? id, string nome)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var taskCollection = db.GetCollection<TaskToDoModel>(TableName);
                if (id.HasValue)
                {
                    var taskToDo = taskCollection.FindOne(x => x.Id == id);
                    return taskToDo;
                }
                else
                {
                    var taskToDo = taskCollection.FindOne(x => x.Nome.Contains(nome));
                    return taskToDo;
                }
            }
        }


        public void Adicionar(TaskToDoModel task)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var taskCollection = db.GetCollection<TaskToDoModel> (TableName);
                taskCollection.Insert(task);
            }
        }

        public bool Alterar(TaskToDoModel task, Guid id)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var taskCollection = db.GetCollection<TaskToDoModel> (TableName);

                var originalTask = taskCollection.FindOne(x => x.Id == id);
                if (originalTask == null)
                    return false;

                originalTask.Nome = task.Nome;
                originalTask.Active = task.Active;

                taskCollection.Update(originalTask);
                return true;
            }
        }

        public void Excluir(Guid id)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var usuarioCollection = db.GetCollection<TaskToDoModel>(TableName);

                usuarioCollection.Delete(id);
            }
        }
    }
}
