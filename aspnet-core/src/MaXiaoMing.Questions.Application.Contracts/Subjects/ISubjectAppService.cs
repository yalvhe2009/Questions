using System;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Subjects.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MaXiaoMing.Questions.Subjects;

public interface ISubjectAppService: IApplicationService
{
    Task<PagedResultDto<SubjectDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    Task CreateAsync(CreateUpdateSubjectDto input);

    Task<SubjectDto> GetAsync(Guid id);
    Task UpdateAsync(Guid id, CreateUpdateSubjectDto input);

    Task DeleteAsync(Guid id);
}