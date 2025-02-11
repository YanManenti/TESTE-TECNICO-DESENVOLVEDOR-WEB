using Back.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Back.Services;

public class MongoDB
{
    private readonly IMongoCollection<Usuario> _usuarios;

    public MongoDB(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);

        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

        _usuarios = database.GetCollection<Usuario>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CriarUmAsync(Usuario usuario)
    {
        await _usuarios.InsertOneAsync(usuario);
    }

    public async Task AtualizarUmAsync(FilterDefinition<Usuario> filter, UpdateDefinition<Usuario> update)
    {
        await _usuarios.UpdateOneAsync(filter, update);
    }
    
    public async Task RemoverUmAsync(string cpf) 
    {
       await _usuarios.DeleteOneAsync(usuario => usuario.Cpf == cpf);
    }

    public async Task<Usuario> BuscarUm(string cpf)
    {
        return await _usuarios.Find(user => user.Cpf == cpf).FirstOrDefaultAsync();
    }

    public async Task<List<Usuario>> BuscarTodos()
    {
        return await _usuarios.Find(user => true).ToListAsync();
    }
}