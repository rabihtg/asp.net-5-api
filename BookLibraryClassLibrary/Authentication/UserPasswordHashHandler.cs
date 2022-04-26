using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Authentication
{
    public class UserPasswordHashHandler
    {
        private static readonly RandomNumberGenerator _gnr = RandomNumberGenerator.Create();

        private readonly byte[] _hashBytes;

        private readonly byte[] _saltBytes;

        private UserPasswordHashHandler(byte[] saltBytes, byte[] hashBytes)
        {
            _saltBytes = saltBytes;
            _hashBytes = hashBytes;
        }

        public static string CreateHash(string password, Type hashAlgo)
        {
            return CreateHash(password, GenerateSalt(8), hashAlgo);
        }

        public static string CreateHash(string password, int saltSize, Type hashAlgo)
        {
            return CreateHash(password, GenerateSalt(saltSize), hashAlgo);
        }

        public static string CreateHash(string password, byte[] saltBytes, Type hashAlgo)
        {
            if (!hashAlgo.IsSubclassOf(typeof(HashAlgorithm))) throw new Exception("Invalid Algorithm Type");

            var fullBytes = GenerateFullBytes(Encoding.UTF8.GetBytes(password), saltBytes);

            var hashFunc = (HashAlgorithm)Activator.CreateInstance(hashAlgo);

            var hashByte = hashFunc.ComputeHash(fullBytes);

            return new UserPasswordHashHandler(saltBytes, hashByte).ToString();
        }

        public static bool VerifyHash(string password, string salt, string hash, Type hashAlgo)
        {
            return hash == CreateHash(password, Convert.FromBase64String(salt), hashAlgo).ToString();
        }
        private new string ToString()
        {
            return $"salt${Convert.ToBase64String(_saltBytes)}:{Convert.ToBase64String(_hashBytes)}";
        }
        private static byte[] GenerateSalt(int saltSize)
        {
            byte[] salt = new byte[saltSize];

            _gnr.GetBytes(salt);

            return salt;
        }
        private static byte[] GenerateFullBytes(byte[] passBytes, byte[] saltBytes)
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

    }
}
