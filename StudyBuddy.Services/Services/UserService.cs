using AutoMapper;
using FluentValidation;
using StudyBuddy.Domain.Constants;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.EncryptionInterfaces;
using StudyBuddy.Domain.Models;
using StudyBuddy.Domain.Pagination;
using StudyBuddy.Domain.RepositoryInterfaces;
using StudyBuddy.Domain.ServiceInterfaces;
using StudyBuddy.Repositories.Entities;
using StudyBuddy.Services.ErrorHandling;

namespace StudyBuddy.Services.Services
{
    public class UserService : BaseService<User, UserDto, IUserRepository>, IUserService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IPasswordHashingService passwordHashingService;
        private readonly IValidator<UserSignUp> userSignUpValidator;

        public UserService(IUserRepository repository,
            IMapper mapper,
            IRoleRepository roleRepository,
            IPasswordHashingService hashingService,
            IHttpContextWrapper httpContextWrapper,
            IValidator<UserSignUp> userSignUpValidator) : base(repository, mapper, httpContextWrapper)
        {
            this.roleRepository = roleRepository;
            this.passwordHashingService = hashingService;
            this.userSignUpValidator = userSignUpValidator;
        }

        public async Task<UserDto> LoginAsync(UserLogin userLogin)
        {
            var user = await repository.GetUserByEmail(userLogin.Username);

            if (user != null && passwordHashingService.VerifyPassword(userLogin.Password, user.PasswordHash, user.Salt))
            {
                return user;
            }
            else
            {
                throw new ErrorHandling.ValidationException(ExceptionMessages.InvalidCredentialsMessage);
            }
        }

        public async Task<Guid> InsertUserAsync(UserSignUp userSignUp)
        {
            var validationResult = userSignUpValidator.Validate(userSignUp);

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var userDto = Mapper.Map<UserDto>(userSignUp);

            byte[] salt = passwordHashingService.GenerateSalt();
            userDto.PasswordHash = passwordHashingService.HashPassword(userSignUp.Password, salt);
            userDto.Salt = salt;

            var role = await roleRepository.GetRoleByName("User");
            userDto.RoleId = role.Id;

            return await InsertAsync(userDto);
        }

        public async Task<int> UpdateUserAsync(UserPutRequest userModel, Guid id)
        {
            Guid currentUserId = httpContextWrapper.GetCurrentUsersId();
            if (!httpContextWrapper.CanAccess(currentUserId, id))
                throw new UnauthorizedException($" User {currentUserId} with unauthorized access is trying to update the user with id: {id}");

            var user = await repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id '{id}' is not found.");
            }

            var role = await roleRepository.GetRoleByName(userModel.Role);

            UserDto userDto = Mapper.Map<UserDto>(userModel);
            userDto.Id = id;

            if (role != null)
            {
                userDto.RoleId = role.Id;
                userDto.RoleName = role.RoleName;
            }
            Mapper.Map(userDto, user);

            //validationService.ValidateUpdateModel(userDto);

            return await base.UpdateAsync(user);
        }

        public async Task<UsersPaginatedOutput> GetUsersAsync(PaginationInput paginator)
        {
            var resultDtos = await repository.GetEntities(paginator);
            var users = Mapper.Map<UsersPaginatedOutput>(resultDtos);
            users.Entities = users.Entities.Select(u => new UserOutput { Id = u.Id, Token = u.Token, Username = u.Username, FirstName = u.FullName.Split(' ')[0], LastName = u.FullName.Split(' ')[1] }).ToList();
            return users;
        }

        public override Task DeleteAsync(Guid id)
        {
            Guid currentUserId = httpContextWrapper.GetCurrentUsersId();

            if (!httpContextWrapper.CanAccess(currentUserId, id))
                throw new UnauthorizedException($" User {currentUserId} with unauthorized access is trying to delete the user with id: {id}");

            return base.DeleteAsync(id);
        }

        public override Task<UserDto> GetByIdAsync(Guid id)
        {
            Guid currentUserId = httpContextWrapper.GetCurrentUsersId();

            if (!httpContextWrapper.CanAccess(currentUserId, id))
                throw new UnauthorizedException($" User {currentUserId} with unauthorized access is trying to access the data of a user with id: {id}");

            return base.GetByIdAsync(id);
        }
    }
}
