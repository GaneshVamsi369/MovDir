using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDirectorWebApi;
using MovieDirectorWebApi.DTO;

namespace MovieDirectorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MoviesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            return await _context.Movies.Select(movie=>new MovieDTO { Title = movie.Title, MovieId = movie.MovieId, DirectorIds = movie.Director.Select(dir=>dir.DirId).ToList()}).ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieTempDTO>> GetMovie(int id)
        {
            var movi = await _context.Movies.FindAsync(id);
            MovieTempDTO movie = new MovieTempDTO()
            {
                MovieId = movi.MovieId,
                Title = movi.Title
            };

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDTO mDTO)
        {
            if (id != mDTO.MovieId)
            {
                return BadRequest();
            }

            var movie = await _context.Movies.Include(m=>m.Director).FirstOrDefaultAsync(m=>m.MovieId==id);

            if (movie==null)
            {
                return NotFound();
            }

            movie.Title = mDTO.Title;
            movie.Director.Clear();

            List<Director> updatedDirectors = new List<Director>();
            foreach (var dirId in mDTO.DirectorIds)
            {
                var director = await _context.Directors.FirstOrDefaultAsync(d=>d.DirId==dirId);
                if (director != null)
                {
                    updatedDirectors.Add(director);
                }
            }
            movie.Director = updatedDirectors;

            foreach (var director in updatedDirectors)
            {
                if (!director.Movies.Contains(movie))
                {
                    director.Movies.Add(movie);
                }
            }
            try
            {
                _context.Entry(movie).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDTO mov)
        {
            List<Director> dirs = new List<Director>();
            foreach (var dirid in mov.DirectorIds)
            {
                Director dir = await _context.Directors.FindAsync(dirid);
                dirs.Add(dir);
            }
            Movie movie = new Movie
            {
                Title = mov.Title,
                //Director = dirs
            };
            foreach (var director in dirs)
            {
                director.Movies.Add(movie);//ask
            }

            _context.Movies.Add(movie);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, mov);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
