namespace StockRich.API.Tests;

public class FileHelper
{
    public static async Task<string> ReadFromFileAsync(string fileName)
    {
        using var sr = new StreamReader(fileName);
        var fileContent = await sr.ReadToEndAsync();
        return fileContent;
    }
}