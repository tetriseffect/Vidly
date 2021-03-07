using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using Vidly.Controllers;
using System.Data.Entity;


namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // Set a varibale that accesses Movies in the database
        private ApplicationDbContext _context; 

        //ctor of above variable
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // Dispose, just gotta do this
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Added with Exercise, action for creating new movie. When button is clicked, take them to New MovieForm page.
        public ViewResult New()
        {
            // Create Dbset in Initial Models for Genre, then came here and made a variable which is a list of genres. Initilize the view model and set.
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        //  Added with exercise. HttpPost becuase it's saving the form
        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            movie.DateAdded = DateTime.Now;
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        //Main page. The Genre inclues it on the page so you can display it in the chart
        public ViewResult Index()
        {
            // Use _context var to access the DB
            var movies = _context.Movies.Include(c => c.Genre).ToList();

            return View(movies);
        }

        // Details page. Include Genre since it's a seperate table
        public ActionResult Details(int id)
        {
            // Use _context var to access the DB
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
    }
}