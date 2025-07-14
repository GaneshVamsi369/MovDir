using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

public class IndexModel : PageModel
{
    private readonly ApiService _apiService;

    public IndexModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public List<Movie> Movies { get; set; } = new();
    public List<Director> Directors { get; set; } = new();

    public async Task OnGetAsync()
    {
        Movies = await _apiService.GetMoviesAsync() ?? new List<Movie>();
        Directors = await _apiService.GetDirectorsAsync() ?? new List<Director>();
    }
}
