using Microsoft.EntityFrameworkCore;
using VocabularyQuiz.Infrastructure.Entities;

namespace VocabularyQuiz.Infrastructure
{

	public class VocabularyQuizDbContext : DbContext
	{

		public VocabularyQuizDbContext()
		{
		}
		public VocabularyQuizDbContext(DbContextOptions<VocabularyQuizDbContext> options) : base(options)
		{
		}

		public DbSet<QuizzSession> QuizzSession { get; set; }
		public DbSet<QuizzSessionQuestion> QuizzSessionQuestion { get; set; }
		public DbSet<QuizzSessionQuestionAnswer> QuizzSessionQuestionAnswer { get; set; }
		public DbSet<QuizzParticipation> QuizzParticipation { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{			
			// Configure your entities and relationships here
			modelBuilder.Entity<QuizzSession>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("quizz_session_pkey");

				entity.ToTable("QuizzSession");

				entity.Property(e => e.Id)
					.ValueGeneratedOnAdd()
					.HasColumnName("id");
				entity.Property(e => e.Title)
					.HasColumnType("character varying")
					.HasMaxLength(250)
					.HasColumnName("title");				
			});
			modelBuilder.Entity<QuizzSessionQuestion>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("quizz_session_question_pkey");

				entity.ToTable("QuizzSessionQuestion");

				entity.Property(e => e.Id)
					.ValueGeneratedOnAdd()
					.HasColumnName("id");
				entity.Property(e => e.Description)
					.HasColumnType("character varying")
					.HasMaxLength(250)
					.HasColumnName("description");
				entity.HasOne(d => d.QuizzSession).WithMany(p => p.QuizzSessionQuestions)
				.HasForeignKey(d => d.QuizzSessionId)
				.HasConstraintName("quizz_session_question_fk");
			});

			modelBuilder.Entity<QuizzSessionQuestionAnswer>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("quizz_session_question_answer_pkey");

				entity.ToTable("QuizzSessionQuestionAnswer");

				entity.Property(e => e.Id)
					.ValueGeneratedOnAdd()
					.HasColumnName("id");
				entity.Property(e => e.Description)
					.HasColumnType("character varying")
					.HasMaxLength(250)
					.HasColumnName("description");
				entity.HasOne(d => d.QuizzSessionQuestion).WithMany(p => p.QuizzSessionQuestionAnswers)
				.HasForeignKey(d => d.QuizzQuestionId)
				.HasConstraintName("quizz_session_question_answer_fk");
			});

			modelBuilder.Entity<QuizzParticipation>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("quizz_participation_pkey");
				entity.ToTable("QuizzParticipation");
				entity.Property(e => e.Id)
					.ValueGeneratedOnAdd()
					.HasColumnName("id");
				entity.Property(e => e.Name)
					.HasColumnType("character varying")
					.HasMaxLength(100)
					.HasColumnName("name");
				entity.Property(e => e.Score).HasColumnName("score");
				entity.HasOne(d => d.QuizzSession).WithMany(p => p.QuizzParticipations)
					.HasForeignKey(d => d.QuizzSessionId)
					.HasConstraintName("quizz_session_participation_fk");
			});
			// Add other configurations as needed
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=HANMANG;Initial Catalog=VocabularyQuizz;Integrated Security=True;Encrypt=False");
			}
		}
	}
}
