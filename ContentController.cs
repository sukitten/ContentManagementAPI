using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class Content
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ContentController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllContents()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public IActionResult GetContentById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult CreateContent([FromBody] Content content)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateContent(int id, [FromBody] Content content)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteContent(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("category")]
    public IActionResult CreateCategory([FromBody] Category category)
    {
        throw new NotImplementedException();
    }

    [HttpGet("category")]
    public IActionResult GetAllCategories()
    {
        throw new NotImplementedException();
    }

    [HttpPut("category/{id}")]
    public IActionResult UpdateCategory(int id, [FromBody] Category category)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("category/{id}")]
    public IActionResult DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }
}