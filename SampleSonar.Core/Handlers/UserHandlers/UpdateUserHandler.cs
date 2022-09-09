using AutoMapper;
using Newtonsoft.Json.Linq;
using SampleSonar.Core.Commands;
using SampleSonar.Core.Interfaces;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Core.Handlers.UserHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response<User>>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UpdateUserHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Response<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Response<User> response = new();
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

            var updatedUser = mapper.Map<User>(request.createUserRequest);
            updatedUser.Id = request.Id;
            updatedUser.CreatedAt = user.CreatedAt;

            var result = await repository.UpdateAsync(updatedUser).ConfigureAwait(false);
            response = new()
            {
                Code = result ? "00" : "99",
                Message = result ? "Success" : "Failed",
                Success = result,
                Data = updatedUser
            };
            return response;
        }
    }
}
