import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubjectService } from '@proxy/controllers';
import { SubjectDto } from '@proxy/subjects';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.scss'],
  providers: [ListService]//Added---
})
export class SubjectComponent implements OnInit {

  subject = { items: [], totalCount: 0 } as PagedResultDto<SubjectDto>;
  selectedSubject = {} as SubjectDto;
  form: FormGroup
  isModalOpen = false;

  constructor(
    public readonly list: ListService, 
    private subjectService: SubjectService, 
    private fb: FormBuilder,
    private confirmation: ConfirmationService
    ) { }

  ngOnInit(): void {
    const subjectStreamCreator = (query) => this.subjectService.getList(query);
    
    this.list.hookToQuery(subjectStreamCreator).subscribe((response) => {
      this.subject = response;
    });
  }

  createSubject(){
    this.selectedSubject = {} as SubjectDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editSubject(id: string){
    this.subjectService.get(id).subscribe((subject) => {
      this.selectedSubject = subject;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm(){
    this.form = this.fb.group({
      name: [this.selectedSubject.name || '这是默认值', Validators.required]
    });
  }
  save(){
    if(this.form.invalid){
      return;
    }

    const request = this.selectedSubject.id 
    ? this.subjectService.update(this.selectedSubject.id, this.form.value) 
    : this.subjectService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  delete(id: string){
    this.confirmation.warn('确认要删除吗?', '提示').subscribe((status)=> {
      if (status === Confirmation.Status.confirm){
        this.subjectService.delete(id).subscribe(() => this.list.get());
      }
    })
  }
}
