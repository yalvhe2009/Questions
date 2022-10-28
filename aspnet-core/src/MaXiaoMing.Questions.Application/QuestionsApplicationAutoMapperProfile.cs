using AutoMapper;
using MaXiaoMing.Questions.Questions;
using MaXiaoMing.Questions.Questions.Dto;
using MaXiaoMing.Questions.Subjects;
using MaXiaoMing.Questions.Subjects.Dto;

namespace MaXiaoMing.Questions;

public class QuestionsApplicationAutoMapperProfile : Profile
{
    public QuestionsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        //科目
        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateUpdateSubjectDto, Subject>();
        CreateMap<Subject, SubjectDto>();
        
        //问题
        CreateMap<Question, QuestionDto>();
        CreateMap<CreateUpdateQuestionDto, Question>();

    }
}
