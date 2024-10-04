using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyQuiz.Core.Services.Interfaces
{
	public interface ISignalRService
	{
		Task PublishMessageAsync(string publishModel);
		Task PublishGroupMessageAsync(string groupName, string target, string publishModel);
	}
}
