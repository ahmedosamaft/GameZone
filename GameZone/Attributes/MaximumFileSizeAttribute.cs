
namespace GameZone.Attributes
{
    public class MaximumFileSizeAttribute : ValidationAttribute
    {
        private readonly int maximumFileSize;

        public MaximumFileSizeAttribute (int maximumFileSize)
        {
            ErrorMessage = $"Maximum allowed file size is {maximumFileSize / 1024 / 1024} bytes";
            this.maximumFileSize = maximumFileSize;
        }
        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                if (file.Length > maximumFileSize)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
