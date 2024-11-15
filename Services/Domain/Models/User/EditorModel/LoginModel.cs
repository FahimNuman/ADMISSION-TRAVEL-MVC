using System.ComponentModel.DataAnnotations;
using Core.ViewService.Models;

namespace Services.Domain.Models.User.EditorModel
{
    public class LoginModel : BaseEditorModel
    {

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string EmailAddress { get; set; }
        public string UserActivateKey { get; set; }
        public bool RememberMe { get; set; }
        public string CookieUserId { get; set; }

        [Display(Name = "User IP")]
        public string UserIP { get; set; }
        public bool? IsFromSignIn { get; set; }
    }
}