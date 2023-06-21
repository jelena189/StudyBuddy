using FluentValidation;
using StudyBuddy.Domain.Models;
using StudyBuddy.Domain.RepositoryInterfaces;

namespace StudyBuddy.Services.Validation
{
    public class UserSignUpValidator : AbstractValidator<UserSignUp>
    {
        public UserSignUpValidator(IUserRepository userRepository)
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("'{PropertyValue}' is not a valid email address.")
                .Must((user, email) => userRepository.IsEmailUnique(email))
                .WithMessage("Entered email address already exists.");

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .Length(2, 15);

            RuleFor(user => user.Username)
                .NotEmpty()
                .Length(2, 15)
                .Must((user, email) => userRepository.IsUsernameUnique(email))
                .WithMessage("Entered Username already exists.");

            RuleFor(user => user.Password)
                .Must((user, password) => IsPasswordValid(password))
                .WithMessage("Password must be at least 8 characters long and must contain at least one capital letter, one number and one lower case letter.");
        }

        private bool IsPasswordValid(string password)
        {
            return password.Any(char.IsUpper)
                && password.Any(char.IsLower)
                && password.Any(char.IsNumber)
                && password.Any(ch => !char.IsWhiteSpace(ch))
                && password.Length > 7;
        }
    }
}
