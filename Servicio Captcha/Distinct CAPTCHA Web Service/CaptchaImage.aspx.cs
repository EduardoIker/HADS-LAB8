using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Drawing.Imaging;

namespace Distinct_CAPTCHA_Web_Service
{
    public partial class CaptchaImage : System.Web.UI.Page
    {
        //A password used to generate a key for decryption.
        private static string _password = "captchapass";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["key"] != null)
            {
                string captchaStr = DecryptStringAES(HttpUtility.UrlDecode(Request.QueryString["key"]));
                Bitmap captchaImg = GenerateCaptchaImage(captchaStr, new Size(240, 80), "Comic Sans MS");

                Context.Response.ContentType = "image/JPEG";
                captchaImg.Save(Context.Response.OutputStream, ImageFormat.Jpeg);
                captchaImg.Dispose();
            }
        }

        /// <summary>Generates the captcha image.</summary>
        /// <param name="text">The text to be rendered into the image.</param>
        /// <param name="size">The size of the image to generate.</param>
        /// <param name="fontFamily">The font family name of the image to generate.</param>
        /// <returns>A dynamically-generated captcha image.</returns>
        public Bitmap GenerateCaptchaImage(string text, Size size, string fontFamily)
        {
            Bitmap bmpCaptcha = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(bmpCaptcha);
            Random rand = new Random();
            Font font = new Font(fontFamily, 30, FontStyle.Bold);

            // Draw white background 
            graphics.Clear(Color.White);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Draw the string into the center of the image
            graphics.DrawString(text, font, Brushes.Black, 3, 3);

            // Distort the final image and return it.
            DistortImage(bmpCaptcha, rand.Next(15));

            // Clean up
            font.Dispose();
            graphics.Dispose();

            return bmpCaptcha;
        }

        /// <summary>Distorts the image.</summary>
        /// <param name="b">The image to be transformed.</param>
        /// <param name="distortion">An amount of distortion.</param>
        private static void DistortImage(Bitmap b, double distortion)
        {
            int width = b.Width, height = b.Height;

            // Copy the image so that we're always using the original for source color
            using (Bitmap copy = (Bitmap)b.Clone())
            {
                // Iterate over every pixel
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Adds a simple wave
                        int newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
                        int newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
                        if (newX < 0 || newX >= width) newX = 0;
                        if (newY < 0 || newY >= height) newY = 0;
                        b.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }
        }

        /// <summary> 
        /// Decrypt the given string.  Assumes the string was encrypted using  
        /// </summary> 
        /// <param name="cipherText">The text to decrypt.</param> 
        public static string DecryptStringAES(string cipherText)
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

            cipherText = cipherText.Replace(" ", "+");
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
    }
}