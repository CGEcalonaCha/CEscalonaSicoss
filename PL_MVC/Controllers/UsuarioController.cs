using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            ML.Result result = BL.Usuario.GetByUserName(UserName);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                int IdUsuario = usuario.IdUsuario;
                HttpContext.Session.SetInt32("IdUsuario", IdUsuario);
                usuario.IdUsuario = IdUsuario;

                if (Password == usuario.Password)
                {
                    //return RedirectToAction("Index", "Home");
                    HttpContext.Session.GetInt32("IdUsuario");
                    return RedirectToAction("VistaGeneral" ,"Digito");
                }
                else
                {
                    ViewBag.Message = "Contraseña incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "Contraseña incorrecta";
                return PartialView("Modal");
            }
        }
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            //Add
            result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                ViewBag.Message = "Se completo el registro satisfactoriamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al insertar el registro";
            }
            return View("Modal");
        }
        //[HttpGet]
        //public ActionResult Login()
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    return View(usuario);
        //}
        //[HttpPost]
        //public ActionResult Login(ML.Usuario usuario, string password, string userName)
        //{
        //    // Crear una instancia del algoritmo de hash bcrypt
        //    var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
        //    // Obtener el hash resultante para la contraseña ingresada 
        //    var passwordHash = bcrypt.GetBytes(20);

        //    if (usuario.UserName != null)
        //    {
        //        // Insertar usuario en la base de datos
        //        usuario.Password = passwordHash;
        //        ML.Result result = BL.Usuario.Add(usuario);
        //        return View();
        //    }
        //    else
        //    {
        //        // Proceso de login
        //        // Proceso de login
        //        ML.Result result = BL.Usuario.GetByUserName(usuario.userName);
        //        usuario = (ML.Usuario)result.Object;

        //        if (usuario.Password.SequenceEqual(passwordHash))
        //        {

        //            return RedirectToAction("Form", "Digito");
        //        }
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //public IActionResult Login(ML.Usuario usuario)
        //{
        //    ML.Result result = BL.Usuario.GetByUserName(usuario);
        //    if (result.Correct)
        //    {
        //        ML.Usuario usuarioUnbox = (ML.Usuario)result.Object;
        //        //SESION 
        //        int IdUsuario = usuarioUnbox.IdUsuario;
        //        HttpContext.Session.SetInt32("IdUsuario", IdUsuario);
        //        usuarioUnbox.IdUsuario = IdUsuario;

        //        if (usuario.Password == usuarioUnbox.Password)
        //        {
        //            HttpContext.Session.GetInt32("IdUsuario");
        //            return RedirectToAction("Form", "Digito");
        //        }
        //        else
        //        {
        //            HttpContext.Session.GetInt32("IdUsuario");
        //            return RedirectToAction("Form", "Digito");
        //            ViewBag.Message = "La contraseña es incorrecta";
        //            return PartialView("Modal");

        //        }

        //    }
        //    else
        //    {
        //        ViewBag.Message = "El Nombre de Usuario es incorrecta o no existe";
        //        return PartialView("Modal");
        //    }

        //}

    }
}
