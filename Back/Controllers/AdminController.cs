using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{    
    private const String nomeUnico = "SISTEMA";
    private const String senhaUnica = "canditado123";
    
    [HttpGet("login")]
    public IActionResult Login(string usuario, string senha)
    {
        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
        {
            return Unauthorized("Usuário ou senha vazios.");
        }

        if (usuario.Equals(nomeUnico) && senha.Equals(senhaUnica))
        {
            return Ok();
        }

        return Unauthorized("Erro ao fazer login.");
    }
}