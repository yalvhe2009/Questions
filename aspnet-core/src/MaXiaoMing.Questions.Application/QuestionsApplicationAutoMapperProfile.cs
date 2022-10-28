using AutoMapper;
using MaXiaoMing.Questions.Answers;
using MaXiaoMing.Questions.Questions;
using MaXiaoMing.Questions.Subjects;

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
        // CreateMap<Subject, SubjectDto>();
        
        //问题
        CreateMap<Question, QuestionDto>();
        CreateMap<CreateUpdateQuestionDto, Question>();
        
        //回答
        // CreateMap<CreateAnswerDto, Answer>();
    }
}
