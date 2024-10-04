namespace VocabularyQuiz.Core.Dtos
{
	public class QuizzSessionQuestionDto
	{
		public int Id { get; set; }
		public string Description { get; set; } = "";
		public int QuizzSessionId { get; set; }
		public ICollection<QuizzSessionQuestionAnswerDto> QuizzSessionQuestionAnswers { get; } = new List<QuizzSessionQuestionAnswerDto>();
	}
}
