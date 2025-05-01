using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IPersonaServicio
    {
        public Task Crear(PersonaDTO persona);
        public Task Actualizar(PersonaDTO persona);
        public Task Eliminar(int id);
        public Task<IEnumerable<PersonaDTO>> ObtenerPersonas();
        public Task<PersonaDTO> ObtenerPersonaPorId(int id);
        public Task<PersonaDTO> ObtenerPersonaPorCedula(long cedula);
    }
}
