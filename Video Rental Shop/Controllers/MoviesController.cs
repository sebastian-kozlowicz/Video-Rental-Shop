﻿using System.Linq;
using System.Web.Mvc;
using Video_Rental_Shop.Models;
using Video_Rental_Shop.ViewModels;
using System.Data.Entity;

namespace Video_Rental_Shop.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.MovieGenre).ToList();

            if (User.IsInRole(RoleName.CanDoAllManipulationsOnEntities))
                return View("List", movies);
            else if (User.IsInRole(RoleName.CanDoManipulationsOnEntitiesExceptDeletion))
                return View("ListWithoutDeletions", movies);

            return View("ReadOnlyList", movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.MovieGenre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanDoAllManipulationsOnEntities + "," + RoleName.CanDoManipulationsOnEntitiesExceptDeletion)]
        public ActionResult New()
        {
            var genres = _context.MovieGenres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanDoAllManipulationsOnEntities + "," + RoleName.CanDoManipulationsOnEntitiesExceptDeletion)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var genres = _context.MovieGenres.ToList();

            var viewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanDoAllManipulationsOnEntities)]
        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanDoAllManipulationsOnEntities + "," + RoleName.CanDoManipulationsOnEntitiesExceptDeletion)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel()
                {
                    Movie = movie,
                    Genres = _context.MovieGenres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.SetNewNumberAvailable(movieInDb, movie);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.Price = movie.Price;
                movieInDb.MovieGenreId = movie.MovieGenreId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}