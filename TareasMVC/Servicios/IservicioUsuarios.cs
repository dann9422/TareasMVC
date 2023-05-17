using System.Security.Claims;

namespace TareasMVC.Servicios
{
    public interface IservicioUsuarios
    {
        string ObtenerUsuarioId();
    }
    public class ServicioUsuarios : IservicioUsuarios
    {
        private HttpContext HttpContext;

        public ServicioUsuarios(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }



        public string ObtenerUsuarioId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var idCliam = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

                return idCliam.Value;
            }
            else
            {
                throw new Exception("El usuario no esta autenticado");
            }
        }
    }
}
