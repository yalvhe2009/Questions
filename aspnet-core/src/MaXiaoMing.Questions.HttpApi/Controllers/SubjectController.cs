using System;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Subjects;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MaXiaoMing.Questions.Controllers;

[RemoteService]
[ControllerName("Subject（科目）")]
[Route("api/subjects")]

public class SubjectController: AbpControllerBase, ISubjectAppService
{
    private readonly ISubjectAppService _subjectAppService;

    public SubjectController(ISubjectAppService subjectAppService)
    {
        _subjectAppService = subjectAppService;
    }
    
    [HttpGet]
    public async Task<PagedResultDto<SubjectDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return await _subjectAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task CreateAsync(CreateUpdateSubjectDto input)
    {
        await _subjectAppService.CreateAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<SubjectDto> GetAsync(Guid id)
    {
        return await _subjectAppService.GetAsync(id);
    }

    [HttpPut]
    public async Task UpdateAsync(Guid id, CreateUpdateSubjectDto input)
    {
        await _subjectAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    public async Task DeleteAsync(Guid id)
    {
        await _subjectAppService.DeleteAsync(id);
    }
}