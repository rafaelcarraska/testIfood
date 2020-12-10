using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace back_ifood.Components
{
    public class Uteis
    {
        private static Random random = new Random();

        internal string getMD5Hash(string input)
        {
            if (input.Length > 0)
            {
                int chave = geraasc(input);
                decimal temp = 9941 / (chave / ((decimal)Math.Pow(10, Convert.ToDouble(chave.ToString().Length)))) * 10000;
                input += Math.Round(temp, 0).ToString();
            }
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(Encrypt(input, input.Substring(4)).Substring(2));
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        internal string Valida_Periodo(DateTime De, DateTime Ate)
        {
            string Data_Inicial = De.ToString("yyyy-MM-dd");
            string Data_Final = Ate.ToString("yyyy-MM-dd");

            if (Convert.ToUInt32(Data_Final.Substring(0, 4) + Data_Final.Substring(5, 2) + Data_Final.Substring(8)) < Convert.ToUInt32(Data_Inicial.Substring(0, 4) + Data_Inicial.Substring(5, 2) + Data_Inicial.Substring(8)))
                return "Informe uma Data Final igual ou posterior a Data Inicial.";
            else return string.Empty;
        }

        internal List<string> List_Erros(List<string> erros)
        {
            if (erros == null)
                return null;

            return erros.Where(e => e != string.Empty).Distinct().Any() ?
                erros.Where(e => e != string.Empty).Distinct().ToList() : null;

        }

        internal static string Aleatorio(int lengthMax = 10, int tamanho = 1, bool letras = true, bool maiusculas = false, bool numeros = true, bool caracteresEspeciais = false, string preTexto = "")
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";

            chars = letras ? chars : string.Empty;
            chars = maiusculas ? chars + chars.ToUpper() : chars;
            chars = numeros ? chars + "0123456789" : chars;
            chars = caracteresEspeciais ? chars + "!@#$%¨&*()" : chars;

            preTexto += " " + new string(Enumerable.Repeat(chars, new Random().Next(0, lengthMax))
              .Select(s => s[random.Next(s.Length)]).ToArray());

            if (tamanho > 1)
            {
                preTexto = Aleatorio(lengthMax, tamanho - 1, letras, maiusculas, numeros, caracteresEspeciais, preTexto);
            }
            return preTexto;
        }


        private static string Encrypt(string toEncrypt, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length).Substring(3);
        }

        private int geraasc(string val)
        {
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(val);
            int temp = 0;
            int value = 0;
            foreach (byte b in ASCIIValues)
            {
                temp++;
                value += (Convert.ToInt32(b) + 7) * temp;
            }
            return value;
        }
    }
}
