import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { QuestionService, SubjectService } from '@proxy/controllers';
import { QuestionDto } from '@proxy/questions/dto';
import { SubjectDto } from '@proxy/subjects/dto';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss'],
  providers: [ListService]
})
export class QuestionComponent implements OnInit {

  question = {items: [], totalCount: 0} as PagedResultDto<QuestionDto>;
  subject = {items: [], totalCount: 0} as PagedResultDto<SubjectDto>;
  selectedQuestion = {} as QuestionDto;
  selectedSubject = {} as SubjectDto;
  form: FormGroup
  isModalOpen = false;
  isSelectSubjectModalOpen = false;

  constructor(
    public readonly list: ListService,
    private questionService: QuestionService,
    private subjectService: SubjectService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toaster: ToasterService
  ) { }

  ngOnInit(): void {
    const questionStreamCreator = (query) => this.questionService.getList(query);
    this.list.hookToQuery(questionStreamCreator).subscribe((response) => {
      this.question = response;
    })
  }

  createQuestion(){
    this.selectedQuestion = {} as QuestionDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editQuestion(id: string){
    this.questionService.get(id).subscribe((question)=>{
      this.selectedQuestion = question;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  buildForm(){
    if (this.selectedQuestion.subjectId){
      //编辑
      this.subjectService.get(this.selectedQuestion.subjectId).subscribe((dto)=> {
        this.selectedSubject = dto;
      });
    }

    this.form = this.fb.group({
      // subjectId: ['', Validators.required],
      questionDescription: [ this.selectedQuestion.questionDescription || '', Validators.required],
      questionAnswer: [ this.selectedQuestion.questionAnswer || '', Validators.required],
    });

  }

  save(){
    if(this.form.invalid){
      return;
    }
    if(!this.selectedSubject.id){
      this.toaster.warn("请你先选择一个科目！", "提示");
      return;
    }
    this.form.value.subjectId = this.selectedSubject.id;
    const request = this.selectedQuestion.id
      ? this.questionService.update(this.selectedQuestion.id, this.form.value)
      : this.questionService.create(this.form.value);

      request.subscribe(()=>{
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
  }

  delete(id: string){
    this.confirmation.warn('确认要删除吗？', '提示').subscribe((status)=>{
      if(status == Confirmation.Status.confirm){
        this.questionService.delete(id).subscribe(()=>{
          this.list.get();
        });
      }
    });
  }

  openSelectSubjectModal(){
    this.isSelectSubjectModalOpen = true;
    const subjectStreamCreator = (query) => this.subjectService.getList(query);
    this.list.hookToQuery(subjectStreamCreator).subscribe((response) => {
      this.subject = response;
    });
  }

  selectSubject(subject: SubjectDto){
    this.isSelectSubjectModalOpen = false;
    this.selectedSubject = subject;
    this.form.value.subjectId = this.selectedSubject.id;
  }

  handleModalDisappear(){
    this.selectedSubject = {} as SubjectDto;
  }

}
