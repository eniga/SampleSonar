using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Requests;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Core.Commands
{
    public record CreateUserCommand(CreateUserRequest createUserRequest) : IRequest<Response<User>>;
    public record UpdateUserCommand(int Id, CreateUserRequest createUserRequest) : IRequest<Response<User>>;
    public record DeleteUserCommand(int Id) : IRequest<Response<User>>;
}
