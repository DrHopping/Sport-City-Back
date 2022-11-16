using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.Services;
using SportCity.SharedKernel;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request,
        CancellationToken cancellationToken = new())
    {
        var category = await _categoryService.CreateCategory(request.Name);
        return Ok(_mapper.Map<CategoryResponse>(category));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken = new())
    {
        var cities = await _categoryService.GetAllCategories();
        return Ok(_mapper.Map<List<CategoryResponse>>(cities));
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request,
        CancellationToken cancellationToken = new())
    {
        var category = await _categoryService.UpdateCategoryName(id, request.Name);
        return Ok(_mapper.Map<CategoryResponse>(category));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken = new())
    {
        await _categoryService.DeleteCategory(id);
        return NoContent();
    }
}
