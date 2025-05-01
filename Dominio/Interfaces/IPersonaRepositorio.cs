using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IPersonaRepositorio
    {
        public Task<IEnumerable<Persona>> ObtenerPersonas();
        public Task<Persona?> ObtenerPorId(int id);
        public Task<Persona?> ObtenerPorCedula(long cedula);
        public Task Crear(Persona persona);
        public Task Actualizar(Persona persona);
        public Task Eliminar(int id);

    }
}
