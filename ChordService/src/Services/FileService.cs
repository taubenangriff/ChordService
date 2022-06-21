

namespace ChordService.Services
{
    public class FileService
    {

        public async Task<Stream?> GetSampleFileAsync()
        {
            return await GetFileAsync("res/sample_long.mp3");
        }

        private async Task<Stream?> GetFileAsync(String Filename)
        {
            return await Task.Run(
                () =>
                {
                    try
                    {
                        return File.OpenRead(Filename);
                    }
                    catch (FileNotFoundException e)
                    {
                        return null;
                    }
                }
            );
        }
    }

}
