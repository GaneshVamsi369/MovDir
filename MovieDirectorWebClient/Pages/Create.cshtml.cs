using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;

public class CreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public Movie Movie { get; set; } = new Movie();

    [BindProperty]
    public Director Director { get; set; } = new Director();

    [BindProperty]
    public List<int> SelectedDirectors { get; set; }

    public List<Director> AllDirectors { get; set; }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("MovieAPI");
        AllDirectors = await client.GetFromJsonAsync<List<Director>>("Directors");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("MovieAPI");
        Movie.DirectorIds = SelectedDirectors;

        var response = await client.PostAsJsonAsync("Movies", Movie);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }
}
