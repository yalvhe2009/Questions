import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateQuestionDto, QuestionDto } from '../questions/dto/models';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  apiName = 'Default';
  

  create = (input: CreateUpdateQuestionDto) =>
    this.restService.request<any, QuestionDto>({
      method: 'POST',
      url: '/api/questions',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/questions',
      params: { id },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, QuestionDto>({
      method: 'GET',
      url: `/api/questions/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<QuestionDto>>({
      method: 'GET',
      url: '/api/questions',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateQuestionDto) =>
    this.restService.request<any, QuestionDto>({
      method: 'PUT',
      url: '/api/questions',
      params: { id },
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
