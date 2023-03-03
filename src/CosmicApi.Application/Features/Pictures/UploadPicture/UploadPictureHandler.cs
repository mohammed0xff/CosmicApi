using MediatR;
using AutoMapper;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Infrastructure.Services;

namespace CosmicApi.Application.Features.Pictures.UploadPicture
{
    public class UploadPictureeHandler : IRequestHandler<UploadPictureRequest, PictureResponse?>
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
        public async Task<PictureResponse?> Handle(UploadPictureRequest request, CancellationToken cancellationToken)
        {
            var uploadedImage = _pictureService.UploadPicture(request.FormFile);
            if (uploadedImage == null) 
                return null;
            
            uploadedImage.LuminaryId = request.LuminaryId;
            await _context.Pictures.AddAsync(uploadedImage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
                
            return _mapper.Map<PictureResponse>(uploadedImage);
        }
    }
}
