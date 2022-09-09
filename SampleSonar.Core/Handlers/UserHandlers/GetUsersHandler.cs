using SampleSonar.Core.Interfaces;
using SampleSonar.Core.Queries;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Core.Handlers.UserHandlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, Response<IEnumerable<User>>>
    {
        private readonly IUserRepository repository;

        public GetUsersHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Response<IEnumerable<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> data = await repository.FindAllAsync().ConfigureAwait(false);

            Response<IEnumerable<User>> response = new()
            {
                Code = "00",
                Message = "Success",
                Success = true,
                Data = data
            };

            return response;
        }
    }
}
