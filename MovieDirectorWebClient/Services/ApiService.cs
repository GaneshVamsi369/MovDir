using MovieDirectorWebClient.Models;

namespace MovieDirectorWebClient.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieAPI");
        }

        public async Task<List<Movie>> GetMoviesAsync() =>
            await _httpClient.GetFromJsonAsync<List<Movie>>("Movies");

        public async Task<Movie?> GetMovieAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Movie>($"Movies/{id}");

        public async Task<HttpResponseMessage> CreateMovieAsync(Movie movie) =>
            await _httpClient.PostAsJsonAsync("Movies", movie);

        public async Task<HttpResponseMessage> UpdateMovieAsync(Movie movie) =>
            await _httpClient.PutAsJsonAsync($"Movies/{movie.MovieId}", movie);

        public async Task<HttpResponseMessage> DeleteMovieAsync(int id) =>
            await _httpClient.DeleteAsync($"Movies/{id}");

        // Repeat similar methods for Directors
        public async Task<List<Director>> GetDirectorsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Director>>("Directors");

        public async Task<Director?> GetDirectorAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Director>($"Directors/{id}");

        public async Task<HttpResponseMessage> CreateDirectorAsync(Director director) =>
            await _httpClient.PostAsJsonAsync("Directors", director);

    }
}
