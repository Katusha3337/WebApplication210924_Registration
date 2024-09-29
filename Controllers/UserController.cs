using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApplication210924_Registration.Models;

namespace WebApplication210924_Registration.Controllers
{
    public class UserController : Controller
    {
        // Действие для отображения формы регистрации
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Действие для обработки отправленной формы
        [HttpPost]
        public IActionResult RegisterPost(User user)
        {
            // Проверка валидности данных
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                ModelState.AddModelError("Username", "Username is required");
            }

            if (string.IsNullOrWhiteSpace(user.Email) || !IsValidEmail(user.Email))
            {
                ModelState.AddModelError("Email", "Invalid Email Address");
            }

            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Password must be at least 6 characters long");
            }

            if (ModelState.IsValid)
            {
                
                ViewBag.Message = "Registration successful!";
                return View("Success");
            }
            else
            {
                // Если данные неверны, отобразить форму с сообщениями об ошибках
                return View("Register", user);
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
