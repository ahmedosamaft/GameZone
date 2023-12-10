using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesService
    {
        Task Create (CreateGameFormViewModel game);
        IEnumerable<Game> GetAll ( );
    }
}
