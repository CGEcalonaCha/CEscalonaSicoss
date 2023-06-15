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
    }
}
