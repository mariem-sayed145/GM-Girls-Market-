using GM.Application.Features.Documents.DTOs;
using MediatR;

namespace GM.Application.Features.Documents.Queries.GetMyDocuments;

public record GetMyDocumentsQuery(int UserId) : IRequest<IReadOnlyList<DocumentDto>>;
