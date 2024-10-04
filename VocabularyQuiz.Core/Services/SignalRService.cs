using VocabularyQuiz.Core.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace VocabularyQuiz.Core.Services
{
	public class SignalRService : ISignalRService
	{
		private readonly HubConnection _connection;

		public SignalRService()
		{
			_connection = new HubConnectionBuilder()
				.WithUrl("https://localhost:7041/chatHub")
				.Build();
		}		

		public async Task PublishGroupMessageAsync(string groupName, string target, string publishModel)
		{
			try
			{
				// Start the connection if it's not already started
				if (_connection.State == HubConnectionState.Disconnected)
				{
					await _connection.StartAsync();
				}

				// Invoke a method on the SignalR hub
				await _connection.InvokeAsync("SendMessageToGroup", groupName, target, publishModel);
			}
			catch (Exception ex)
			{
				// Handle errors
				Console.WriteLine($"Error sending message: {ex.Message}");
			}
		}

		public async Task PublishMessageAsync(string publishModel)
		{
			try
			{
				// Start the connection if it's not already started
				if (_connection.State == HubConnectionState.Disconnected)
				{
					await _connection.StartAsync();
				}

				// Invoke a method on the SignalR hub
				await _connection.InvokeAsync("SendMessage", publishModel);
			}
			catch (Exception ex)
			{
				// Handle errors
				Console.WriteLine($"Error sending message: {ex.Message}");
			}
		}
	}
}
