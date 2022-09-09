using AutoMapper;
using SampleSonar.Core.Commands;
using SampleSonar.Core.Interfaces;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Core.Handlers.UserHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<User>>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public CreateUserHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Response<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Response<User> response = new();
            IEnumerable<User> user = await repository.FindAllAsync(x => x.Email == request.createUserRequest.Email).ConfigureAwait(false);

            if (user.Any())
            {
                response = new()
                {
                    Code = "63",
                    Message = "Duplicate record",
                    Success = false
                };
                return response;
            }

            var newUser = mapper.Map<User>(request.createUserRequest);
            var result = await repository.InsertAsync(newUser).ConfigureAwait(false);
            response = new()
            {
                Code = result ? "00" : "99",
                Message = result ? "Success" : "Failed",
                Success = result,
                Data = newUser
            };
            return response;
        }
    }
}
