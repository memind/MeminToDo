
using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Commands.ArtCommands.UpdateArt
{
    public class UpdateArtCommandHandler : IRequestHandler<UpdateArtCommandRequest, UpdateArtCommandResponse>
    {
        private IArtService _service;

        public UpdateArtCommandHandler(IArtService service)
        {
            _service = service;
        }

        public async Task<UpdateArtCommandResponse> Handle(UpdateArtCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateArtAsync(request.Id ,request.Model);
            return new()
            {
                Result = result
            };
        }
    }
}
