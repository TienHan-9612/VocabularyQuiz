using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VocabularyQuiz.Core.Dtos;
using VocabularyQuiz.Core.Models;
using VocabularyQuiz.Core.Services.Interfaces;
using VocabularyQuiz.Infrastructure;

namespace VocabularyQuiz.Core.Commands
{
	public class JoinQuizzByIdCommand
	{
		public class Command : IRequest<bool>
		{
			public Command(int quizzId, JoinQuizzParticipantDto quizzParticipation)
			{
				QuizzId = quizzId;
				QuizzParticipation = quizzParticipation;
			}
			public JoinQuizzParticipantDto QuizzParticipation { get; set; }
			public int QuizzId { get; set; }
		}

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly VocabularyQuizDbContext _dbContext;
			private readonly ISignalRService _signalRService;
			private readonly IMapper _mapper;
			private const string target = "participantion";

			public Handler(VocabularyQuizDbContext dbContext, ISignalRService signalRService, IMapper mapper)
			{
				_dbContext = dbContext;
				_signalRService = signalRService;
				_mapper = mapper;
			}

			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{				
				var quizzSession = await _dbContext.QuizzSession.Include(x=> x.QuizzSessionQuestions)
					.FirstOrDefaultAsync(x => x.Id == request.QuizzId);
				if (quizzSession == null)
				{
					throw new Exception("Not found");
				}
				try
				{
					var quizzParticipation = new Infrastructure.Entities.QuizzParticipation()
					{
						Name = request.QuizzParticipation.Name,
						QuizzSessionId = quizzSession.Id,
						Score = 0
					};
					await _dbContext.QuizzParticipation.AddAsync(quizzParticipation);
					await _dbContext.SaveChangesAsync(cancellationToken);

				}
				catch (Exception ex) { var test = ex; }

				await _signalRService.PublishGroupMessageAsync(quizzSession.Id.ToString(), $"{quizzSession.Id}_{target}", JsonConvert.SerializeObject(request.QuizzParticipation));

				return true;
			}
		}
	}
}
