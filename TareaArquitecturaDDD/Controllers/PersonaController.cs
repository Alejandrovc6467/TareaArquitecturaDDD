using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaServicio _personaServicio;

        public PersonaController(IPersonaServicio personaServicio)
        {
            this._personaServicio = personaServicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaDTO>>> ObtenerPersonas()
        {
            try
            {
                var personas = await _personaServicio.ObtenerPersonas();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<PersonaDTO>> ObtenerPersonaPorCedula(long cedula)
        {
            try
            {
                var persona = await _personaServicio.ObtenerPersonaPorCedula(cedula);
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<PersonaDTO>> ObtenerPersonaPorId(int id)
        {
            try
            {
                var persona = await _personaServicio.ObtenerPersonaPorId(id);
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> EliminarPersona(int id)
        {
            try
            {
                await _personaServicio.Eliminar(id);
                return Ok(new { mensaje = "Persona eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> CrearPersona([FromBody] PersonaDTO persona)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _personaServicio.Crear(persona);
                return Ok(new { mensaje = "Persona creada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPut]
        public async Task<IActionResult> ActualizarPersona([FromBody] PersonaDTO persona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _personaServicio.Actualizar(persona);
                return Ok(new { mensaje = "Persona actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }



    }
}
