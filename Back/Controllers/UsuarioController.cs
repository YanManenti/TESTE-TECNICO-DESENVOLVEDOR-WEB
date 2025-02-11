using Back.Models;
using Back.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Back.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly Services.MongoDB _mongodb;

    public UsuarioController(Services.MongoDB mongodb)
    {
        _mongodb = mongodb;
    }

    [HttpGet("buscartodos")]
    public async Task<IActionResult> BuscarTodos()
    {
        try
        {
            //Busca de todos os usuários.
            var usuarios = await _mongodb.BuscarTodos();
            return Ok(usuarios);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("buscarum/{cpf}")]
    public async Task<IActionResult> BuscarUm(string cpf)
    {
        //Problemas na requisição.
        if (string.IsNullOrEmpty(cpf))
        {
            return BadRequest("CPF vazio.");
        }
        try
        {
            //Busca do usuário.
            var usuario = await _mongodb.BuscarUm(cpf);
            //Problemas na busca do usuário.
            if (Object.Equals(usuario, null))
            {
                throw new Exception("Usuário não encontrado");
            }
            return Ok(usuario);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    
    [HttpPost("criar")]
    public async Task<IActionResult> Criar([FromBody] Usuario usuario)
    {
        //Problemas na requisição.
        if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Cpf))
        {
            return BadRequest("Nome ou CPF vazios.");
        }
        //Verifica o CPF
        if (!new verifyCpf().IsCpf(usuario.Cpf))
        {
            return BadRequest("CPF não válido.");
        }
        try
        {
            //Problemas no banco de dados.
            var alreadyInDb = await _mongodb.BuscarUm(usuario.Cpf);
            if (!Object.Equals(alreadyInDb, null))
            {
                throw new Exception("CPF já inserido na base de dados.");
            }
            //Inserção.
            await _mongodb.CriarUmAsync(usuario);
            //Problemas na inserção.
            var inserido = await _mongodb.BuscarUm(usuario.Cpf);
            if (Object.Equals(inserido, null))
            {
                throw new Exception("Erro ao procurar o usuário na base de dados.");
            }
            return CreatedAtAction(nameof(Criar),inserido.cpfResposta());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
        
    }

    [HttpPost("remover")]
    public async Task<IActionResult> RemoverUm(string cpf)
    {
        //Problemas na requisição.
        if (string.IsNullOrEmpty(cpf))
        {
            return BadRequest("CPF vazios.");
        }
        try
        {
            //Problemas no banco de dados.
            var usuario = await _mongodb.BuscarUm(cpf);
            if (Object.Equals(usuario, null))
            {
                throw new Exception("Usuário não encontrado.");
            }
            //Remoção.
            await _mongodb.RemoverUmAsync(cpf);
            //Problemas na remoção.
            var usuarioInDb = await _mongodb.BuscarUm(cpf);
            if (!Object.Equals(usuarioInDb, null))
            {
                throw new Exception("Usuário não foi deletado.");
            }
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
        
    }

    [HttpPost("atualizar")]
    public async Task<IActionResult> Atualizar([FromBody] Usuario usuarioBody)
    {
        //Problemas na requisição.
        if (string.IsNullOrEmpty(usuarioBody.Cpf))
        {
            return BadRequest("CPF vazio.");
        }
        try
        {
            //Problemas no banco de dados.
            var usuario = await _mongodb.BuscarUm(usuarioBody.Cpf);
            if (Object.Equals(usuario, null))
            {
                throw new Exception("Usuário não encontrado.");
            }
            //Atualizando o usuário com os novos parametros.
            usuario.Nome = string.IsNullOrEmpty(usuarioBody.Nome) ? usuario.Nome : usuarioBody.Nome;
            usuario.Codigo = string.IsNullOrEmpty(usuarioBody.Codigo) ? usuario.Codigo : usuarioBody.Codigo;
            usuario.Telefone = string.IsNullOrEmpty(usuarioBody.Telefone) ? usuario.Telefone : usuarioBody.Telefone;
            usuario.Endereco = string.IsNullOrEmpty(usuarioBody.Endereco) ? usuario.Endereco : usuarioBody.Endereco;
            //Preparando as variáveis para atualização.
            var filter = Builders<Usuario>.Filter.Eq(user => user.Cpf, usuario.Cpf);
            var update = Builders<Usuario>.Update.Set(user => user.Nome, usuario.Nome)
                .Set(user => user.Endereco, usuario.Endereco)
                .Set(user => user.Codigo, usuario.Codigo)
                .Set(user => user.Telefone, usuario.Telefone);
            //Atualização.
            await _mongodb.AtualizarUmAsync(filter,update);
            //Problemas na atualização.
            var inserido = await _mongodb.BuscarUm(usuarioBody.Cpf);
            if (inserido.Equals(null))
            {
                throw new Exception("Usuário não encontrado.");
            }
            return Ok(inserido.cpfResposta());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
        
    }
}