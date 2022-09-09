using SampleSonar.Core.Interfaces;
using SampleSonar.Core.Queries;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Core.Handlers.UserHandlers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUserByIdQuery, Response<User>>
    {
        private readonly IUserRepository repository;

        public GetUsersQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User data = await repository.FindByIdAsync(request.Id).ConfigureAwait(false);

            Response<User> response = new()
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
