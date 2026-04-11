using System.Net.NetworkInformation;

namespace Ecommerce.Util
{
    public class FileStorageUtil
    {
        private readonly IWebHostEnvironment _environment;
        public FileStorageUtil(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string?> UploadImage(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) { return null; }

            string wwwRootPath = _environment.WebRootPath;
            string contentPath = Path.Combine(wwwRootPath, "Images", folderName);
            if (!Directory.Exists(contentPath))
            {
                Directory.CreateDirectory(contentPath);
            }
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            string fullFileName = fileName + extension;
            string physicalPath = Path.Combine(contentPath, fullFileName);

            using (var fileStream = new FileStream(physicalPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Path.Combine("Images", folderName, fullFileName).Replace("\\", "/");
        }

        public void DeleteImage(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            string fullPath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
