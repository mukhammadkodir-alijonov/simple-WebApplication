using simple_Web.Service.Dtos;
using simple_Web.Service.ViewModels;

namespace simple_Web.Service.Interfaces
{
    public interface IAdminService
    {
        public Task<List<AdminViewModel>> GetAllAsync();
        public Task<List<AdminViewModel>> GetAllAsync(string search);
        public Task<AdminViewModel> GetByIdAsync(int id);
        public Task<AdminViewModel> GetEmailAsync(string email);
        public Task<bool> UpdateAsync(int id, AdminUpdateDto adminUpdateDto);
        public Task<bool> DeleteAsync(int id);
    }
}
