
using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Commands.ArtCommands.CreateArt
{
    public class CreateArtCommandHandler : IRequestHandler<CreateArtCommandRequest, CreateArtCommandResponse>
    {
        private IArtService _service;

        public CreateArtCommandHandler(IArtService service)
        {
            _service = service;
        }

        public async Task<CreateArtCommandResponse> Handle(CreateArtCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateArtAsync(request.Model, request.Model.UserId.ToString());
            return new()
            {
                Result = result
            };
        }
    }
}
