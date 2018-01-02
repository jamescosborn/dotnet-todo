﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ToDoList.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private ToDoListContext db = new ToDoListContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Item> model = db.Items.ToList();
            return View(model);
        }

		public IActionResult Details(int id)
		{
			Item thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			return View(thisItem);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Item item)
		{
			db.Items.Add(item);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Edit(int id)
		{
			var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			return View(thisItem);
		}
		[HttpPost]
		public IActionResult Edit(Item item)
		{
			db.Entry(item).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			return View(thisItem);
		}
        /*The attribute ActionName, seen here, lets us give the Action a different
        name than used in the method signature.So, even though our method is named
        DeleteConfirmed, because we set the attribute ActionName equal to Delete,
        the proper action will execute when the form is submitted.*/
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			db.Items.Remove(thisItem);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
    }
}
