using AutoMapper;
using VocabularyQuiz.Core.Dtos;
using VocabularyQuiz.Infrastructure.Entities;

namespace VocabularyQuiz.AutoMapper
{
	public class QuizzMapping : Profile
	{
		public QuizzMapping()
		{
			CreateMap<QuizzSession, QuizzSessionDto>();
			CreateMap<QuizzSessionQuestion, QuizzSessionQuestionDto>();
			CreateMap<QuizzSessionQuestionAnswer, QuizzSessionQuestionAnswerDto>();
		}			
	}
}
