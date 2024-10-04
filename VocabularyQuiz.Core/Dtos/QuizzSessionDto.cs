namespace VocabularyQuiz.Core.Dtos
{
	public class QuizzSessionDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }

		public List<QuizzSessionQuestionDto> QuizzSessionQuestions { get; } = new List<QuizzSessionQuestionDto>();
	}
}
