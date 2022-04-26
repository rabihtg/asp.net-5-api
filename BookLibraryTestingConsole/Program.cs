using System;
using System.Security.Cryptography;
using System.Text;

namespace BookLibraryTestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var passHash = PassHash.Encode<SHA256>("rabih123", 16);

            var oldHash = Convert.ToBase64String(passHash.Passhash);
            var oldSalt = Convert.ToBase64String(passHash.Salt);



            //Console.WriteLine(PassHash.Verify<SHA256>("rabih123", Convert.FromBase64String(oldSalt), Convert.FromBase64String(oldHash)));


            Console.WriteLine(Convert.ToBase64String(passHash.Passhash));
            Console.WriteLine(Convert.ToBase64String(passHash.Salt));

            /*int stopIndex = Math.Min(asciBytes.Length, utf8Bytes.Length);

            stopIndex = Math.Min(utf32Bytes.Length, stopIndex);


            int max = Math.Max(asciBytes.Length, utf8Bytes.Length);

            max = Math.Max(utf32Bytes.Length, max);

            Console.WriteLine($"max: {max}, min: {stopIndex}");

            for (int i = 0; i < stopIndex; i++)
            {
                Console.WriteLine($"Ascii: {asciBytes[i]}");
                Console.WriteLine($"utf8: {utf8Bytes[i]}");
                Console.WriteLine($"utf32: {utf32Bytes[i]}");
            }*/

        }
    }
}
