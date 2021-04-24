using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Ultilities
{
    public class Md5Helper
    {
        public static string GetMd5Hash(string input)
        {
            MD5 md5hash = MD5.Create();
            byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for(int i=0; i< data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static bool VerifyMd5(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);
            StringComparer compare = StringComparer.OrdinalIgnoreCase;
            if (0 == compare.Compare(hashOfInput, hash))
            {
                return true;
            }
            return false;
        }
    }
}
