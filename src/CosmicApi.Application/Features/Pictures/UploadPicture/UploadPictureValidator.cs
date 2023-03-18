using CosmicApi.Application.Features.Pictures.UploadPicture;
using FluentValidation;

namespace CosmicApi.Application.Features.Auth.Authenticate;

public class UploadPictureValidator : AbstractValidator<UploadPictureRequest>
{

    public UploadPictureValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.FormFile);
    }
}