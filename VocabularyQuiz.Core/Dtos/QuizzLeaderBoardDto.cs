using VocabularyQuiz.Core.Models;

namespace VocabularyQuiz.Core.Dtos
{
	public class QuizzLeaderBoardDto
	{
        public IEnumerable<QuizzParticipantDto> Participants { get; set; } = new List<QuizzParticipantDto>();
    }
}
