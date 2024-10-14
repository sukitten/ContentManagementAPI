using System;

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