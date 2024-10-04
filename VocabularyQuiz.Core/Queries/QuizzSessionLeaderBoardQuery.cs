using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyQuiz.Core.Dtos;
using VocabularyQuiz.Core.Models;
using VocabularyQuiz.Infrastructure;

namespace VocabularyQuiz.Core.Queries
{
	public class QuizzSessionLeaderBoardQuery
	{
		public class Query : IRequest<QuizzLeaderBoardDto>
		{
			public int QuizzSessionId { get; set; }

			public Query(int quizzSessionId)
			{
				QuizzSessionId = quizzSessionId;
			}
		}
		public class Handler : IRequestHandler<Query, QuizzLeaderBoardDto>
		{
			private readonly VocabularyQuizDbContext _dbContext;

			public Handler(VocabularyQuizDbContext dbContext) => _dbContext = dbContext;

			public async Task<QuizzLeaderBoardDto> Handle(Query request, CancellationToken cancellationToken)
			{
				var leaderBoard = await _dbContext.QuizzParticipation.Where(x=> x.QuizzSessionId == request.QuizzSessionId).ToListAsync();

				return new QuizzLeaderBoardDto(){
					Participants = leaderBoard.Select(x => new QuizzParticipantDto() { Name = x.Name, Score = x.Score }).ToList()
				};
			}
		}
	}
}
