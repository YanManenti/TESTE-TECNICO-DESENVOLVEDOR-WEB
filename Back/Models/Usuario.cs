using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Back.Models;

[BsonIgnoreExtraElements]
public class Usuario
{
    public string? Codigo { get; set; }
    [BsonRequired]
    public string Nome { get; set; }
    [BsonRequired]
    public string Cpf { get; set; }
    public string? Endereco { get; set; }
    public string? Telefone { get; set; }

    public Usuario(string nome, string cpf, string? endereco = "", string? telefone = "", string? codigo = "")
    {
        this.Codigo = codigo;
        this.Nome = nome;
        this.Cpf = cpf;
        this.Endereco = endereco;
        this.Telefone = telefone;
    }

    public string cpfResposta()
    {
        String cpf = String.Concat(this.Cpf.Substring(0,3),this.Cpf.Substring(4,1));
        return $"Pessoa cadastrada com sucesso, cpf {cpf}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        var usuario = obj as Usuario;
        if (usuario == null) return false;
        if(!String.Equals(Codigo, usuario.Codigo)) return false;
        if(!String.Equals(Nome, usuario.Nome)) return false;
        if(!String.Equals(Cpf, usuario.Cpf)) return false;
        if(!String.Equals(Endereco, usuario.Endereco)) return false;
        if(!String.Equals(Telefone, usuario.Telefone)) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}