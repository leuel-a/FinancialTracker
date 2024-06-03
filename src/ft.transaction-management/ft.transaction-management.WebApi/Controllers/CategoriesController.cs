using MediatR;
using Microsoft.AspNetCore.Mvc;
using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Features.Category.Requests.Queries;
using ft.transaction_management.Application.Features.Category.Requests.Commands;

namespace ft.transaction_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>An ActionResult containing the category details.</returns>
    /// <response code="200">Returns the category details.</response>
    /// <response code="400">If the category is not found.</response>
    [HttpGet("{id:int}", Name = "GetCategoryById")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var response = await _mediator.Send(new GetSingleCategoryQuery() { Id = id });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message });
        return Ok(new { response.Message });
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="categoryDto">The category data transfer object.</param>
    /// <returns>An ActionResult containing the created category details.</returns>
    /// <response code="201">Returns the created category details.</response>
    /// <response code="400">If the category could not be created.</response>
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
    {
        var response = await _mediator.Send(new CreateCategoryCommand() { CategoryDto = categoryDto });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        return CreatedAtRoute("GetCategoryById", new { Id = response.CreatedResource!.Id }, response.CreatedResource);
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="categoryDto">The updated category data transfer object.</param>
    /// <returns>An ActionResult containing the update status.</returns>
    /// <response code="200">If the category was successfully updated.</response>
    /// <response code="400">If the category could not be updated.</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
    {
        categoryDto.Id = id;
        var response = await _mediator.Send(new UpdateCategoryCommand() { CategoryDto = categoryDto });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, response.ErrorMessages });
        return Ok(new { response.Message });
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>An ActionResult containing the deletion status.</returns>
    /// <response code="200">If the category was successfully deleted.</response>
    /// <response code="400">If the category could not be deleted.</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var response = await _mediator.Send(new DeleteCategoryCommand() { Id = id });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        return Ok(new { Message = response.Message });
    }

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>An ActionResult containing the list of all categories.</returns>
    /// <response code="200">Returns the list of categories.</response>
    /// <response code="400">If there was an error retrieving the categories.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var response = await _mediator.Send(new GetAllCategoriesQuery());

        if (response.Succeeded == false)
            return BadRequest(new { response.Message });
        return Ok(response.Resources);
    }
}