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
            _imagesPath = $"{_webHostEnvironment.WebRootPath}/assets/images/games";
        }

        public async Task Create (CreateGameFormViewModel model)
        {
            string coverName = $"cover_{Guid.NewGuid()}.{Path.GetExtension(model.Cover.FileName)}";
            string coverPath = Path.Combine(_imagesPath, coverName);
            using var coverStream = File.Create(coverPath);
            await model.Cover.CopyToAsync(coverStream);
            Game game = new ()
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
    }
}
