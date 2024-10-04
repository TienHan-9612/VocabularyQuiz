using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VocabularyQuiz.Middlewares;

public static class SlackNotifierExceptionMiddleware
{
	public static void ConfigureExceptionMiddlewareHandler(this IApplicationBuilder app)
	{
		app.UseExceptionHandler(delegate (IApplicationBuilder appError)
		{
			appError.Run(async delegate (HttpContext context)
			{
				context.Response.StatusCode = 500;
				context.Response.ContentType = "application/json";
				IExceptionHandlerFeature exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
				if (exceptionHandlerFeature != null)
				{
					Exception error = exceptionHandlerFeature.Error;
					string text3 = error.Message;					

					JsonSerializerSettings settings = new JsonSerializerSettings
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver()
					};
					string text4 = JsonConvert.SerializeObject(new 
					{
						Message = error.Message						
					}, settings);
					

					await context.Response.WriteAsync(text4);
				}
			});
		});		
	}
}
