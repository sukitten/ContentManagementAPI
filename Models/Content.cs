using System;
using System.Collections.Generic;

public class ContentModel
{
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime PublicationDate { get; set; }
    public List<string> AssociatedMedia { get; set; }

    public ContentModel() : this(string.Empty, string.Empty)
    {
    }

    public ContentModel(string title, string body, DateTime publicationDate = default, List<string> associatedMedia = null)
    {
        Title = title;
        Body = body;
        PublicationDate = publicationDate == default ? DateTime.Now : publicationDate;
        AssociatedMedia = associatedMedia ?? new List<string>();
    }
}