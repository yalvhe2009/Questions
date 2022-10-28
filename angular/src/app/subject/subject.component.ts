import {  ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubjectService } from '@proxy/controllers';
import { SubjectDto } from '@proxy/subjects/dto';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.scss'],
  providers: [ListService]
})
export class SubjectComponent implements OnInit {

  subject = { items: [], totalCount: 0 } as PagedResultDto<SubjectDto>;

  selectedSubject = {} as SubjectDto;

  isModalOpen = false;

  form: FormGroup

  constructor(
    public readonly list: ListService, 
    private subjectService: SubjectService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
    ) { }

  ngOnInit(): void {
    const subjectStreamCreator = (query) => this.subjectService.getList(query);

    this.list.hookToQuery(subjectStreamCreator).subscribe((response)=> {
      this.subject = response;
    })
  }

  createSubject(){
    this.selectedSubject = {} as SubjectDto;
    this.buildForm()
    this.isModalOpen = true;
  }

  save(){
    if(this.form.invalid){
      return;
    }
    console.log(this.selectedSubject)
    const request = this.selectedSubject.id
      ? this.subjectService.update(this.selectedSubject.id, this.form.value)
      : this.subjectService.create(this.form.value);

    request.subscribe(()=>{
      this.isModalOpen = false;
      this.form.reset();
      this.list.get()
    })
  }

  buildForm(){
    this.form = this.fb.group({
      name: [this.selectedSubject.name || '', Validators.required]
    });
  }

  editSubject(id: string){
    this.subjectService.get(id).subscribe((subject)=> {
      this.selectedSubject = subject;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  delete(id: string){
    this.confirmation.warn('您确认要删除吗？', '提示').subscribe((status)=>{
      if(status == Confirmation.Status.confirm){
        this.subjectService.delete(id).subscribe(()=>{
          this.list.get();
        });
      }
    });
  }
}
