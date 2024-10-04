using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VocabularyQuiz.Core.Commands;
using VocabularyQuiz.Core.Dtos;
using VocabularyQuiz.Core.Models;
using VocabularyQuiz.Core.Queries;

namespace VocabularyQuiz.Controllers
{
    [ApiController]
	[Route("quizz")]
	public class QuizzParticipationController : ControllerBase
	{
		protected readonly IMediator _mediator;

		public QuizzParticipationController(IMediator mediator)
		{
			_mediator = mediator;
		}
		
		[HttpPost("{id}/participations")]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> PostQuizzSessionParticipationAsync([FromRoute] int id, [FromBody] JoinQuizzParticipantDto quizzParticipationModel)
		{
			var result = await _mediator.Send(new JoinQuizzByIdCommand.Command(id, quizzParticipationModel));

			return Ok(result);
		}

		[HttpPost("{id}/participations/questions")]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> PostQuizzSessionParticipationQuestionAsync([FromRoute] int id)
		{
			var result = await _mediator.Send(new PublishQuizzSessionQuestionsCommand.Command(id));

			return Ok(result);
		}

		[HttpGet("{id}/leader-board")]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetQuizzSessionLeaderBoardAsync([FromRoute] int id)
		{
			var result = await _mediator.Send(new QuizzSessionLeaderBoardQuery.Query(id));

			return Ok(result);
		}
	}
}
