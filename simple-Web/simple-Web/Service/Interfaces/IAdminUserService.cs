using simple_Web.Service.Common.Utils;
using simple_Web.Service.Dtos;
using simple_Web.Service.ViewModels;

namespace simple_Web.Service.Interfaces
{
    public interface IAdminUserService
    {
        public Task<List<UserViewModel>> GetAllAsync();
        public Task<List<UserViewModel>> GetAllAsync(string search);
        public Task<PagedList<UserViewModel>> GetByNameAsync(PaginationParams @params, string name);
        public Task<UserViewModel> GetByIdAsync(int id);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(int id, UserUpdateDto studentAllUpdateDto);
        public Task<bool> UserStatusUpdateAsync(UserUpdateDto userUpdateDto, int id);
    }
}
