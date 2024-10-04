using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyQuiz.Infrastructure.Entities
{
	public class QuizzSession
	{
		public int Id { get; set; }
		public string? Title { get; set; }

		public ICollection<QuizzSessionQuestion> QuizzSessionQuestions { get; } = new List<QuizzSessionQuestion>();
		public ICollection<QuizzParticipation> QuizzParticipations { get; } = new List<QuizzParticipation>();
	}
}
