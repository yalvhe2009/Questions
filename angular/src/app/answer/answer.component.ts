import { ListResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { AnswerDto, CreateAnswerDto } from '@proxy/answers';
import { AnswerService, SubjectService } from '@proxy/controllers';
import { QuestionDto } from '@proxy/questions';
import { SubjectDto } from '@proxy/subjects';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.scss']
})
export class AnswerComponent implements OnInit {

  subject = {items: []} as ListResultDto<SubjectDto>;
  unDoQuestions = {items: []} as ListResultDto<QuestionDto>;
  analysis = {items: []} as ListResultDto<AnswerDto>;
  yourAnswers: CreateAnswerDto[] = []
  selectedSubjectId = "";
  currentDoingIndex = 1;
  isShowStartBtnLayer = true
  isShowAnswerLayer = false;
  isShowAnalysisLayer = false;
  isStartAnsweringBtnDisable = false;

  constructor(
    private answerService: AnswerService,
    private subjectService: SubjectService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.subjectService.getAllList().subscribe((response)=>{
      this.subject = response;
    })
  }

  //开始答题
  startAnswering(){
    if(this.selectedSubjectId === ""){
      this.toaster.warn("请您选择一个科目, 然后才可以开始答题!", "提示");
      return;
    }
      //请求题目列表
      this.isStartAnsweringBtnDisable = true;
      this.answerService.getUnDoQuestion(this.selectedSubjectId, 3)//TODO 每次请求题目数允许用户输入
      .subscribe((response)=>{
        this.isStartAnsweringBtnDisable =  false;
        if(response.items.length === 0){
          this.toaster.warn("您选择的科目下没有未答题目!");
          return;
        }
        this.unDoQuestions = response;
        this.isShowStartBtnLayer = false;
        this.isShowAnswerLayer = true;
        this.yourAnswers = response.items.map((val)=> { 
          let dto = { questionId: val.id, yourAnswer: ''  } as CreateAnswerDto;
          return dto;
        });

      });
  }

  onSelectSubject(val: string){
    this.selectedSubjectId = val;
  }

  //点击返回
  onClickBack(){
    this.confirmation.warn('您确认要返回吗? 您未交卷, 返回后所做题目将不会保存!', '提示').subscribe((status)=>{
      if (status == Confirmation.Status.confirm) {
        this.isShowStartBtnLayer = true;
        this.isShowAnswerLayer = false;
        this.currentDoingIndex = 1;
        this.selectedSubjectId = "";
      }
    });
  }

  //在分析页面点击返回
  onAnalysisClickBack(){
      this.isShowStartBtnLayer = true;
      this.isShowAnswerLayer = false;
      this.isShowAnalysisLayer = false;
      this.currentDoingIndex = 1;
      this.selectedSubjectId = "";
  }

  //交卷
  onClickSubmit(){
    this.answerService.submitYourAnswer(this.yourAnswers).subscribe((ids) => {
      console.log(ids)
      this.answerService.getAnalysisByAnswerIds(ids.items).subscribe((answers) => {
        console.log(answers);
        //跳转到解析的界面
        this.isShowAnswerLayer = false;
        this.isShowAnalysisLayer = true;
        this.analysis = answers;
        this.currentDoingIndex = 1;//从第一题开始查看解析
      });
    });
  }

  //下一题
  onClickNext(){
    this.currentDoingIndex += 1;
  }

  //上一题
  onClickPre(){
    this.currentDoingIndex -= 1;
  }

  //当您的回答文本框内容变化时的回调
  onYourAnswerChange(val: string){
    console.log(this.yourAnswers)
    this.yourAnswers[this.currentDoingIndex - 1].yourAnswer = val;
  }
}
