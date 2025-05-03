using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class PersonaRepositorio : IPersonaRepositorio
    {

        private readonly ContextoDB _contextoDB;


        public PersonaRepositorio(ContextoDB contextoDB)
        {
            this._contextoDB = contextoDB;
        }

        public async Task<IEnumerable<Persona>> ObtenerPersonas()
        {
            try
            {
                var personas = await _contextoDB.Personas
                                .Where(p => !p.Eliminado)  // Filtro para borrado lógico
                                .ToListAsync();

                if (personas == null || !personas.Any())
                    throw new Exception("No se encontraron personas");

                return personas;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<Persona?> ObtenerPorCedula(long cedula)
        {

            try
            {
                var persona = await _contextoDB.Personas
                   .Where(p => p.Cedula == cedula && !p.Eliminado) // Filtro por cedula  y estado activo
                   .FirstOrDefaultAsync();
                if (persona == null)
                    throw new Exception("No se encontro la persona con la cedula: " + cedula);
                return persona;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }


        public async Task<Persona?> ObtenerPorId(int id)
        {

            try
            {
                var persona = await _contextoDB.Personas
                    .Where(p => p.Id == id && !p.Eliminado) // Filtro por ID y estado activo
                    .FirstOrDefaultAsync();
                return persona;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la persona por ID: " + ex.Message);
            }

        }


        public async Task Eliminar(int id)
        {

            try
            {
                var persona = await _contextoDB.Personas
                             .FirstOrDefaultAsync(p => p.Id == id);

                if (persona == null)
                    throw new Exception("No se encontró ninguna persona con ese ID");

                persona.Eliminado = true;

                //await _contextoDB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la persona: " + ex.Message);
            }


        }


        public async Task Crear(Persona persona)
        {

            // create a person but first check if the person already exists by cedula
            var existePersona = await _contextoDB.Personas
                .AnyAsync(p => p.Cedula == persona.Cedula && !p.Eliminado);
            if (existePersona)
                throw new Exception("Ya existe una persona con esa cedula");
            try
            {
                await _contextoDB.Personas.AddAsync(persona);
                //await _contextoDB.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la persona: " + ex.Message);
            }

        }


        public async Task Actualizar(Persona persona)
        {
            try
            {
                var personaExistente = await _contextoDB.Personas
                    .FirstOrDefaultAsync(p => p.Id == persona.Id && !p.Eliminado);

                if (personaExistente == null)
                    throw new Exception("No se encontró ninguna persona activa con ese ID");

                // Verificar que la cédula no esté en uso por otra persona
                var cedulaEnUso = await _contextoDB.Personas
                    .AnyAsync(p => p.Cedula == persona.Cedula && p.Id != persona.Id && !p.Eliminado);

                if (cedulaEnUso)
                    throw new Exception("Ya existe otra persona con esa cédula");

                // Actualizar propiedades
                personaExistente.Cedula = persona.Cedula;
                personaExistente.Nombre = persona.Nombre;
                personaExistente.Edad = persona.Edad;
                personaExistente.Genero = persona.Genero;
                personaExistente.Telefono = persona.Telefono;

                //await _contextoDB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la persona: " + ex.Message);
            }
        }




    }
}
