using Ardalis.Result;
using AutoMapper;
using CosmicApi.Application.Features.Users;
using CosmicApi.Domain.Constants;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;
using MediatR;
using BC = BCrypt.Net.BCrypt;
using ValidationError = Ardalis.Result.ValidationError;

namespace CosmicApi.Application.Features.Auth.Signup
{

    public class SignupHandler : IRequestHandler<SignupRequest, Result<UserResponse>>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;


        public SignupHandler(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<UserResponse>> Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<User>(request);
            var validationErrors = new List<ValidationError>();
            
            // check unique username 
            if(_context.Users.Any(x => x.Username== newUser.Username)) {
                validationErrors.Add(new ValidationError { 
                    ErrorMessage = "Username Already Exists!"
                });
            }
            // check unique email
            if (_context.Users.Any(x => x.Email == newUser.Email))
            {
                validationErrors.Add(new ValidationError
                {
                    ErrorMessage = "Email Already Signed!"
                });
            }
            if (validationErrors.Any())
                return Result.Invalid(validationErrors);
            
            newUser.Role = Roles.User;
            _context.Users.Add(newUser);
            newUser.Password = BC.HashPassword(request.Password);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserResponse>(newUser);
        }
    }
}