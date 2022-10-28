import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateQuestionDto {
  subjectId?: string;
  questionDescription: string;
  questionAnswer: string;
}

export interface QuestionDto extends FullAuditedEntityDto<string> {
  subjectId?: string;
  questionDescription?: string;
  questionAnswer?: string;
}
