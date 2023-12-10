using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAll ( );
        Game? GetById (int id);
        Task Create (CreateGameFormViewModel game);
        Task<Game?> Edit(EditGameFormViewModel model);
        bool Delete (int id);
    }
}
