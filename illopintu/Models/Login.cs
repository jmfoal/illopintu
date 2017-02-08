using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace illopintu.Models
{
    public class Login
    {
        MD5CryptoServiceProvider cifrador = new MD5CryptoServiceProvider();
        String path = HttpContext.Current.Server.MapPath("~/textos/users.txt");
        public Boolean comprobarUser(String username, String password)
        {
            byte[] cadena = UTF8Encoding.UTF8.GetBytes(password);
            cadena = cifrador.ComputeHash(cadena);
            password = Convert.ToBase64String(cadena);

            Boolean correcto = false;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    String linea = sr.ReadLine();
                    String[] usuario = linea.Split(':');

                    if(usuario[0] == username && usuario[1] == password)
                    {
                        correcto = true;
                        List<string> listaUsers = HttpContext.Application["listaUsers"] as List<String>;
                        if (listaUsers.Contains(username))
                        {

                        } else
                        {
                            listaUsers.Add(username);
                            httpContext.Application["listaUsers"] = listaUsers;
                        }
                    }
                }
                sr.Close();
            }
            return correcto;
        }

        public Boolean registrarUser(String username,String password)
        {
            byte[] cadena = UTF8Encoding.UTF8.GetBytes(password);
            cadena = cifrador.ComputeHash(cadena);
            password = Convert.ToBase64String(cadena);

            Boolean existe = false;
            Boolean correcto = false;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    String linea = sr.ReadLine();
                    String[] usuario = linea.Split(':');

                    if (usuario[0] == username)
                    {
                        existe = true;
                    }
                }
                sr.Close();
            }
            if (!existe)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(username + ":" + password);
                }
                correcto = true;
            }
            return correcto;
        }
    }
}