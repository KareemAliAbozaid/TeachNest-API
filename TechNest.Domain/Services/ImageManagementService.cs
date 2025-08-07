using Microsoft.AspNetCore.Http;

namespace TechNest.Domain.Services
{
    public class ImageManagementService : IImageManagementService
    {
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> savedImagePaths = new List<string>();
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", src);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var savePath = Path.Combine(imageDirectory, fileName);
                    var imageUrl = $"/Images/{src}/{fileName}";

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedImagePaths.Add(imageUrl);
                }
            }

            return savedImagePaths;
        }

        public void DeletImageAsync(string relativePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            else
            {
                throw new FileNotFoundException($"The file '{relativePath}' does not exist.");
            }
        }
    }
}
