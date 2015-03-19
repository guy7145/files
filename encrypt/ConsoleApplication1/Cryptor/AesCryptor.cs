using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ConsoleApplication1
{
    class AesCryptor : ICryptograph
    {
        Aes myAes;
        ICryptoTransform encryptor, decryptor;

        public AesCryptor(byte[] Key, byte[] IV)
        {
            myAes = Aes.Create();
            myAes.Key = Key;
            myAes.IV = IV;
            encryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);
            decryptor = myAes.CreateDecryptor(myAes.Key, myAes.IV);
        }
        byte[] ICryptograph.Encrypt(string data)
        {
            byte[] result;

            // Create the streams used for encryption. 
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(data);
                    }
                    result = msEncrypt.ToArray();
                }
            }
            return result;
        }
        string ICryptograph.Decrypt(byte[] data)
        {
            string result;

            using (MemoryStream msDecrypt = new MemoryStream(data))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                            // and place them in a string.
                            result = srDecrypt.ReadToEnd();
                    }
                }
            }

            return result;
        }
    }
}
