using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole
{
    public class PassHash
    {


        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        public static readonly int DefaultSaltSize = 8; // 64-bit salt
        public readonly byte[] Salt;
        public readonly byte[] Passhash;

        internal PassHash(byte[] salt, byte[] passhash)
        {
            Salt = salt;
            Passhash = passhash;
        }

        public override string ToString()
        {
            return string.Format("{{'salt': '{0}', 'passhash': '{1}'}}",
                                 Convert.ToBase64String(Salt),
                                 Convert.ToBase64String(Passhash));
        }

        public static PassHash Encode<HA>(string password) where HA : HashAlgorithm
        {
            return Encode<HA>(password, DefaultSaltSize);
        }

        public static PassHash Encode<HA>(string password, int saltSize) where HA : HashAlgorithm
        {
            return Encode<HA>(password, GenerateSalt(saltSize));
        }

        private static PassHash Encode<HA>(string password, byte[] salt) where HA : HashAlgorithm
        {

            BindingFlags publicStatic = BindingFlags.Public | BindingFlags.Static;
            MethodInfo hasher_factory = typeof(HA).GetMethod("Create", publicStatic, Type.DefaultBinder, Type.EmptyTypes, null);

            using HashAlgorithm hasher = (HashAlgorithm)hasher_factory.Invoke(null, null);

            using MemoryStream hashInput = new MemoryStream();

            hashInput.Write(salt, 0, salt.Length);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            hashInput.Write(passwordBytes, 0, passwordBytes.Length);
            hashInput.Seek(0, SeekOrigin.Begin);
            byte[] passhash = hasher.ComputeHash(hashInput);

            return new PassHash(salt, passhash);
        }

        private static byte[] GenerateSalt(int saltSize)
        {
            // This generates salt.
            // Rephrasing Schneier:
            // "salt" is a random string of bytes that is
            // combined with password bytes before being
            // operated by the one-way function.
            byte[] salt = new byte[saltSize];
            _rng.GetBytes(salt);
            return salt;
        }

        public static bool Verify<HA>(string password, byte[] salt, byte[] passhash) where HA : HashAlgorithm
        {
            // OMG: I don't know how to compare byte arrays in C#.
            return Encode<HA>(password, salt).ToString() == new PassHash(salt, passhash).ToString();
        }

    }
}
