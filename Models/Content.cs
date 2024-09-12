using System;
using System.Collections.Generic;

public class ContentModel
{
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime PublicationDate { get; set; }
    public List<string> AssociatedMedia { get; set; }

    public ContentModel()
    {
        Title = string.Empty;
        Body = string.Empty;
        PublicationDate = DateTime.Now;
        AssociatedMedia = new List<string>();
    }
}