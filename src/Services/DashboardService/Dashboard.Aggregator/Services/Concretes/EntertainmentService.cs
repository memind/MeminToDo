using Dashboard.Aggregator.Extensions;
using Dashboard.Aggregator.Models.EntertainmentModels;
using Dashboard.Aggregator.Services.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Aggregator.Services.Concretes
{
    public class EntertainmentService : IEntertainmentService
    {
        private readonly HttpClient _client;

        public EntertainmentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetTotalBookCount()
        {
            var response = await _client.GetAsync($"/getBooks");
            var books = await response.ReadContentAs<List<BookModel>>();

            return books.Count;
        }

        public async Task<int> GetTotalBookNoteCount()
        {
            var response = await _client.GetAsync($"/getBookNotes");
            var bookNotes = await response.ReadContentAs<List<BookNoteModel>>();

            return bookNotes.Count;
        }

        public async Task<int> GetTotalGameCount()
        {
            var response = await _client.GetAsync($"/getGames");
            var games = await response.ReadContentAs<List<GameModel>>();

            return games.Count;
        }

        public async Task<int> GetTotalShowCount()
        {
            var response = await _client.GetAsync($"/getShows");
            var shows = await response.ReadContentAs<List<ShowModel>>();

            return shows.Count;
        }
        public async Task<int> GetUserBookCount(string id)
        {
            var response = await _client.GetAsync($"/getUsersBooks?id={id}");
            var books = await response.ReadContentAs<List<BookModel>>();

            return books.Count;
        }

        public async Task<int> GetUserBookNoteCount(string id)
        {
            var response = await _client.GetAsync($"/getUsersBookNotes?id={id}");
            var bookNotes = await response.ReadContentAs<List<BookNoteModel>>();

            return bookNotes.Count;
        }

        public async Task<int> GetUserGameCount(string id)
        {
            var response = await _client.GetAsync($"/getUsersGames?id={id}");
            var games = await response.ReadContentAs<List<GameModel>>();

            return games.Count;
        }

        public async Task<int> GetUserShowCount(string id)
        {
            var response = await _client.GetAsync($"/getUsersShows?id={id}");
            var shows = await response.ReadContentAs<List<ShowModel>>();

            return shows.Count;
        }
    }
}
