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
        private readonly RandomNumberGenerator _gnr = RandomNumberGenerator.Create();

        private readonly byte[] _hashBytes;

        private readonly byte[] _saltBytes;

        private readonly byte[] _passBytes;

        private UserPasswordHashHandler(byte[] saltBytes, byte[] hashBytes)
        {
            _saltBytes = saltBytes;
            _hashBytes = hashBytes;
        }

        public UserPasswordHashHandler(string password, int saltLength = 8)
        {
            _saltBytes = GenerateSalt(saltLength);
            _passBytes = Encoding.UTF8.GetBytes(password);
        }

        public UserPasswordHashHandler(string password, string salt)
        {
            _saltBytes = Convert.FromBase64String(salt);
            _passBytes = Encoding.UTF8.GetBytes(password);
        }

        public string CreateHash(Type hashAlgo)
        {
            if (!hashAlgo.IsSubclassOf(typeof(HashAlgorithm))) throw new Exception("Invalid Algorithm Type");
            var fullBytes = GenerateFullBytes();

            var hashFunc = (HashAlgorithm)Activator.CreateInstance(hashAlgo);

            var hashByte = hashFunc.ComputeHash(fullBytes);

            return new UserPasswordHashHandler(_saltBytes, hashByte).ToString();
        }

        public static bool VerifyHash(string password, string salt, string hash, Type hashAlgo)
        {
            return hash == new UserPasswordHashHandler(password, salt).CreateHash(hashAlgo).ToString();
        }

        private new string ToString()
        {
            return $"salt${Convert.ToBase64String(_saltBytes)}:{Convert.ToBase64String(_hashBytes)}";
        }
        private byte[] GenerateSalt(int saltSize)
        {
            byte[] salt = new byte[saltSize];

            _gnr.GetBytes(salt);

            return salt;
        }
        private byte[] GenerateFullBytes()
        {
            var fullBytes = new byte[_saltBytes.Length + _passBytes.Length];

            for (int i = 0; i < _saltBytes.Length; i++)
            {
                fullBytes[i] = _saltBytes[i];
            }

            for (int i = 0; i < _passBytes.Length; i++)
            {
                fullBytes[_saltBytes.Length + i] = _passBytes[i];
            }
            return fullBytes;
        }

    }
}
