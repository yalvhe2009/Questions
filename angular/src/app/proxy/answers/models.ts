import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface AnswerDto extends FullAuditedEntityDto<string> {
  subjectId?: string;
  subjectName?: string;
  questionDescription?: string;
  questionAnswer?: string;
  questionId?: string;
  yourAnswer?: string;
}

export interface CreateAnswerDto {
  questionId?: string;
  yourAnswer?: string;
}

export interface GetMyAnswersInput extends PagedAndSortedResultRequestDto {
  subjectId?: string;
  filter?: string;
  createTimeFrom?: string;
  createTimeTo?: string;
}
