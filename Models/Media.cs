using System;
using System.IO;

public class MediaEntity
{
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public string RelatedContent { get; set; }
    
    public MediaEntity(string filePath, string fileType, string relatedContent)
    {
        FilePath = Environment.GetEnvironmentVariable(filePath) ?? filePath;
        FileType = fileType;
        RelatedContent = relatedContent;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"File Path: {FilePath}");
        Console.WriteLine($"File Type: {FileType}");
        Console.WriteLine($"Related Content: {RelatedContent}");
    }
}

public static class MediaEntityExtensions
{
    public static bool IsValidFilePath(this MediaEntity media)
    {
        return File.Exists(media.FilePath);
    }
    
    public static void UpdateRelatedContent(this MediaEntity media, string newContent)
    {
        media.RelatedContent = newContent;
    }
}

class Program
{
    static void Main(string[] args)
    {
        MediaEntity media = new MediaEntity("example.jpg", "image", "An example image file");
        
        bool fileExists = media.IsValidFilePath();
        Console.WriteLine($"Does the file exist? {fileExists}");
        
        media.UpdateRelatedContent("Updated related content");
        media.DisplayInfo();
    }
}