import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateSubjectDto {
  name: string;
}

export interface SubjectDto extends FullAuditedEntityDto<string> {
  name?: string;
}
