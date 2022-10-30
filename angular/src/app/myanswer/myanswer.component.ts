import { ListResultDto, ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { AnswerDto, GetMyAnswersInput } from '@proxy/answers';
import { AnswerService } from '@proxy/controllers/answer.service';
import { SubjectDto } from '@proxy/subjects/dto';

@Component({
  selector: 'app-myanswer',
  templateUrl: './myanswer.component.html',
  styleUrls: ['./myanswer.component.scss'],
  providers: [ListService]
})
export class MyanswerComponent implements OnInit {

  subject = { items: []} as ListResultDto<SubjectDto>;
  myAnswer = { items: [], totalCount: 0} as PagedResultDto<AnswerDto>;
  //筛选条件
  getMyAnswerInput = {} as GetMyAnswersInput;

  constructor(
    public readonly list: ListService<GetMyAnswersInput>,
    private answerService: AnswerService
  ) { }

  ngOnInit(): void {
    //获取科目
    this.answerService.getAllSubjectList().subscribe((response)=>{
      this.subject = response;
    });
    //获取我的已答
    const myAnswerStreamCreator = query => this.answerService.getMyAnswers({
      ...query,
      ...this.getMyAnswerInput
    });

    this.list.hookToQuery(myAnswerStreamCreator).subscribe((response) =>{
      this.myAnswer = response;
    });
  }

  onSearchMyAnswers(){
    this.list.get();
  }

}
