using SUS.HTTP;
using SUS.MvcFramework;
using Git.Services;
using Git.ViewModels.Users;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        
        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserInputModel login)
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }

            string userId = usersService.GetUserId(login.Username, login.Password);

            if (userId == null)
            {
                return Error("Invalid username or password.");
            }
            
            SignIn(userId);

            return Redirect("/Repositories/All");
        }
        
        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserInputModel register)
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }

            if (string.IsNullOrEmpty(register.Username) ||
                register.Username.Length < 5 ||
                register.Username.Length > 20)
            {
                return Error("Username should be between 5 and 20 characters.");
            }
            
            if (!usersService.IsUsernameAvailable(register.Username))
            {
                return Error("Username is not available.");
            }

            if (string.IsNullOrEmpty(register.Email))
            {
                return Error("Email is required.");
            }

            if (!usersService.IsEmailAvailable(register.Email))
            {
                return Error("Email is not available.");
            }
            
            if (string.IsNullOrEmpty(register.Password) ||
                register.Password.Length < 6 ||
                register.Password.Length > 20)
            {
                return Error("Password should be between 6 and 20 characters.");
            }

            if (register.Password != register.ConfirmPassword)
            {
                return Error("Passwords do not match.");
            }
            
            usersService.CreateUser(register.Username, register.Email, register.Password);

            return Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (IsUserSignedIn())
            {
                this.SignOut();
            }

            return this.Redirect("/");
        }
    }
}