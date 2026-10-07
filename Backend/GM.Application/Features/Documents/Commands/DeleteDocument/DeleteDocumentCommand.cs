using MediatR;

namespace GM.Application.Features.Documents.Commands.DeleteDocument;

public record DeleteDocumentCommand(int Id, int UserId) : IRequest;
