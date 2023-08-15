using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using MVC_Core.Models.DTO;
using LoginModel = MVC_Core.Models.DTO.LoginModel;
using RegisterModel = MVC_Core.Models.DTO.RegisterModel;

namespace MVC_Core.Reponsitories.Abstract
{
    public interface IUserAuthentication
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegisterAnsync(RegisterModel model);
        Task LogoutAnsync();

    }
}
