using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Permissions;
using MaXiaoMing.Questions.Subjects.Dto;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MaXiaoMing.Questions.Subjects;

public class SubjectAppService: QuestionsAppService, ISubjectAppService
{
    private readonly IRepository<Subject, Guid> _subjectRepository;

    public SubjectAppService(IRepository<Subject, Guid> subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    
    [Authorize(QuestionsPermissions.Subjects.查询)]
    public async Task<PagedResultDto<SubjectDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _subjectRepository.GetQueryableAsync();

        queryable = queryable
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .OrderBy(input.Sorting ?? nameof(Subject.CreationTime));
        List<Subject> subjects = await AsyncExecuter.ToListAsync(queryable);
        var count = await _subjectRepository.GetCountAsync();

        var subjectDtos = ObjectMapper.Map<List<Subject>, List<SubjectDto>>(subjects);
        return new PagedResultDto<SubjectDto>(count, subjectDtos);
    }

    [Authorize(QuestionsPermissions.Subjects.创建)]
    public async Task CreateAsync(CreateUpdateSubjectDto input)
    {
        var subject = ObjectMapper.Map<CreateUpdateSubjectDto, Subject>(input);
        await _subjectRepository.InsertAsync(subject);
    }

    [Authorize(QuestionsPermissions.Subjects.查询)]
    public async Task<SubjectDto> GetAsync(Guid id)
    {
        var subject = await _subjectRepository.GetAsync(id);
        var subjectDto = ObjectMapper.Map<Subject, SubjectDto>(subject);
        return subjectDto;
    }

    [Authorize(QuestionsPermissions.Subjects.编辑)]
    public async Task UpdateAsync(Guid id, CreateUpdateSubjectDto input)
    {
        var subject = await _subjectRepository.GetAsync(id);
        ObjectMapper.Map(input, subject);
    }

    [Authorize(QuestionsPermissions.Subjects.删除)]
    public async Task DeleteAsync(Guid id)
    {
        await _subjectRepository.DeleteAsync(id);
    }
}