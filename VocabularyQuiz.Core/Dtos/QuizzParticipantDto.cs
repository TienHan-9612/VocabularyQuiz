using System.Text.Json.Serialization;

namespace VocabularyQuiz.Core.Models
{
	public class QuizzParticipantDto
	{
        public string? Name { get; set; }
		public int? Score { get; set; }
	}
}
