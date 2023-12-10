using GameZone.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
      
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaximumFileSize(FileSettings.MaxImageSizeInB)]
        public IFormFile Cover { get; set; } = default!;
    }
}
