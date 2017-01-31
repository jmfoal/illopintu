using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using illopintu.Models;

namespace illopintu.Controllers
{
    public class LoginController : Controller
    {
        Login modelo = new Login();
        [HttpGet]
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(String user,String password)
        {
            ActionResult redireccion;
            if (modelo.comprobarUser(user, password))
            {
                Session["username"] = user;
                redireccion = RedirectToAction("index","game");
            } else
            {
                ViewBag.error = "error de Login, Usuario o Contraseña Incorrectas";
                redireccion = View("Index");
            }
            return redireccion;
        }
        
        public ActionResult register(String user, String password)
        {
            ActionResult redireccion;
            if (modelo.registrarUser(user,password))
            {
                 redireccion =  this.login(user, password);
            }
            else
            {
                ViewBag.error = "Error de Registro, Usuario Existente";
                redireccion = View("Index");
            }
            return redireccion;
        }        
    }
}