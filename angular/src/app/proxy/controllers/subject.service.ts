import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateSubjectDto, SubjectDto } from '../subjects/models';

@Injectable({
  providedIn: 'root',
})
export class SubjectService {
  apiName = 'Default';
  

  create = (input: CreateUpdateSubjectDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/subjects',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/subjects',
      params: { id },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, SubjectDto>({
      method: 'GET',
      url: `/api/subjects/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<SubjectDto>>({
      method: 'GET',
      url: '/api/subjects',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateSubjectDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: '/api/subjects',
      params: { id },
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
