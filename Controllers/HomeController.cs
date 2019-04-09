using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crudelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace crudelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Dish> AllDishesUnsorted = dbContext.Dishes.ToList();
            List<Dish> AllDishes = AllDishesUnsorted.OrderByDescending(d => d.CreatedAt).ToList();
            return View(AllDishes);
        }

        [Route("new")]
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [Route("new")]
        [HttpPost]
        public IActionResult New(Dish newDish)
        {
            // System.Console.WriteLine("The new dish: ");
            // System.Console.WriteLine(newDish.Name);
            // System.Console.WriteLine(newDish.Chef);
            // System.Console.WriteLine(newDish.Tastiness);
            // System.Console.WriteLine(newDish.Calories);
            // System.Console.WriteLine(newDish.Description);
            if(ModelState.IsValid) 
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                return View("New");
            }
        }

        [Route("edit/{dishid}")]
        [HttpGet]
        public IActionResult Edit(int dishid)
        {
            Dish showDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishid);
            return View(showDish);
        }

        [Route("edit/{dishid}")]
        [HttpPost]
        public IActionResult Edit(int dishid, Dish editDishNew)
        {
            if(ModelState.IsValid) 
            {
                Dish editDishBefore = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishid);
                editDishBefore.Name =  editDishNew.Name;
                editDishBefore.Chef =  editDishNew.Chef;
                editDishBefore.Tastiness =  editDishNew.Tastiness;
                editDishBefore.Calories =  editDishNew.Calories;
                editDishBefore.Description =  editDishNew.Description;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                return View("edit/dishid");
            }
        }

        [Route("delete/{dishid}")]
        [HttpGet]
        public IActionResult Delete(int dishid)
        {
            Dish dishToDelete = dbContext.Dishes.SingleOrDefault(d => d.DishId == dishid);
            dbContext.Dishes.Remove(dishToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("{dishid}")]
        [HttpGet]
        public IActionResult Show(int dishid)
        {
            Dish showDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishid);
            return View(showDish);
        }

    }
}
