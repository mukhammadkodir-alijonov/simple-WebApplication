using simple_Web.Service.Common.Utils;
using simple_Web.Service.Dtos;
using simple_Web.Service.ViewModels;

namespace simple_Web.Service.Interfaces
{
    public interface IUserService
    {
        public Task<PagedList<UserViewModel>> GetAllUsernameAysnc(PaginationParams @params);
        public Task<UserViewModel> GetAsync(int id);

        public Task<bool> UpdateAsync(int id, UserUpdateDto entity);

        public Task<bool> DeleteAsync(int id);

        public Task<UserViewModel> GetEmailAsync(string email);
    }
}
