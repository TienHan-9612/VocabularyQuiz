namespace VocabularyQuiz.Core.Dtos
{
	public class QuizzSessionQuestionAnswerDto
	{
		public int Id { get; set; }
		public int QuizzQuestionId { get; set; }
		public string Description { get; set; } = "";
		public bool IsCorrect { get; set; }
	}
}
