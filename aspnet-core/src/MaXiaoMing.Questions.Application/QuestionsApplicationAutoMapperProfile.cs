using AutoMapper;
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

        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateUpdateSubjectDto, Subject>();
        CreateMap<Subject, SubjectDto>();
    }
}
