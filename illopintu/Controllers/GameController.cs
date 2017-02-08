using illopintu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace illopintu.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["username"] as String == null) {
                ViewBag.error = "No Estas Logeado";
                return View("index", "Login");
            } else {                
                ViewBag.user = Session["username"] as String;
                List<String> listaUsers = HttpContext.application["listaUsers"] as List<String>;
                if(listaUsers.Count() == 1 && listaUsers.Contains(Session["username"] as String))
                {
                    HttpContext.Application["dibuja"] = Session["username"] as String;
                    new Palabras().nuevaPalabra();
                    ViewBag.palabra = HttpContext.Application["palabra"];
                }
                ViewBag.dibuja = HttpContext.Application["dibuja"] as String;
                return View();
            }
        }

        [HttpPost]
        public JsonResult setDibujo(int[] posX, int[]posY, Boolean[] clickDrag)
        {
            HttpContext.Application["dibujo_clickX"] = posX;
            HttpContext.Application["dibujo_clickY"] = posY;
            HttpContext.Application["dibujo_clickDrag"] = clickDrag;
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDibujo()
        {
            int[] clickX = HttpContext.Application["dibujo_clickX"] as int[];
            int[] clickY = HttpContext.Application["dibujo_clickY"] as int[];
            Boolean[] clickDrag = HttpContext.Application["dibujo_clickDrag"] as Boolean[];
            String dibuja = HttpContext.Application["dibuja"] as String;
            int numLetras = null;

            var dibujo = new
            {
                posX = clickX,
                posY = clickY,
                clickDrag = clickDrag,
                Dibuja = dibuja,
                numLetras = (HttpContext.Application["palabra"] as String).Length()
            };

            return Json(dibujo,JsonRequestBehavior.AllowGet);
        }

        public JsonResult setChat(string User, string Mensaje)
        {
            // llamar aqui a la comprobacion del juego            
            Chat chat = new Chat();
            Palabras palabras = new Palabras();            
            chat.setChat(User + ":" + Mensaje);
            bool correcto = false;
            String palabra = null;

            if (palabras.comprobarPalabra(Mensaje))
            {
                correcto = true;
                HttpContext.Application["dibuja"] = User;
                palabras.nuevaPalabra();
            }
            var json = new
            {
                correcto = correcto,
                palabra = HttpContext.Application["palabra"] as String
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getChat()
        {
            Chat chat = new Chat();            
            return Json(chat.getChat(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult logout(string Username)
        {
            List<string> users = HttpContext.Application["listaUsers"] as List<String>;
            users.Remove(Username);
            HttpContext.Application["listaUsers"] = users;
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}