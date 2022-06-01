using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Detalle_Exquisito.Models.Usuarios_Modelos
{
    public class Encryptar
    {
        public static string Codificar(string pass)
        {
            SHA256 encryp = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = encryp.ComputeHash(encoding.GetBytes(pass));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }

            return sb.ToString();
                        
        }
    }
}