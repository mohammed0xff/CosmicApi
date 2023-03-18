using MediatR;
using AutoMapper;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Infrastructure.Services;
using Ardalis.Result;

namespace CosmicApi.Application.Features.Pictures.UploadPicture
{
    public class UploadPictureeHandler : IRequestHandler<UploadPictureRequest, Result<PictureResponse>>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;
        private readonly IPictureService _pictureService;

        public UploadPictureeHandler(IContext context, IMapper mapper, IPictureService pictureService)
        {
            _context = context;
            _mapper = mapper;
            _pictureService = pictureService;
        }
        public async Task<Result<PictureResponse>> Handle(UploadPictureRequest request, CancellationToken cancellationToken)
        {
            var uploadedImage = _pictureService.UploadPicture(request.FormFile);
            
            if(uploadedImage.IsSuccess)
            {
                var image = uploadedImage.Value;
                image.LuminaryId = request.LuminaryId;
                await _context.Pictures.AddAsync(image, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success( 
                    _mapper.Map<PictureResponse>(image)
                    );
            }

            return Result.Error(
                uploadedImage.Errors.FirstOrDefault()
                );
        }
    }
}
