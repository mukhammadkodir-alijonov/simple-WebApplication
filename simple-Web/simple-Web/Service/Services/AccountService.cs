using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.Domain.Entities;
using simple_Web.Domain.Enums;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces;
using System.Net.Mail;
using System.Net;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Security;
using simple_Web.Service.Common.Helpers;

namespace simple_Web.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _repository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper, IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            this._repository = unitOfWork;
            this._authService = authService;
            this._mapper = mapper;
        }

        public async Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto)
        {
            var emailcheck = await _repository.Admins.FirstOrDefault(x => x.Email == adminRegisterDto.Email);
            if (emailcheck is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Email alredy exist");

            var hashresult = PasswordHasher.Hash(adminRegisterDto.Password);
            var admin = _mapper.Map<Admin>(adminRegisterDto);
            admin.Role = Role.Admin;
            admin.PasswordHash = hashresult.Hash;
            admin.Salt = hashresult.Salt;
            admin.RegistrationTime = TimeHelper.GetCurrentServerTime();
            admin.LastLogin = TimeHelper.GetCurrentServerTime();
            _repository.Admins.Add(admin);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RegisterAsync(AccountRegisterDto registerDto)
        {
            var emailcheck = await _repository.Users.FirstOrDefault(x => x.Email == registerDto.Email);
            if (emailcheck is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Email alredy exist");

            var hasherResult = PasswordHasher.Hash(registerDto.Password);
            var user = _mapper.Map<User>(registerDto);
            user.Role = Role.User;
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;
            user.Status = StatusType.Active;
            user.RegistrationTime = TimeHelper.GetCurrentServerTime();
            user.LastLogin = TimeHelper.GetCurrentServerTime();
            _repository.Users.Add(user);
            var databaseResult = await _repository.SaveChangesAsync();
            return databaseResult > 0;
        }
        
        public async Task<string> LoginAsync(AccountLoginDto accountLoginDto)
        {
            var admin = await _repository.Admins.FirstOrDefault(x => x.Email == accountLoginDto.Email);
            if (admin != null)
            {
                var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, admin.Salt, admin.PasswordHash);
                if (hasherResult)
                {
                    string token = _authService.GenerateToken(admin, "admin");
                    return token;
                }
                else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
            }

            var user = await _repository.Users.FirstOrDefault(x => x.Email == accountLoginDto.Email);
            if (user != null)
            {
                var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, user.Salt, user.PasswordHash);
                if (hasherResult)
                {
                    string token = _authService.GenerateToken(user, "user");
                    return token;
                }
                else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
            }

            throw new NotFoundException(nameof(accountLoginDto.Email), "No user with this email is found!");
        }

        public async Task<string> RoleCheckerAsync(string email)
        {
            var checkAdmin = await _repository.Admins.FirstOrDefault(x => x.Email == email);
            if (checkAdmin == null)
            {
                var checkUser = await _repository.Users.FirstOrDefault(x => x.Email == email);
                return "User";
            }
            else
                return "Admin";
        }
    }
}
