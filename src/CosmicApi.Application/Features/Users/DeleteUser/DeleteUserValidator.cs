using FluentValidation;


namespace CosmicApi.Application.Features.Users.DeleteUser
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }

    }
}