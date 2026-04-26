using Application.Pipelines;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands;

public class UpdateSchoolCommand : IRequest<IResponseWrapper>, IValidateMe
{
    public UpdateSchoolRequest UpdateSchool { get; set; }
}

public class UpdateSchoolCommandHandler : IRequestHandler<UpdateSchoolCommand, IResponseWrapper>
{
    private readonly ISchoolService _schoolService;

    public UpdateSchoolCommandHandler(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    public async Task<IResponseWrapper> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
    {
        var school = await _schoolService.GetByIdAsync(request.UpdateSchool.Id);

        if (school is not null)
        {
            school.Name = request.UpdateSchool.Name;
            school.EstablishedDate = request.UpdateSchool.EstablishedDate;

            var schoolId = await _schoolService.UpdateAsync(school);

            return await ResponseWrapper<int>.SuccessAsync(data: schoolId, "School updated successfully.");
        }

        return await ResponseWrapper<int>.FailAsync("School does not exist.");
    }
}