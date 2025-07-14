using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

namespace MovieDirectorWebClient.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;
        public EditModel(ApiService apiService)
        {
            this._apiService = apiService;
        }
        [BindProperty]
        public Movie movie { get; set; } = new();

        [BindProperty]
        public List<int> SelectedDirectors { get; set; } = new();
        public List<Director> Directors { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            movie = await _apiService.GetMovieAsync(id);
            if(movie == null)
                return NotFound();
            SelectedDirectors = movie.DirectorIds ?? new();
            Directors = await _apiService.GetDirectorsAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            movie.DirectorIds = SelectedDirectors ?? new();
            var response = await _apiService.UpdateMovieAsync(movie);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            //Directors = await _apiService.GetDirectorsAsync();
            return Page();
        }
    }
}
