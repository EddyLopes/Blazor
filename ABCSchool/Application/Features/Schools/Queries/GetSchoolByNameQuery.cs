using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Schools.Queries;

public class GetSchoolByNameQuery : IRequest<IResponseWrapper>
{
    public string SchoolName { get; set; }
}

public class GetSchoolByNameQueryHandler : IRequestHandler<GetSchoolByNameQuery, IResponseWrapper>
{
    private readonly ISchoolService _schoolService;

    protected GetSchoolByNameQueryHandler(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    public async Task<IResponseWrapper> Handle(GetSchoolByNameQuery request, CancellationToken cancellationToken)
    {
        var school = await _schoolService.GetByNameAsync(request.SchoolName);

        if (school is not null)
        {
            return await ResponseWrapper<SchoolResponse>.SuccessAsync(data: school.Adapt<SchoolResponse>(), "School retrieved successfully.");
        }

        return await ResponseWrapper<SchoolResponse>.FailAsync("School does not exist.");
    }
}
