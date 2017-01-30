﻿using System;
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
            if (Session["username"] as String == "") {
                ViewBag.error = "No Estas Logeado";
                return RedirectToAction("index", "Login");
            } else {
                List<String> listaUsers = HttpContext.Application["listaUsers"] as List<String>;
                listaUsers.Add(Session["username"] as String);
                HttpContext.Application["listaUsers"] = listaUsers;
                return View();
            }
        }

        [HttpPost]
        public void setDibujo(int[] clickX, int[]clickY, Boolean[] clickDrag)
        {
            HttpContext.Application["dibujo_clickX"] = clickX;
            HttpContext.Application["dibujo_clickY"] = clickY;
            HttpContext.Application["dibujo_clickDrag"] = clickDrag;
        }

        public JsonResult getDibujo()
        {
            int[] clickX = HttpContext.Application["dibujo_clickX"] as int[];
            int[] clickY = HttpContext.Application["dibujo_clickY"] as int[];
            Boolean[] clickDrag = HttpContext.Application["dibujo_clickDrag"] as Boolean[];

            var dibujo = new
            {
                posX = clickX,
                posY = clickY,
                clickDrag = clickDrag
            };

            return Json(dibujo);
        }
    }
}