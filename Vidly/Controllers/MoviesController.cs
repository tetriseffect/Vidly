using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
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

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel); 
        }


        //  Added with exercise. HttpPost becuase it's saving the form
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            // If movie id is 0, it means it's an new movie. Otherwise, update it
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }

            else
            {
                // movieinDb is the movie in the db which matches the ID given by user clicking on the movie
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

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