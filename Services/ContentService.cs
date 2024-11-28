using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IContentRepository
{
    Task<Content> CreateContentAsync(Content content);
    Task<Content> UpdateContentAsync(Content content);
    Task<bool> DeleteContentAsync(Guid contentId);
    Task<IEnumerable<Content>> GetAllContentsAsync();
    Task<Content> GetContentByIdAsync(Guid contentId);
}

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Guid categoryId);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(Guid categoryId);
}

public interface IContentService
{
    Task<Content> CreateContentAsync(Content content);
    Task<Content> UpdateContentAsync(Content content);
    Task<bool> DeleteContentAsync(Guid contentId);
    Task<IEnumerable<Content>> GetAllContentsAsync();
    Task<Content> GetContentByIdAsync(Guid contentId);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Guid categoryId);
}

public class Content
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public Guid CategoryId { get; set; }
}

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class ContentService : IContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ContentService(IContentRepository contentRepository, ICategoryRepository categoryRepository)
    {
        _contentRepository = contentRepository ?? throw new ArgumentNullException(nameof(contentRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<Content> CreateContentAsync(Content content) => await _contentRepository.CreateContentAsync(content);

    public async Task<Content> UpdateContentAsync(Content content) => await _contentRepository.UpdateContentAsync(content);

    public async Task<bool> DeleteContentAsync(Guid contentId) => await _contentRepository.DeleteContentAsync(contentId);

    public async Task<IEnumerable<Content>> GetAllContentsAsync() => await _contentRepository.GetAllContentsAsync();

    public async Task<Content> GetContentByIdAsync(Guid contentId) => await _contentRepository.GetContentByIdAsync(contentId);

    public async Task<Category> CreateCategoryAsync(Category category) => await _categoryRepository.CreateCategoryAsync(category);

    public async Task<Category> UpdateCategoryAsync(Category category) => await _categoryRepository.UpdateCategoryAsync(category);

    public async Task<bool> DeleteCategoryAsync(Guid categoryId) => await _categoryRepository.DeleteCategoryAsync(categoryId);
}