namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath = "/assets/images/games";
        public const string AllowedExtensions = ".png,.jpeg,.jpg";
        public const int MaxImageSizeInMB = 1;
        public const int MaxImageSizeInB = MaxImageSizeInMB * 1024* 1024;
    }
}
