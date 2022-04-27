using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BookLibraryClassLibrary.Authentication
{
    public class UserPassHashHandlerTwo
    {
        private static readonly RandomNumberGenerator _gnr = RandomNumberGenerator.Create();

        private static readonly int _defaultSaltSize = 8;

        private readonly byte[] _saltBytes;
        private readonly byte[] _hashBytes;


        private UserPassHashHandlerTwo(byte[] saltBytes, byte[] hashBytes)
        {
            _saltBytes = saltBytes;
            _hashBytes = hashBytes;
        }

        public static string Compute(string password, int saltSize, Type algoType)
        {
            return Compute(password, GenerateSaltBytes(saltSize), algoType);
        }

        public static string Compute(string password, Type algoType)
        {
            return Compute(password, GenerateSaltBytes(_defaultSaltSize), algoType);
        }

        public static string Compute(string password, byte[] salt, Type algoType)
        {

            var passBytes = Encoding.UTF8.GetBytes(password);
            var fullBytes = GenerateHashBytes(salt, passBytes);
            var hashFunc = (SHA256Managed)Activator.CreateInstance(algoType);

            var hashBytes = hashFunc.ComputeHash(fullBytes);

            return new UserPassHashHandlerTwo(salt, hashBytes).ToString();
        }

        public static bool VerifyHash(string password, string salt, string hash, Type algoType)
        {
            return Compute(password, Convert.FromBase64String(salt), algoType) == hash;
        }

        private static byte[] GenerateSaltBytes(int saltSize)
        {
            var salt = new byte[saltSize];
            _gnr.GetBytes(salt);
            return salt;
        }

        private static byte[] GenerateHashBytes(byte[] saltBytes, byte[] passBytes)
        {
            var fullBytes = new byte[saltBytes.Length + passBytes.Length];

            for (int i = 0; i < saltBytes.Length; i++)
            {
                fullBytes[i] = saltBytes[i];
            }

            for (int i = 0; i < passBytes.Length; i++)
            {
                fullBytes[saltBytes.Length + i] = passBytes[i];
            }

            return fullBytes;
        }

        public override string ToString()
        {
            return $"salt${Convert.ToBase64String(_saltBytes)}:{Convert.ToBase64String(_hashBytes)}";
        }

    }
}
