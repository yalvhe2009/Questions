using System;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Questions;
using MaXiaoMing.Questions.Questions.Dto;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MaXiaoMing.Questions.Controllers;

[RemoteService]
[ControllerName("Question(问题)")]
[Route("api/questions")]
public class QuestionController: AbpController, IQuestionAppService
{
    private readonly IQuestionAppService _questionAppService;

    public QuestionController(IQuestionAppService questionAppService)
    {
        _questionAppService = questionAppService;
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<QuestionDto> GetAsync(Guid id)
    {
        return await _questionAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<QuestionDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return await _questionAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input)
    {
        return await _questionAppService.CreateAsync(input);
    }

    [HttpPut]
    public async Task<QuestionDto> UpdateAsync(Guid id, CreateUpdateQuestionDto input)
    {
        return await _questionAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    public async Task DeleteAsync(Guid id)
    {
        await _questionAppService.DeleteAsync(id);
    }
}