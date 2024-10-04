using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyQuiz.Infrastructure.Entities
{
	public class QuizzSessionQuestionAnswer
	{
		public int Id { get; set; }
		public int QuizzQuestionId { get; set; }
		public string Description { get; set; } = "";
		public bool IsCorrect { get; set; }

		public virtual QuizzSessionQuestion? QuizzSessionQuestion { get; set; } 
	}
}
