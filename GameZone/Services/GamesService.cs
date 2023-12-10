using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GamesService (ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public IEnumerable<Game> GetAll ( )
        {
            return _context.Games.Include(g => g.Category).Include(g => g.Devices).ThenInclude(d => d.Device).AsNoTracking();
        }

        public Game? GetById (int id)
        {
            return _context.Games.Where(g => g.Id == id).Include(g => g.Category).Include(g => g.Devices).ThenInclude(d => d.Device).AsNoTracking().SingleOrDefault();
        }
        public async Task Create (CreateGameFormViewModel model)
        {
            string coverName = await SaveImage(model.Cover);
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                Cover = coverName,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(deviceId => new GameDevice { DeviceId = deviceId }).ToList()
            };
            _context.Add(game);
            _context.SaveChanges();
        }
        public async Task<Game?> Edit (EditGameFormViewModel model)
        {
            var game = _context.Games.Where(g => g.Id == model.Id).Include(g => g.Devices).SingleOrDefault();
            if (game == null)
            {
                return null;
            }
            var oldCover = game.Cover;
            var hasNewCover = model.Cover is not null;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (hasNewCover) { game.Cover = await SaveImage(model.Cover!); }
            var effectedRows = await _context.SaveChangesAsync();
            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return game;
            } else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }
        public bool Delete (int id)
        {
            bool isDeleted = false;
            var game = _context.Games.Where(g => g.Id == id).SingleOrDefault();
            if (game == null) { return isDeleted; }
            var gameCover = game.Cover;
            _context.Games.Remove(game);
            var effectedRows = _context.SaveChanges();
            if(effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, gameCover);
                File.Delete(cover);
            }
            return isDeleted;
        }
        private async Task<string> SaveImage (IFormFile cover)
        {
            string coverName = $"cover_{Guid.NewGuid()}.{Path.GetExtension(cover.FileName)}";
            string coverPath = Path.Combine(_imagesPath, coverName);
            using var coverStream = File.Create(coverPath);
            await cover.CopyToAsync(coverStream);
            return coverName;
        }
 
    }
}
