using Bookah.Data.Interfaces;
using Bookah.Data.Utilities;
using Bookah.Models;
using Bookah.Web.Models;
using Firebase.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookah.Web.Controllers
{
    public class AccountController : Controller
    {
        IUserRepo UserRepo;
        public AccountController(IUserRepo _UserRepo)
        {
            UserRepo = _UserRepo;
        }
        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }
        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel registerUser)
        {
            try
            {
                AppUser appUser = new AppUser()
                {
                    Id = registerUser.Id,
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName,
                    EmailAddress = registerUser.EmailAddress,
                    CellphoneNo = registerUser.CellphoneNo,
                    ID_Number = registerUser.ID_Number,
                    StudentNo = registerUser.StudentNo,
                    Password = registerUser.Password
                };
                if (ModelState.IsValid)
                {



                    var auth = new FirebaseAuthProvider(new FirebaseConfig(AppHelper.ApiKey));

                    await auth.CreateUserWithEmailAndPasswordAsync(registerUser.EmailAddress, registerUser.Password, registerUser.FirstName, true);



                    await UserRepo.Create(appUser);
                    ModelState.AddModelError(string.Empty, "Please verify your email");
                    return RedirectToAction("Login", "Account");


                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(/*registerUser*/);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                //Verification
                if (this.Request.IsAuthenticated)
                {
                    //
                }

            }
            catch (Exception ex)
            {
                //Info
                Console.Write(ex);
            }
            //Info
            return this.View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel loginUser, string returnUrl)
        {
            try
            {
                //Verification
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(AppHelper.ApiKey));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(loginUser.EmailAddress, loginUser.Password);
                    string token = ab.FirebaseToken;
                    var user = ab.User;
                    if (token != "")
                    {
                        this.SignInUser(user.Email, token, false);
                        return this.RedirectToAction("Index", "Home");
                        //return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        //Setting
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            //error occured, redisplay form
            return View(loginUser);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                //Verification
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.RedirectToAction("LogOff", "Account");
        }

        private void SignInUser(string email, string token, bool isPersistent)
        {
            //Initialization
            var claims = new List<Claim>();
            try
            {
                //Setting
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Authentication, token));
                var claimIdentities = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                //Sign In.
                authenticationManager.SignIn(new AuthenticationProperties()
                {
                    IsPersistent = isPersistent
                }, claimIdentities);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void ClaimIdentities(string username, bool isPersistent)
        {
            //Initialization.
            var claims = new List<Claim>();
            try
            {
                //Setting
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdentities = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}
