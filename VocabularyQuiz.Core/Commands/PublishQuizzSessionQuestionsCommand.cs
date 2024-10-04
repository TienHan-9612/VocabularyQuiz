using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyQuiz.Core.Dtos;
using VocabularyQuiz.Core.Services.Interfaces;
using VocabularyQuiz.Infrastructure;

namespace VocabularyQuiz.Core.Commands
{
	public class PublishQuizzSessionQuestionsCommand
	{
		public class Command : IRequest<bool>
		{
			public Command(int quizzId)
			{
				QuizzId = quizzId;
			}

			public int QuizzId { get; set; }
		}

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly VocabularyQuizDbContext _dbContext;
			private readonly ISignalRService _signalRService;
			private readonly IMapper _mapper;
			private const string target = "questions";

			public Handler(VocabularyQuizDbContext dbContext, ISignalRService signalRService, IMapper mapper)
			{
				_dbContext = dbContext;
				_signalRService = signalRService;
				_mapper = mapper;
			}

			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var quizzSession = await _dbContext.QuizzSession
					.Include(x => x.QuizzSessionQuestions)
					.ThenInclude(x=> x.QuizzSessionQuestionAnswers)
					.FirstOrDefaultAsync(x => x.Id == request.QuizzId);
				if (quizzSession == null)
				{
					throw new Exception("Not found");
				}
				var quizzSessionDto = _mapper.Map<QuizzSessionDto>(quizzSession);
				await _signalRService.PublishGroupMessageAsync(quizzSession.Id.ToString(), $"{quizzSession.Id}_{target}",JsonConvert.SerializeObject(quizzSessionDto));

				return true;
			}
		}
	}
}
