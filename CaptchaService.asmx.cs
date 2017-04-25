using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography;

namespace Distinct_CAPTCHA_Web_Service
{
    /// <summary>
    /// Summary description for CaptchaService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CaptchaService : System.Web.Services.WebService
    {
        //A password used to generate a key for decryption.
        private static string _password = "captchapass";

        [WebMethod]
        public GenerateCaptcha Generate()
        {
            GenerateCaptcha item = new GenerateCaptcha();
            item.Tokan = EncryptStringAES(GenerateCaptchaText(9));
            item.ImageUrl = String.Format("http://{0}/{1}?key={2}", HttpContext.Current.Request.Url.Authority, "CaptchaImage.aspx", HttpUtility.UrlEncode(item.Tokan));
            return item;
        }

        [WebMethod]
        public bool Validate(string tokan, string inputtext)
        {
            if (tokan == null || inputtext == null)
            {
                return false;
            }
            if (DecryptStringAES(tokan).ToLower() == inputtext.Trim().ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary> 
        /// Encrypt the given string using AES.  The string can be decrypted using  
        /// </summary> 
        /// <param name="plainText">The text to encrypt.</param> 
        private static string EncryptStringAES(string plainText)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Hash the passphrase using MD5
            // use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder that use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_password));

            // Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(plainText);

            // Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);

        }

        /// <summary> 
        /// Decrypt the given string.  Assumes the string was encrypted using  
        /// </summary> 
        /// <param name="cipherText">The text to decrypt.</param> 
        public static string DecryptStringAES(string Tokan)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // hash the passphrase using MD5
            // use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder that use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_password));

            // Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            //Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            string cipherText = Tokan.Replace(" ", "+");
            // Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(cipherText);

            //Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);

        }

        /// <summary>Generates the random captcha word.</summary>
        /// <param name="length">The length of the captcha word text.</param>
        /// <returns>A dynamically-generated random captcha word.</returns>
        private static string GenerateCaptchaText(int length)
        {
            Random rand = new Random();
            string characters = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string word = String.Empty;

            for (int i = 0; i < length; i++)
            {
                word += characters[rand.Next(characters.Length)];
            }

            return word;
        }
    }

    public class GenerateCaptcha
    {
        public string Tokan;
        public string ImageUrl;
    }
}
