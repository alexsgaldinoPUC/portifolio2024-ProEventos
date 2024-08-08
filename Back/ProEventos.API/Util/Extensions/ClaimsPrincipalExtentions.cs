using System.Security.Claims;

namespace ProEventos.API.Util.Extensions
{
    public static class ClaimsPrincipalExtentions
    {
        public static string GetUserName(this ClaimsPrincipal usuario)
        {
            Console.WriteLine(usuario.FindFirst(ClaimTypes.Name)?.Value);
            return usuario.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal usuario)
        {
            return int.Parse(usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
