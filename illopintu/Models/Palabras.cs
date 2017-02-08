using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace illopintu.Models
{
    public class Palabras
    {
        String path = HttpContext.Current.Server.MapPath("~/textos/Palabras.txt");
        public void NuevaPalabra()
        {
            List<String> Palabras = new List<String>();
            using (StreamReader listaPalabras = new StreamReader(path))
            {
                while (!listaPalabras.EndOfStream)
                {
                    Palabras.Add(listaPalabras.ReadLine());
                }
            }
            Random random = new Random();
            int indice = random.Next(0, Palabras.Count());
            HttpContext.Application["palabra"] = Palabras.ElementAt(indice);
        }

        public bool comprobarPalabra(String palabra)
        {
            bool correcto = false;
            if (palabra == HttpContext.Application["palabra"] as String)
            {
                correcto = true;
            }
        }
    }
}