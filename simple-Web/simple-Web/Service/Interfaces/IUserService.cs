using simple_Web.Service.Common.Utils;
using simple_Web.Service.Dtos;
using simple_Web.Service.ViewModels;

namespace simple_Web.Service.Interfaces
{
    public interface IUserService
    {
        public Task<PagedList<UserViewModel>> GetAllAysnc(PaginationParams @params);
        public Task<bool> DeleteAsync(List<int> ids);
        public Task<bool> BlockAsync(List<int> ids);
        public Task<bool> ActiveAsync(List<int> ids);
    }
}
