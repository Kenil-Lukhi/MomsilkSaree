using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Saree.Common
{
    public static class CommonHelper 
    {
        public static string Encrypt(string strText)
        {
            if (strText == null || strText == "")
            {
                return strText;
            }
            else
            {
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
                try
                {
                    string strEncrKey = "&%#@?,:*";
                    byte[] bykey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                    byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static string Decrypt(string strText)
        {
            if (strText == null || strText == "")
            {
                return strText;
            }
            else
            {
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
                byte[] inputByteArray = new byte[strText.Length + 1];
                try
                {
                    string strEncrKey = "&%#@?,:*";
                    byte[] byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(strText);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
    }
    public class Transtatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}