using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;
using TareasMVC.Models;
using TareasMVC.Servicios;

namespace TareasMVC.Controllers
{
    [Route("api/tareas")]
    public class TareasController : ControllerBase// para poder recibir datos tipo api 
    {
        public readonly ApliationDBContext context;
        private readonly IservicioUsuarios servicioUsuarios;
        private readonly IMapper mapper;

        public TareasController(ApliationDBContext context, IservicioUsuarios ServicioUsuarios,
            IMapper mapper)
        {
            this.context = context;
            servicioUsuarios = ServicioUsuarios;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tarea>> Get(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var tarea = await context.Tareas
                .Include(t=>t.Pasos)
                .Include(t=> t.ArchivosAdjuntos.OrderBy(o=> o.Orden))
                .FirstOrDefaultAsync(t => t.Id == id &&
            t.UsuarioCreacionId == usuarioId);

            if (tarea is null)
            {
                return NotFound();
            }

            return tarea;

        }

        [HttpGet]
        public async Task<ActionResult<List<TareaDTO>>> Get()
        {
            
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tareas = await context.Tareas
                .Where(t => t.UsuarioCreacionId == usuarioId)
               .OrderBy(t => t.Orden)// se aplica el orderBy de manera mas sensilla con linq de manera en que se necesite
               //.Select(t => new TareaDTO
               //{
               //   Id= t.Id,
               //    Titulo = t.Titulo,
               //})
               .ProjectTo<TareaDTO>(mapper.ConfigurationProvider)// a diferencia del list, el automapper ayuda a que se traiga la antidad de campos que se tienen en el modelo en caso de ser muchos campos 
                .ToListAsync();// se traen los usuarios filtrados por el Id


            return tareas;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody] string titulo)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var existenTareas = await context.Tareas.AnyAsync(t => t.UsuarioCreacionId == usuarioId);
            var OrdenMayor = 0;
            if (existenTareas)
            {
                OrdenMayor = await context.Tareas.Where(t => t.UsuarioCreacionId == usuarioId)
                    .Select(t => t.Orden).MaxAsync();

            }
            var tarea = new Tarea
            {
                Titulo = titulo,
                UsuarioCreacionId = usuarioId,
                FechaCreacion = DateTime.Now,
                Orden = OrdenMayor + 1
            };

            context.Add(tarea);
            await context.SaveChangesAsync();

            return tarea;

        }

        [HttpPost("ordenar")]
        public async Task<IActionResult> Ordenar([FromBody] int[] ids)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var tareas = await context.Tareas
                .Where(t => t.UsuarioCreacionId == usuarioId).ToListAsync();

            var tareasId = tareas.Select(t => t.Id);

            var idsTareasNoPertenecenAlUsuario = ids.Except(tareasId).ToList();

            if (idsTareasNoPertenecenAlUsuario.Any())
            {
                return Forbid();
            }

            var tareasDiccionario = tareas.ToDictionary(x => x.Id);

            for (int i = 0; i < ids.Length; i++)
            {
                var id = ids[i];
                var tarea = tareasDiccionario[id];
                tarea.Orden = i + 1;
            }

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditarTarea(int id, [FromBody] TareaEditarDTO tareaEditarDTO)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var tarea = await context.Tareas.FirstOrDefaultAsync(t => t.Id == id &&
            t.UsuarioCreacionId == usuarioId);

            if (tarea is null)
            {
                return NotFound();
            }

            tarea.Titulo = tareaEditarDTO.Titulo;
            tarea.Descripcion = tareaEditarDTO.Descripcion;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var tarea = await context.Tareas.FirstOrDefaultAsync(t => t.Id == id &&
            t.UsuarioCreacionId == usuarioId);

            if (tarea is null)
            {
                return NotFound();
            }

            context.Remove(tarea);
            await context.SaveChangesAsync();
            return Ok();
        }


    }
}
