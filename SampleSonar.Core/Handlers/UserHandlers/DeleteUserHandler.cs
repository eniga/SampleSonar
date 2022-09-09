using SampleSonar.Core.Commands;
using SampleSonar.Core.Interfaces;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Core.Handlers.UserHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Response<User>>
    {
        private readonly IUserRepository repository;

        public DeleteUserHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Response<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Response<User> response = new();
            // Check if user already exists
            var user = await repository.FindByIdAsync(request.Id).ConfigureAwait(false);
            if (user == null)
            {
                response = new()
                {
                    Code = "25",
                    Message = "Could not locate record",
                    Success = false
                };
                return response;
            }
            var result = await repository.DeleteAsync(user);
            response = new()
            {
                Code = result ? "00" : "99",
                Message = result ? "Success" : "Failed",
                Success = result,
                Data = user
            };
            return response;
        }
    }
}
