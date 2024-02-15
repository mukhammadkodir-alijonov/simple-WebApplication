using simple_Web.Service.Dtos;

namespace simple_Web.Service.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> RegisterAsync(AccountRegisterDto registerDto);
        public Task<string> LoginAsync(AccountLoginDto accountLoginDto);
    }
}
