using MediatR;
using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Infrastructure.Context;

namespace CosmicApi.Application.Features.Pictures.GetPictures
{
    public class GetPictureeHandler : IRequestHandler<GetLuminaryPicturesRequest, PaginatedList<PictureResponse>>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetPictureeHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<PictureResponse>> Handle(GetLuminaryPicturesRequest request, CancellationToken cancellationToken)
        {
            var pictures = _context.Pictures
                .Where(x => x.LuminaryId == request.LuminaryId);

            if (request.New)
                pictures.OrderBy(x => x.AddedAt);

            return await _mapper.ProjectTo<PictureResponse>(pictures)
                .ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        }
    }
}
