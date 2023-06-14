using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class DigitoController : Controller
    {
        [HttpGet]
        public ActionResult VistaGeneral()
        {
            ML.Historial historial = new ML.Historial();
            ML.Result result = new ML.Result();
            int? IdUsuario = HttpContext.Session.GetInt32("IdUsuario");//Obtener el idUsuario
            result = BL.Usuario.HistorialGetByIdUsuario(IdUsuario.Value);//Obtener el historial del usuario

            if (result.Correct)
            {
                historial.Historials = result.Objects;
                return View(historial);
            }
            else
            {
                return View(historial);
            }
            //return View(historial);
        }
        [HttpPost]
        public ActionResult VistaGeneral(ML.Historial historial)
        {
            ML.Result result = new ML.Result();
            int? IdUsuario = HttpContext.Session.GetInt32("IdUsuario");//Obtener el IdUsuario
            historial.Usuario = new ML.Usuario();
            historial.Usuario.IdUsuario = IdUsuario.Value;//guardarlo en ML.Historial
            historial.SuperDigito = BL.Historial.CalcularSuperDigito(historial.Digito);//Calcular el SuperDigito
            ML.Result resultAdd = BL.Historial.Add(historial);//agregarlo a la BD
            result = BL.Usuario.HistorialGetByIdUsuario(IdUsuario.Value);//Obtener el historial

            if (result.Correct && resultAdd.Correct)
            {
                historial.Historials = result.Objects;
                return View(historial);
            }
            else
            {
                return View(historial);
            }
            //return View(historial);
        }

        [HttpGet]
        public ActionResult HistorialGetByIdUsuario(ML.Historial historial)
        {
            int? IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ML.Result result = BL.Usuario.HistorialGetByIdUsuario(IdUsuario.Value);//Obtener el historial
            //historial.SuperDigito = BL.Historial.CalcularSuperDigito(historial.Digito);//checar si hacerlo

            if (result.Correct)
            {
                historial.Historials = result.Objects;
                return View("VistaGeneral", historial);
                //return View("VistaGeneral");
            }
            else
            {
                return View(historial);
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Historial historial)
        {
            ML.Result result = new ML.Result();

            result = BL.Historial.Delete(historial);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino el registro satisfactoriamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return View("Modal");
        }
    }
}
