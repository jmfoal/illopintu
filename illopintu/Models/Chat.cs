using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace illopintu.Models
{
    public class Chat
    {
        String path = HttpContext.Current.Server.MapPath("~/textos/chat.txt");

        public void setChat(string linea)
        {            
            using(StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(linea);
            }
        }

        public List<string> getChat()
        {
            List < string > chat = new List<string>();

            using(StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream) {
                    if (chat.Count < 20)
                    {
                        chat.Add(sr.ReadLine());
                    }
                    else
                    {
                        chat.RemoveAt(0);
                        chat.Add(sr.ReadLine());
                    }
                }
            }
            return chat;
        }
        
    }
}