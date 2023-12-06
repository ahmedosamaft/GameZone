using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController (ICategoriesService categoryService, IDevicesService devicesService, IGamesService gamesService) : Controller
    {
        private readonly ICategoriesService categoryService = categoryService;
        private readonly IDevicesService devicesService = devicesService;
        private readonly IGamesService gamesService = gamesService;

        public IActionResult Index ( )
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create ( )
        {
            CreateGameFormViewModel viewModel = new CreateGameFormViewModel
            {
                Categories = categoryService.GetSelectList(),
                Devices = devicesService.GetSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (CreateGameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = categoryService.GetSelectList();
                viewModel.Devices = devicesService.GetSelectList();
                return View(viewModel);
            }
            await gamesService.Create(viewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
