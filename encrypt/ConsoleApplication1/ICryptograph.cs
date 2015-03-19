using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    interface ICryptograph
    {
        byte[] EncryptToByte(string data);
        Stream EncryptToStream(string data);
        string Decrypt(byte[] data);
    }
}
