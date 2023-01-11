using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneProgrami.Data.HelperClass
{
    public static class Sifreleme
    {
        private static string MD5(this string parola)
        {
            // MD5CryptoServiceProvider sınıfının örneğini oluşturduk
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            // Hash hesaplaması yaptık ve
            // parametre olarak gelen veriyi byte dizisine dönüştürdük
            byte[] dizi = md5.ComputeHash(Encoding.UTF8.GetBytes(parola));
            StringBuilder sb = new StringBuilder();
            // Her byte'ı dizi içerisinden alarak string türüne dönüştürdük.
            for (int i = 0; i < dizi.Length; i++)
            {
                sb.Append(dizi[i].ToString("x2"));
            }

            //hexadecimal(onaltılık) stringi geri döndürdük. (32 Karakter)
            return sb.ToString();   
        }

        private static string SHA_1(this string parola)
        {
            SHA1 sha1Hasher = SHA1.Create();
            byte[] dizi = sha1Hasher.ComputeHash(Encoding.Default.GetBytes(parola));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dizi.Length; i++)
            {
                sb.Append(dizi[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string Sifrele(this string parola)
        {
            parola = parola.SHA_1();
            parola = parola.MD5();
            parola = parola.SHA_1();
            parola = parola.MD5();
            return parola;
        }

        public static string YeniSifreOlustur(int karakterSayisi)
        {
            var chars = "ABCDEFGHIJKLMNOPRQRSTUWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, karakterSayisi)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
