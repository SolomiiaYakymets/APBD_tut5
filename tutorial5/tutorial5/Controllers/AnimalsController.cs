using Microsoft.AspNetCore.Mvc;
using tutorial5.Services;

namespace tutorial5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly AnimalsService _animalsService;

    public AnimalsController()
    {
        _animalsService =
            new("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True");
    }

    [HttpGet]
    public IActionResult GetStudents(string orderBy)
    {
        var animals = _animalsService.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] Animal animal)
    {
        var affectedCount = _animalsService.AddAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult UpdateStudent(int id, Animal student)
    {
        var affectedCount = _animalsService.UpdateAnimal(id, student);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteStudent(int id)
    {
        var affectedCount = _animalsService.DeleteAnimal(id);
        return NoContent();
    }
    
}
