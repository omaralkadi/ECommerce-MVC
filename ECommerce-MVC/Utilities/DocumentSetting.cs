namespace ECommerce_MVC.Utilities
{
    public static class DocumentSetting
    {
        public static string Upload(IFormFile file, string FolderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", FolderName);
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            var filePath = Path.Combine(FolderPath, fileName);

            using (var Stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(Stream);
            }

            return fileName;
        }

        public static void Delete(string FileName, string FolderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", FolderName, FileName);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
