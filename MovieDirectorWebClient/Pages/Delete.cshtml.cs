using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

public class DeleteModel : PageModel
{
    private readonly ApiService _apiService;

    public DeleteModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public Movie Movie { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Movie = await _apiService.GetMovieAsync(id);
        if (Movie == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var response = await _apiService.DeleteMovieAsync(id);
        if (response.IsSuccessStatusCode)
            return RedirectToPage("Index");

        return Page();
    }
}