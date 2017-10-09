using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MicroStrutLibrary.Infrastructure.Core
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public static class SecurityHelper
    {
        private const string Key = "0a4a0560976d4c25";

        #region 加密
        /// <summary>
        /// 自定义的加密算法
        /// </summary>
        /// <param name="inputString">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptCustom(string inputString)
        {
            int vkeylen = Key.Length;
            int vKeyPos = 0;
            int vSrcAsc = 0;
            int vRange = 256;
            int voffset = 0;
            string vDest = "";
            Random ro = new Random();
            voffset = ro.Next(vRange);
            vDest = Convert.ToString(voffset, 16);
            if (vDest.Length < 2)
            {
                vDest = "0" + vDest;
            }
            for (int i = 0; i < inputString.Length; i++)
            {
                vSrcAsc = ((int)inputString[i] + voffset) % 255;//求模
                if (vKeyPos < vkeylen)
                {
                    vKeyPos = vKeyPos + 1;
                }
                else
                {
                    vKeyPos = 1;
                }
                vRange = (int)(Key[vKeyPos - 1]);
                vSrcAsc = vSrcAsc ^ vRange;
                string v1 = Convert.ToString(vSrcAsc, 16);
                if (v1.Length < 2)
                {
                    v1 = "0" + v1;
                }
                vDest = vDest + v1;
                voffset = vSrcAsc;
            }
            vDest = vDest.ToUpper();
            return vDest;
        }

        public static string EncryptRSA(string inputString)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSA.ExportParameters(false));

                UnicodeEncoding converter = new UnicodeEncoding();
                byte[] encryptedData = RSA.Encrypt(converter.GetBytes(inputString), false);

                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string EncryptAES(string inputString)
        {
            using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
            {
                UnicodeEncoding converter = new UnicodeEncoding();
                AES.Key = converter.GetBytes(Key);
                AES.IV = converter.GetBytes(Key);

                ICryptoTransform encryptor = AES.CreateEncryptor(AES.Key, AES.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(inputString);
                        }

                        byte[] bytes = msEncrypt.ToArray();

                        return Convert.ToBase64String(bytes);
                    }
                }
            }
        }
        #endregion

        #region 解密
        /// <summary>
        /// 自定义的加密算法的解密程序
        /// </summary>
        /// <param name="inputString">加密后的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptCustom(string inputString)
        {
            int vkeylen = Key.Length;
            int vKeyPos = 0;
            int vSrcPos = 0;
            int vSrcAsc = 0;
            int vTmpSrcAsc = 0;
            string vDest = "";
            string ff = inputString.Substring(0, 2);
            int ddddd = Convert.ToInt32(ff, 16);
            string fff = Convert.ToString(ddddd, 10);
            int vOffset = Convert.ToInt32(fff);
            vSrcPos = 2;
            while (vSrcPos < inputString.Length)
            {
                int eee = Convert.ToInt32(inputString.Substring(vSrcPos, 2), 16);
                vSrcAsc = Convert.ToInt32(Convert.ToString(eee, 10));
                if (vKeyPos < vkeylen)
                {
                    vKeyPos = vKeyPos + 1;
                }
                else
                {
                    vKeyPos = 1;
                }
                char ddddff = (Key[vKeyPos - 1]);
                vTmpSrcAsc = vSrcAsc ^ (int)(Key[vKeyPos - 1]);
                if (vTmpSrcAsc <= vOffset)
                {
                    vTmpSrcAsc = 255 + vTmpSrcAsc - vOffset;
                }
                else
                {
                    vTmpSrcAsc = vTmpSrcAsc - vOffset;
                }
                vDest = vDest + (char)(vTmpSrcAsc);
                vOffset = vSrcAsc;
                vSrcPos = vSrcPos + 2;
            }
            return vDest;
        }

        public static string DecryptRSA(string inputString)
        { 
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSA.ExportParameters(true));

                byte[] decryptedData = RSA.Decrypt(Convert.FromBase64String(inputString), false);

                UnicodeEncoding converter = new UnicodeEncoding();
                return converter.GetString(decryptedData);
            }
        }

        public static string DecryptAES(string inputString)
        {
            using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
            {
                UnicodeEncoding converter = new UnicodeEncoding();
                AES.Key = converter.GetBytes(Key);
                AES.IV = converter.GetBytes(Key);

                ICryptoTransform decryptor = AES.CreateDecryptor(AES.Key, AES.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(inputString)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
