using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Test_qasysapp.Models;
using Test_qasysapp.Models.Repository;
using Test_qasysapp.Models.ViewModels;

namespace Test_qasysapp.Controllers
{
	public class AuthController : Controller
	{
		private AuthViewModel authModel = new AuthViewModel();
		private UserRepos _userRepos;
		private UserBadgesRepos _badges;

		public AuthController()
		{
			_userRepos = new UserRepos();
			_badges = new UserBadgesRepos();
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(User model)
		{
			//if (ModelState.IsValid)
			//{
				User user = _userRepos.GetUsers()
					.First(u=> u.Password== model.Password && u.Login == model.Login);
				if (user != null)
				{
					await Authenticate(user.Id);
					if (user.RoleId == 1)
					{
						return RedirectToAction("Index", "Question");
					}
					else if (user.RoleId == 2)
					{
						return RedirectToAction("Index", "Admin");
					}
				}
			//}


			/*
			foreach (var item in userRepos.GetUsers())
			{
				if (item.Login == user.Login && item.Password == user.Password)
				{
					//authModel.User = item;
					//var role = userRoles.GetUserRoles().First(u => u.UserId == authModel.User.Id);
					//if (role.RoleId == 1)
					//{
					//	return RedirectToAction("Index", "Home", authModel.User);
					//}
					//else if (role.RoleId == 2)
					//{
					//	return RedirectToAction("Index", "Admin", authModel.User);
					//}

                    if (item.Role == "user")
                    {
                        return RedirectToAction("Index", "Question", new { id = item.Id });
					}
                    else if (item.Role == "admin")
                    {
                        return RedirectToAction("Index", "Admin", item.Id);
                    }
                }
			}*/


			return View(nameof(Index));
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Register(User user)
		{
            //userRolesVM.UserId = user.Id;
            //userRolesVM.RoleId = 1;
            //userRoles.AddUserRole(userRolesVM);

            if (_userRepos.AddUser(user))
			{
                
                ModelState.Clear();
                return RedirectToAction(nameof(Index));

            }
			return View();
        }

		private async Task Authenticate(int user_id)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(user_id))
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public ActionResult Account(int userid)
		{
			var user = _userRepos.GetUsers().First(u=>u.Id == userid);
			ViewBag.UserRating = _userRepos.GetRating(userid);
			int rating = _userRepos.GetRating(userid);

			if (rating > 10)
			{
				UserBadges userBadges = new UserBadges();
				var bg = _badges.GetAll().Any(b=>b.UserId == userid && b.BadgeId == 1);
				if (bg == false)
				{
					userBadges.BadgeId = 1;
					userBadges.UserId = userid;
					_badges.AddBadge(userBadges);
				}
				ViewBag.BadgeUser = _badges.GetAll().First(b => b.UserId == userid && b.BadgeId == 1).BadgeId;
            }

			return View(user);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Auth");
		}

	}
}
