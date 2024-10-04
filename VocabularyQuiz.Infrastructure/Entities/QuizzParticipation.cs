namespace VocabularyQuiz.Infrastructure.Entities
{
	public class QuizzParticipation
	{
		public int Id { get; set; }
		public int QuizzSessionId  { get; set; }
		public string? Name { get; set; }
		public int? Score { get; set; }

		public virtual QuizzSession? QuizzSession { get; set; }
	}
}
