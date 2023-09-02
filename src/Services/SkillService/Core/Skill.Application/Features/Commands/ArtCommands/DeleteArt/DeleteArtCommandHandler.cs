using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Commands.ArtCommands.DeleteArt
{
    public class DeleteArtCommandHandler : IRequestHandler<DeleteArtCommandRequest, DeleteArtCommandResponse>
    {
        private IArtService _service;

        public DeleteArtCommandHandler(IArtService service)
        {
            _service = service;
        }

        public async Task<DeleteArtCommandResponse> Handle(DeleteArtCommandRequest request, CancellationToken cancellationToken)
        {
            await _service.DeleteArtAsync(request.Id);
            return new();
        }
    }
}
