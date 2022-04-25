using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ToDoList.Models;
using Dapper;
using ToDoList.Helper;

namespace TodoList.ViewModels
{
    public class TodoListViewModel
    {
        public TodoListViewModel()
        {
            using var db = DbHelper.GetConnection();
            this.EditableItem = new ToDoListItems();
            this.TodoItems = db.Query<ToDoListItems>("SELECT * FROM todolistitemss ORDER BY AddDate DESC").ToList();
        }

        public List<ToDoListItems> TodoItems { get; set; }

        public ToDoListItems EditableItem { get; set; }
    }
}