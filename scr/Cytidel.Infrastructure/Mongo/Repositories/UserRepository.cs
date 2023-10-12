using Cytidel.Core.Entities;
using Cytidel.Core.Repositories;
using Cytidel.Infrastructure.Mongo.Documents;
using Omatka.Persistence.MongoDB;

namespace Cytidel.Infrastructure.Mongo.Repositories;
//inject repository
internal sealed class UserRepository(IMongoRepository<UserDocument, Guid> repository) : IUserRepository
{
    //initialisate the instance repository
    private readonly IMongoRepository<UserDocument, Guid> _repository = repository;
    //Create user
    public async Task AddAsync(User user)
        => await _repository.AddAsync(user.AsDocument());
    //Delete user
    public async Task DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);
    //Get user by email
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsync(x => x.Email == email, cancellationToken);
        //Convert object from document to entity and return.
        return user?.AsEntity();
    }

    //Get user by id
    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsync(id, cancellationToken);
        //Convert object from document to entity and return.
        return user?.AsEntity();
    }
    //get user by username

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsync(x => x.Username == username, cancellationToken);
        //Convert object from document to entity and return.
        return user?.AsEntity();
    }
    //update user
    public async Task UpdateAsync(User user)
        => await _repository.UpdateAsync(user.AsDocument());
}
