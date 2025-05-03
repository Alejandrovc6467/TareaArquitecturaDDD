using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class PersonaServicio : IPersonaServicio
    {

        private readonly IPersonaRepositorio _personaRepositorio;

        private readonly IUnitOfWork _unitOfWork;

        public PersonaServicio(IPersonaRepositorio personaRepositorio, IUnitOfWork unitOfWork)
        {
            this._personaRepositorio = personaRepositorio;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<PersonaDTO>> ObtenerPersonas()
        {

            var personas = await _personaRepositorio.ObtenerPersonas();

            if (personas == null || !personas.Any())
                throw new Exception("No se encontraron personas");

            // get persons and casteralas to personDTO
            return personas.Select(p => new PersonaDTO
            {
                Id = p.Id,
                Cedula = p.Cedula,
                Nombre = p.Nombre,
                Edad = p.Edad,
                Genero = p.Genero,
                Telefono = p.Telefono
            });

        }

        public async Task<PersonaDTO> ObtenerPersonaPorCedula(long cedula)
        {
            try
            {
                var persona = await this._personaRepositorio.ObtenerPorCedula(cedula);

                if (persona == null)
                    throw new Exception("No se encontró ninguna persona con esa cédula");

                return new PersonaDTO
                {
                    Id = persona.Id,
                    Cedula = persona.Cedula,
                    Nombre = persona.Nombre,
                    Edad = persona.Edad,
                    Genero = persona.Genero,
                    Telefono = persona.Telefono
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al obtener por cédula: " + ex.Message);
            }
        }



        public async Task<PersonaDTO> ObtenerPersonaPorId(int id)
        {
            try
            {
                var persona = await _personaRepositorio.ObtenerPorId(id);

                if (persona == null)
                    throw new Exception("No se encontró ninguna persona con ese ID");

                return new PersonaDTO
                {
                    Id = persona.Id,
                    Cedula = persona.Cedula,
                    Nombre = persona.Nombre,
                    Edad = persona.Edad,
                    Genero = persona.Genero,
                    Telefono = persona.Telefono
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al obtener por ID: " + ex.Message);
            }
        }



        public async Task Eliminar(int id)
        {
            try
            {
                await _personaRepositorio.Eliminar(id);
                await this._unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al eliminar: " + ex.Message);
            }
        }



        public async Task Crear(PersonaDTO persona)
        {
            try
            {
                var nuevaPersona = new Persona
                {
                    Cedula = persona.Cedula,
                    Nombre = persona.Nombre,
                    Edad = persona.Edad,
                    Genero = persona.Genero,
                    Telefono = persona.Telefono,
                    Eliminado = false // asegurarse de que se cree activa
                };

                await _personaRepositorio.Crear(nuevaPersona);
                await this._unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al crear la persona: " + ex.Message);
            }
        }



        public async Task Actualizar(PersonaDTO persona)
        {
            try
            {
                var personaEntity = new Persona
                {
                    Id = persona.Id,
                    Cedula = persona.Cedula,
                    Nombre = persona.Nombre,
                    Edad = persona.Edad,
                    Genero = persona.Genero,
                    Telefono = persona.Telefono
                };

                await _personaRepositorio.Actualizar(personaEntity);
                await this._unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al actualizar la persona: " + ex.Message);
            }


        }




    }
}
