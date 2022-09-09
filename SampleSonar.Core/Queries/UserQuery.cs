using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Core.Queries
{
    public record GetUsersQuery() : IRequest<Response<IEnumerable<User>>>;
    public record GetUserByIdQuery(int Id) : IRequest<Response<User>>;
}
