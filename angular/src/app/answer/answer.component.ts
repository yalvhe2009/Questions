import { ListResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { AnswerDto, CreateAnswerDto } from '@proxy/answers';
import { AnswerService } from '@proxy/controllers/answer.service';
import { QuestionDto } from '@proxy/questions/dto';
import { SubjectDto } from '@proxy/subjects/dto';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.scss']
})
export class AnswerComponent implements OnInit {

  subject = { items: []} as ListResultDto<SubjectDto>;
  unDoQuestions = { items: []} as ListResultDto<QuestionDto>;
  yourAnswers: CreateAnswerDto[] = []
  analysis = {items: []} as ListResultDto<AnswerDto>;
  selectedSubjectId = "";
  currentDoingIndex = 1;
  isShowStartBtnLayer = true;
  isShowAnswerLayer = false;
  isShowAnalysisLayer = false;
  isStartAnsweringBtnDisable = false;

  constructor(
    private anserService: AnswerService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.anserService.getAllSubjectList().subscribe((response) => {
      this.subject = response;
    });
  }

  onSelectSubject(val: string){
    this.selectedSubjectId = val;
  }

  //开始答题
  startAnswering(){
    if (this.selectedSubjectId === "") {
      this.toaster.warn("请您选择一个科目, 然后才可以开始答题", "提示");
      return;
    }
    //请求未答列表
    this.isStartAnsweringBtnDisable = true;
    this.anserService.getUnDoQuestion(this.selectedSubjectId, 3)//TODO 
    .subscribe((response) => {
      this.isStartAnsweringBtnDisable = false;
      if(response.items.length === 0){
        this.toaster.warn("您选择的科目下没有未答题目!");
        return;
      }
      this.unDoQuestions = response;
      console.log(response)
      this.isShowStartBtnLayer = false;
      this.isShowAnswerLayer = true;
      this.yourAnswers = response.items.map((val) => {
        let dto = { questionId: val.id, yourAnswer: ''} as CreateAnswerDto;
        return dto;
      });
    })
  }

  onClickBack(){
    this.confirmation.warn("您确认要返回吗? 您未交卷,返回后所做题目将不会保存!", "提示").subscribe((status) => {
      if(status == Confirmation.Status.confirm){
        this.isShowStartBtnLayer = true;
        this.isShowAnswerLayer = false;
        this.currentDoingIndex = 1;
        this.selectedSubjectId = "";
      }
    })
  }
  onClickAnalysisBack(){
    this.isShowStartBtnLayer = true;
    this.isShowAnswerLayer = false;
    this.currentDoingIndex = 1;
    this.selectedSubjectId = "";
    this.isShowAnalysisLayer = false;
  }

  onClickPre(){
    this.currentDoingIndex -= 1;
  }

  onClickNext(){
    this.currentDoingIndex += 1;
  }

  onClickSubmit(){
    this.anserService.submitYourAnswer(this.yourAnswers).subscribe((ids) => {
      this.anserService.getAnalysisByAnswerIds(ids.items).subscribe((answers) => {
        this.isShowAnalysisLayer = true;
        this.isShowAnswerLayer = false;
        this.analysis = answers;
        this.currentDoingIndex = 1;
      });
    });
  }

  onYourAnswerChange(val: string){
    this.yourAnswers[this.currentDoingIndex - 1].yourAnswer = val;
  }
}
