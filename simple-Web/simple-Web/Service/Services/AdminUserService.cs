using AutoMapper;
using Microsoft.EntityFrameworkCore;
using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.DataAccess.Repositories.Comman;
using simple_Web.Domain.Entities;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Helpers;
using simple_Web.Service.Common.Utils;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.ViewModels;
using System.Net;

namespace simple_Web.Service.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IUnitOfWork _repository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AdminUserService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
        {
            this._repository = unitOfWork;
            this._authService = authService;
            this._mapper = mapper;
        }
        public async Task<List<UserViewModel>> GetAllAsync(string search)
        {
            var query = await _repository.Users.GetAll().OrderByDescending(x => x.RegistrationTime).Select(x => (UserViewModel)x).ToListAsync();
            return query;
        }
        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var query = await _repository.Users.GetAll()
            .OrderByDescending(x => x.RegistrationTime)
            .Select(x => (UserViewModel)x)
            .ToListAsync();

            return query;
        }
        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            var query = await (from user in _repository.Users.GetAll()
                         where user.Id == id
                         select new UserViewModel()
                         {
                             Id = user.Id,
                             UserName = user.UserName,
                             Email = user.Email,
                             LastLogin = user.LastLogin,
                             Status = user.Status
                         }).ToListAsync();

            if (query.Count == 0)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
            }
            return query.First();
        }
        public async Task<PagedList<UserViewModel>> GetByNameAsync(PaginationParams @params, string name)
        {
            var query = from user in _repository.Users.Where(x => x.UserName.ToLower().Contains(name.ToLower()))

                 select new UserViewModel()
                 {
                     Id = user.Id,
                     UserName = user.UserName,
                     Email = user.Email,
                     LastLogin = user.LastLogin,
                     Status = user.Status
                 };

    return await PagedList<UserViewModel>.ToPagedListAsync(query, @params);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _repository.Users.FindByIdAsync(id);
            if (student is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found.");
            }
            _repository.Users.Delete(id);
            var res = await _repository.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto userAllUpdateDto)
        {
            var user = await _repository.Users.FindByIdAsync(id);
            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");
            else
            {
                _repository.Users.TrackingDeteched(user);
                if (userAllUpdateDto != null)
                {
                    user.UserName = String.IsNullOrEmpty(userAllUpdateDto.UserName) ? user.UserName : userAllUpdateDto.UserName;
                    user.Email = String.IsNullOrEmpty(userAllUpdateDto.Email) ? user.Email : userAllUpdateDto.Email;
                }
                user.LastLogin = TimeHelper.GetCurrentServerTime();
                _repository.Users.Update(id, user);

                var res = await _repository.SaveChangesAsync();
                return res > 0;
            }
        }

        public async Task<bool> UserStatusUpdateAsync(UserUpdateDto userUpdateDto, int id)
        {
            var user = await _repository.Users.FindByIdAsync(id);
            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");
            else
            {
                _repository.Users.TrackingDeteched(user);
                if (userUpdateDto != null)
                {
                    user.Status = userUpdateDto.Status;
                }
                user.LastLogin = TimeHelper.GetCurrentServerTime();
                _repository.Users.Update(id, user);

                var res = await _repository.SaveChangesAsync();
                return res > 0;
            }
        }

    }
}
