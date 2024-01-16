using Microsoft.AspNetCore.Http;
using System.IO;

namespace MoviesRating.Services
{
    public class Upload
    {
        public static string UploadPosterImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "";//
            }
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Posters", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
