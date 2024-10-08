namespace Spedito.Helpers
{
    public class FileHelper
    {
        public static bool HasValidSize(IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }

        public static bool IsImage(IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static void DeleteFile(string root, string path, string filname)
        {
            string fullpath = Path.Combine(root, path, filname);

            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }
    }
}
