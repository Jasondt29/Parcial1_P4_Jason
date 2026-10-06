using Microsoft.AspNetCore.Mvc;
using Parcial1_P4_Jason.Models;
using Parcial1_P4_Jason.Services;

namespace Parcial1_P4_Jason.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumberController(NumbersService numbersService) : ControllerBase
    {
        [HttpGet("{number:int}")]
        public IActionResult sumar(int number)
        {
            return Ok(number + number);
        }
        [HttpGet("historial")]
        public async Task<IActionResult> GetHistorial()
        {
            var historial = await numbersService.GetListAsync();
            return Ok(historial);
        }
        [HttpGet("historial/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var number = await numbersService.GetByIdAsync(id);
            if (number == null)
            {
                return NotFound();
            }
            return Ok(number);
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] NumberRecord number)
        {
            var nuevo = number with
            {
                Fecha = DateTime.UtcNow,
                Resultado = number.Numero + number.Numero
            };

            await numbersService.SaveAsync(nuevo);
            return Ok(nuevo);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] NumberRecord number)
        {
            var row = await numbersService.GetByIdAsync(id);

            if (row == null)
            {
                return NotFound();
            }

            var actualizado = number with
            {
                Id = id,
                Fecha = DateTime.UtcNow,
                Resultado = number.Numero + number.Numero
            };

            await numbersService.UpdateAsync(actualizado);

            return Ok(new
            {
                Numero_Anterior = row.Numero,
                Resultado_Anterior = row.Resultado,
                Numero_Actual = actualizado.Numero,
                Resultado_Actual = actualizado.Resultado
            });
        }
    }
}