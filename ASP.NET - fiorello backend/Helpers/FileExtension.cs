namespace ASP.NET___fiorello_backend.Helpers
{
    public static class FileExtension
    {
        public static bool CheckFileType(IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static bool CheckFileSize(IFormFile file)
        {
            return file.Length / 1024 > 70;
        }

        public static async Task SaveFileAsync(string rootPath, string folder, string file, IFormFile fileName)
        {
            string path = Path.Combine(rootPath, folder, file);
            using FileStream stream = new (path, FileMode.Create);
            await fileName.CopyToAsync(stream);
        }
    }

}
