using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string allowedExtensions;

        public AllowedExtensionsAttribute (string allowedExtensions)
        {
            ErrorMessage = "Only .jpg, .jpeg, .png is allowed";
            this.allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                string extension = Path.GetExtension(file.FileName);
                if (!allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
