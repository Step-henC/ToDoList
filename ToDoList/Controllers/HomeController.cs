using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Helper;
using TodoList.ViewModels;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using Dapper;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(x => x.ID == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using var db = DbHelper.GetConnection();
            ToDoListItems item = db.Get<ToDoListItems>(id);
            if (item != null)
                db.Delete(item);
            return RedirectToAction("Index");
        }
        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableItem.ID <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<ToDoListItems>(viewModel.EditableItem);
                    }
                    else
                    {
                        ToDoListItems dbItem = db.Get<ToDoListItems>(viewModel.EditableItem.ID);
                        var result = TryUpdateModelAsync<ToDoListItems>(dbItem, "EditableItem");
                        db.Update<ToDoListItems>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new TodoListViewModel());
        }
        public IActionResult ToggleIsDone(int id)
        {
            using var db = DbHelper.GetConnection();
            ToDoListItems item = db.Get<ToDoListItems>(id);
            if (item != null)
            {
                item.IsDone = !item.IsDone;
                db.Update<ToDoListItems>(item);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
