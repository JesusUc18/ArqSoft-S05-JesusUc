using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController : ControllerBase
    {
        private readonly CalculadoraService _service;

        public CalculadoraController(CalculadoraService service)
        {
            _service = service;
        }

        [HttpGet("sumar")]
        public IActionResult Sumar([FromQuery] double a, [FromQuery] double b)
            => Ok(new { operacion = "suma", a, b, resultado = _service.Sumar(a, b) });

        [HttpGet("restar")]
        public IActionResult Restar([FromQuery] double a, [FromQuery] double b)
            => Ok(new { operacion = "resta", a, b, resultado = _service.Restar(a, b) });

        [HttpGet("multiplicar")]
        public IActionResult Multiplicar([FromQuery] double a, [FromQuery] double b)
            => Ok(new { operacion = "multiplicacion", a, b, resultado = _service.Multiplicar(a, b) });

        [HttpGet("dividir")]
        public IActionResult Dividir([FromQuery] double a, [FromQuery] double b)
        {
            try
            {
                return Ok(new { operacion = "division", a, b, resultado = _service.Dividir(a, b) });
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}