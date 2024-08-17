using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Assuming use of Entity Framework Core for database access
using System.Collections.Generic;

public class ContentController : ControllerBase
{
    private readonly YourDbContext _context; // Your database context

    public ContentController(YourDbContext context)
    {
        _context = context;
    }

    // GET: api/contents
    [HttpGet]
    public IActionResult GetAllContents()
    {
        try
        {
            var contents = _context.Contents.Include(c => c.Category).ToList();
            return Ok(contents);
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            return StatusCode(500, "An error occurred while retrieving data. Please try again later.");
        }
    }

    // GET: api/contents/{id}
    [HttpGet("{id}")]
    public IActionResult GetContentById(int id)
    {
        try
        {
            var content = _context.Contents.Include(c => c.Category).FirstOrDefault(c => c.Id == id);

            if (content == null)
            {
                return NotFound("Content not found.");
            }

            return Ok(content);
        }
        catch (Exception ex)
        {
            // Log exception
            return StatusCode(500, "An error occurred while retrieving the content. Please try again later.");
        }
    }

    // POST: api/contents
    [HttpPost]
    public IActionResult CreateContent([FromBody] Content content)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _context.Contents.Add(content);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetContentById), new { id = content.Id }, content);
        }
        catch (Exception ex)
        {
            // Log exception
            return StatusCode(500, "An error occurred while creating the content. Please try again later.");
        }
    }

    // PUT: api/contents/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateContent(int id, [FromBody] Content content)
    {
        if (id != content.Id)
        {
            return BadRequest("Content ID mismatch.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _context.Entry(content).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!ContentExists(id))
            {
                return NotFound("Content not found.");
            }
            else
            {
                // Log exception
                return StatusCode(500, "An error occurred while updating the content. Please try again later.");
            }
        }
    }

    // DELETE: api/contents/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteContent(int id)
    {
        try
        {
            var content = _context.Contents.Find(id);
            if (content == null)
            {
                return NotFound("Content not found.");
            }

            _context.Contents.Remove(content);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            // Log exception
            return StatusCode(500, "An error occurred while deleting the content. Please try again later.");
        }
    }

    // Additional methods for Category operations with similar error handling...

    private bool ContentExists(int id)
    {
        return _context.Contents.Any(e => e.Id == id);
    }

    // Assuming YourDbContext is your EF Core database context
    // Replace YourDbContext with the actual name of your database context class
}