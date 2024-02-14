using simple_Web.Domain.Entities;
using simple_Web.Domain.Enums;

namespace simple_Web.Service.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime LastLogin { get; set; }
        public StatusType Status {  get; set; }
        public string Email { get; set; } = string.Empty;
        //public bool IsChecked { get; set; } = true;
        public static implicit operator UserViewModel(User model)
        {
            return new UserViewModel()
            {
                Id = model.Id,
                UserName = model.UserName,
                LastLogin = model.LastLogin,
                Email = model.Email,
                Status = model.Status,
                //IsChecked = model.IsChecked
            };
        }
    }
}
