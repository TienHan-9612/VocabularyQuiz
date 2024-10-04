namespace VocabularyQuiz.Infrastructure.Entities
{
	public class QuizzSessionQuestion
	{
        public int Id { get; set; }
		public string Description { get; set; } = "";
		public int QuizzSessionId { get; set; }
		public virtual QuizzSession? QuizzSession { get; set; }
		public ICollection<QuizzSessionQuestionAnswer> QuizzSessionQuestionAnswers { get; } = new List<QuizzSessionQuestionAnswer>();
	}
}
