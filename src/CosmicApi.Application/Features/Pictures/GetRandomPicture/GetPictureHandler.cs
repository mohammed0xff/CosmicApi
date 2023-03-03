using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CosmicApi.Infrastructure.Context;

namespace CosmicApi.Application.Features.Pictures.GetPictures
{
    public class GetRandomPictureHandler : IRequestHandler<GetRandomPicturesRequest, PictureResponse>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetRandomPictureHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PictureResponse> Handle(GetRandomPicturesRequest request, CancellationToken cancellationToken)
        {
            var picturesCount = await _context.Pictures.CountAsync();
            if (picturesCount > 0)
                throw new ApplicationException("Database is Empty");
            int randomNumber = Random.Shared.Next(0, picturesCount-1);
            var randomPicture = await _context.Pictures
                .Skip(randomNumber)
                .FirstOrDefaultAsync();

            return _mapper.Map<PictureResponse>(randomPicture);

        }
    }
}
