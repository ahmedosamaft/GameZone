using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController (ICategoriesService categoryService, IDevicesService devicesService, IGamesService gamesService) : Controller
    {
        private readonly ICategoriesService _categoryService = categoryService;
        private readonly IDevicesService _devicesService = devicesService;
        private readonly IGamesService _gamesService = gamesService;

        public IActionResult Index ( )
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        [HttpGet]
        public IActionResult Create ( )
        {
            CreateGameFormViewModel viewModel = new CreateGameFormViewModel
            {
                Categories = _categoryService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (CreateGameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _categoryService.GetSelectList();
                viewModel.Devices = _devicesService.GetSelectList();
                return View(viewModel);
            }
            await _gamesService.Create(viewModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details (int id)
        {
            var game = _gamesService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpGet]
        public IActionResult Edit (int id)
        {
            var game = _gamesService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoryService.GetSelectList(),
                Devices = _devicesService.GetSelectList(),
                CurrentCover = game.Cover,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (EditGameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var game = await _gamesService.Edit(viewModel);
            if (game == null)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete (int id)
        {
            var isDeleted = _gamesService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
