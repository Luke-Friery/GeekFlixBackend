using FluentValidation;

namespace Application.MovieList.Commands.AddMovie
{
  public class AddMovieCommandValidator : AbstractValidator<AddMovieCommand>
  {
    public AddMovieCommandValidator()
    {
      RuleFor(v => v.Title).MaximumLength(100).NotEmpty();
      RuleFor(v => v.Url).NotEmpty();
      RuleFor(v => v.PhotoUrl).NotEmpty();
      RuleFor(v => v.ReleasedYear).NotEmpty();
    }
  }
}
