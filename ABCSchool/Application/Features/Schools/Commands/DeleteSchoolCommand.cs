using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands;

public class DeleteSchoolCommand : IRequest<IResponseWrapper>
{
    public int SchoolId { get; set; }
}

public class DeleteCommandHandler : IRequestHandler<DeleteSchoolCommand, IResponseWrapper>
{
    private readonly ISchoolService _schoolService;

    public DeleteCommandHandler(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    public async Task<IResponseWrapper> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
    {
        var school = await _schoolService.GetByIdAsync(request.SchoolId);

        if (school is not null)
        {
            var schoolId = await _schoolService.DeleteAsync(school);

            return await ResponseWrapper<int>.SuccessAsync(data: schoolId, "School deleted successfully.");
        }

        return await ResponseWrapper<int>.FailAsync("School does not exist.");
    }
}