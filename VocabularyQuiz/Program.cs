using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VocabularyQuiz.Core.Commands;
using VocabularyQuiz.Core.Services;
using VocabularyQuiz.Core.Services.Interfaces;
using VocabularyQuiz.Infrastructure;
using VocabularyQuiz.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
 .AddJsonOptions(options =>
 {
	 options.JsonSerializerOptions.PropertyNamingPolicy = null;
 });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<VocabularyQuizDbContext>(
	options => options.UseSqlServer("Data Source=HANMANG;Initial Catalog=VocabularyQuizz;Integrated Security=True;Encrypt=False"));
builder.Services.AddHttpClient();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(JoinQuizzByIdCommand))));
builder.Services.AddScoped<ISignalRService, SignalRService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllCorsPolicy", builder =>
	{
		builder.WithOrigins("https://localhost:7041") // Replace with your actual origin
			   .AllowAnyHeader()
			   .AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowAllCorsPolicy");
app.ConfigureExceptionMiddlewareHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
