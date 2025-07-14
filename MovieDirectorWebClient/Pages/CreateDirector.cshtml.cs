using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

public class CreateDirectorModel : PageModel
{
    private readonly ApiService _apiService;

    public CreateDirectorModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public Director Directors { get; set; } = new Director();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Directors.Movies = new List<Movie>();

        var response = await _apiService.CreateDirectorAsync(Directors);

        if (response.IsSuccessStatusCode)
            return RedirectToPage("Index");

        return Page();
    }
}
