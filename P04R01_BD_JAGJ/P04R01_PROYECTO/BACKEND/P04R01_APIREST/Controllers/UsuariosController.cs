using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P04R01_APIREST.Context;
using P04R01_APIREST.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace P04R01_APIREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //variable con contexto de solo lectura, viene de la clase App
        private readonly AppDbContext context;

        //constructor, inicializas contexto, es decir llama la entidad
        public UsuariosController(AppDbContext context)
        {
            this.context = context;
        }

        //metodos de la apirest

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.TBLUsuarios.ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]

        public ActionResult Get(int id)
        {
            try
            {
                var usuario = context.TBLUsuarios.Find(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public ActionResult Post([FromBody] TBLUsuarios usuarios)
        {
            try
            {
                context.TBLUsuarios.Add(usuarios);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        //put: api/Usuarios/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TBLUsuarios usuario) 
        {
            try
            {
                //Solo se actualizan los campos básicos; así no se borran las columnas del registro Thona
                var existente = context.TBLUsuarios.Find(id);
                if (existente == null)
                {
                    return NotFound();
                }
                existente.nombre = usuario.nombre;
                existente.hobby = usuario.hobby;
                existente.fechanacimiento = usuario.fechanacimiento;
                existente.email = usuario.email;
                context.SaveChanges();
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            try
            {
                var usuario = context.TBLUsuarios.Find(id);
                if(usuario == null)
                {
                    return NotFound();
                }
                context.TBLUsuarios.Remove(usuario);
                context.SaveChanges();
                return Ok();

            }
            catch (Exception ex) 
            
            {
                return BadRequest(ex.Message);
            }
        
        }

     
    }//fin de la clase

}//fim del namespace
