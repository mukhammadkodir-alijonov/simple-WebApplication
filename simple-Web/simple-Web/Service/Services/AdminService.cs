using AutoMapper;
using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Helpers;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.Interfaces;
using simple_Web.Service.ViewModels;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace simple_Web.Service.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;
        private readonly IMapper _imapper;

        public AdminService(IMapper imapper, IUnitOfWork unitOfWork, IIdentityService identityService)
        {
            this._unitOfWork = unitOfWork;
            this._identityService = identityService;
            this._imapper = imapper;
        }

        public async Task<List<AdminViewModel>> GetAllAsync()
        {
            var query = await _unitOfWork.Admins.GetAll().OrderByDescending(x => x.RegistrationTime).Select(x => (AdminViewModel)x).ToListAsync();
            return query;
        }
        public async Task<List<AdminViewModel>> GetAllAsync(string search)
        {
            var query = _unitOfWork.Admins.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.UserName.ToLower().StartsWith(search.ToLower()) || x.UserName.ToLower().StartsWith(search.ToLower()));
            }

            var result = await query.OrderByDescending(x => x.RegistrationTime).Select(x => (AdminViewModel)x).ToListAsync();
            return result;
        }
        public async Task<AdminViewModel> GetByIdAsync(int id)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null) throw new NotFoundException("Admin", $"{id} not found");
            var adminView = (AdminViewModel)admin;
            return adminView;
        }
        public async Task<AdminViewModel> GetEmailAsync(string email)
        {
            var admin = await _unitOfWork.Admins.GetByEmailAsync(email.Trim());
            if (admin is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "admin not found");
            var adminView = _imapper.Map<AdminViewModel>(admin);
            return adminView;
        }
        public async Task<bool> UpdateAsync(int id, AdminUpdateDto adminUpdatedDto)
        {
            if (id == _identityService.Id)
            {
                var admin = await _unitOfWork.Admins.FindByIdAsync(id);
                if (admin is null) throw new NotFoundException("Admin", $"{id} not found");
                _unitOfWork.Admins.TrackingDeteched(admin);
                if (adminUpdatedDto != null)
                {
                    admin.UserName = string.IsNullOrEmpty(adminUpdatedDto.UserName) ? admin.UserName : adminUpdatedDto.UserName;
                    admin.Email = string.IsNullOrEmpty(adminUpdatedDto.Email) ? adminUpdatedDto.Email : adminUpdatedDto.Email;
                    admin.LastLogin = TimeHelper.GetCurrentServerTime();
                    _unitOfWork.Admins.Update(id, admin);
                    var result = await _unitOfWork.SaveChangesAsync();
                    return result > 0;
                }
                else throw new ModelErrorException("", "Not found");
            }
            else throw new ModelErrorException("", "Permission not granted");
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null) throw new NotFoundException("Admin", $"{id} not found");
            _unitOfWork.Admins.Delete(id);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
