import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AnswerDto, CreateAnswerDto, GetMyAnswersInput } from '../answers/models';
import type { QuestionDto } from '../questions/dto/models';
import type { SubjectDto } from '../subjects/dto/models';

@Injectable({
  providedIn: 'root',
})
export class AnswerService {
  apiName = 'Default';
  

  getAllSubjectList = () =>
    this.restService.request<any, ListResultDto<SubjectDto>>({
      method: 'GET',
      url: '/api/answers/all-subject',
    },
    { apiName: this.apiName });
  

  getAnalysisByAnswerIds = (answersId: string[]) =>
    this.restService.request<any, ListResultDto<AnswerDto>>({
      method: 'GET',
      url: '/api/answers/analysis-by-ids',
      params: { answersId },
    },
    { apiName: this.apiName });
  

  getMyAnswers = (input: GetMyAnswersInput) =>
    this.restService.request<any, PagedResultDto<AnswerDto>>({
      method: 'GET',
      url: '/api/answers/my-answers',
      params: { subjectId: input.subjectId, filter: input.filter, createTimeFrom: input.createTimeFrom, createTimeTo: input.createTimeTo, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getUnDoQuestion = (subjectId: string, count: number) =>
    this.restService.request<any, ListResultDto<QuestionDto>>({
      method: 'GET',
      url: `/api/answers/un-do-question/${subjectId}`,
      params: { count },
    },
    { apiName: this.apiName });
  

  submitYourAnswer = (input: CreateAnswerDto[]) =>
    this.restService.request<any, ListResultDto<string>>({
      method: 'POST',
      url: '/api/answers',
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
