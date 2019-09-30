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
      public string SearchString { get; set; }
      // Requires using Microsoft.AspNetCore.Mvc.Rendering;
      public SelectList Genres { get; set; }
      [BindProperty(SupportsGet = true)]
      public string MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
          // Use LINQ to get list of genres.
          IQueryable<string> genreQuery = from m in _context.Movie
                                orderby m.Genre
                                select m.Genre;

          var movies = from m in _context.Movie
          select m;

          if (!string.IsNullOrEmpty(SearchString))
          {
              movies = movies.Where(s => s.Title.Contains(SearchString));
          }

          if (!string.IsNullOrEmpty(MovieGenre))
          {
              movies = movies.Where(x => x.Genre == MovieGenre);
          }
          Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
          Movie = await movies.ToListAsync();
        }
    }
}
