using Ardalis.GuardClauses;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.CategoryAggregate.Specifications;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.Entities.SportAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class CategoryService : ICategoryService
{
  private readonly IRepository<Category> _categoryRepository;

  public CategoryService(IRepository<Category> categoryRepository)
  {
    _categoryRepository = categoryRepository;
  }

  public async Task<Category> CreateCategory(string name)
  {
    var category = await _categoryRepository.GetBySpecAsync(new CategoryByNameSpec(name));
    Guard.Against.EntityAlreadyExists(category, nameof(name), name);
    
    category = new Category(name);
    await _categoryRepository.AddAsync(category);
    return category;
  }
  
  public async Task<List<Category>> GetAllCategories() => await _categoryRepository.ListAsync();
  
  public async Task<Category> UpdateCategoryName(int id, string name)
  {
    var category = await _categoryRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(category, nameof(id), id.ToString());    
    
    var sameNameCategory = await _categoryRepository.GetBySpecAsync(new CategoryByNameSpec(name));
    Guard.Against.EntityAlreadyExists(sameNameCategory, nameof(name), name);

    category.UpdateName(name);        
    await _categoryRepository.UpdateAsync(category);
    return category;
  }
 
  public async Task DeleteCategory(int id)
  {
    var category = await _categoryRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(category, nameof(id), id.ToString());    
    await _categoryRepository.DeleteAsync(category);
  }
  
}

public interface ICategoryService
{
  Task<Category> CreateCategory(string name);
  Task<List<Category>> GetAllCategories();
  Task<Category> UpdateCategoryName(int id, string name);
  Task DeleteCategory(int id);
}
