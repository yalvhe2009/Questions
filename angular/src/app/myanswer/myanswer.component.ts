import { ListResultDto, ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { AnswerDto, GetMyAnswersInput } from '@proxy/answers';
import { AnswerService, SubjectService } from '@proxy/controllers';
import { SubjectDto } from '@proxy/subjects';

@Component({
  selector: 'app-myanswer',
  templateUrl: './myanswer.component.html',
  styleUrls: ['./myanswer.component.scss'],
  providers: [ListService]
})
export class MyanswerComponent implements OnInit {

  subject = {items: []} as ListResultDto<SubjectDto>;
  myAnswer = {items: [], totalCount: 0} as PagedResultDto<AnswerDto>;
  //筛选条件
  getMyAnswerInput = {} as GetMyAnswersInput;

  
  constructor(
    public readonly list: ListService<GetMyAnswersInput>,
    private answerService: AnswerService,
    private subjectService: SubjectService
  ) { }

  ngOnInit(): void {

    this.subjectService.getAllList().subscribe((response)=>{
      this.subject = response;
    });

    const myAnswerStreamCreator = query => this.answerService.getMyAnswers({
      ...query,
      ...this.getMyAnswerInput
    });

    this.list.hookToQuery(myAnswerStreamCreator).subscribe(response => {
        this.myAnswer = response;
    });


  }

  onSearchMyAnswers(){
    this.list.get();
  }
}
