using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace illopintu.Models
{
    public class Login
    {
        public Boolean comprobarUser(String username, String password)
        {
            Boolean correcto = false;

            using (StreamReader sr = new StreamReader("../textos/users.txt"))
            {
                while (!sr.EndOfStream)
                {
                    String linea = sr.ReadLine();
                    String[] usuario = linea.Split(':');

                    if(usuario[0] == username && usuario[1] == password)
                    {
                        correcto = true;
                    }
                }
                sr.Close();
            }
            return correcto;
        }
    }
}