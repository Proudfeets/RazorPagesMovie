using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        // For security, SupportsGet property is required for GET requests.  For more info: https://www.youtube.com/watch?v=p7iHB9V-KVU&feature=youtu.be&t=54m27s
        [BindProperty(SupportsGet = true)]
      //SearchString is the text that users enter to the search box
    //   the BindProperty attribute forms values and query strings with the same name as the Property
      public string SearchString { get; set; }
      // Requires using Microsoft.AspNetCore.Mvc.Rendering;
      public SelectList Genres { get; set; }
    //   SelectList requires using Microsoft.AspNetCore.Mvc.Rendering
      [BindProperty(SupportsGet = true)]
      public string MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
          // Use System.Linq to get list of genres.
            // This defines the query that returns the movies- does not run the query
          IQueryable<string> genreQuery = from m in _context.Movie
                                orderby m.Genre
                                select m.Genre;

          var movies = from m in _context.Movie
            select m;
            // filters for the movies which contain the user's input
          if (!string.IsNullOrEmpty(SearchString))
          {
            //   'Contains' is run on the db, so case sensitivity depends on db, not C# code 
              movies = movies.Where(s => s.Title.Contains(SearchString));
          }

          if (!string.IsNullOrEmpty(MovieGenre))
          {
              movies = movies.Where(x => x.Genre == MovieGenre);
          }
          Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
        //   ends the async and runs the 
          Movie = await movies.ToListAsync();
        }
    }
}
