using System;
using System.Security.Cryptography;
using System.Text;

namespace Cuadrantes.Security
{
    public class Common
    {
        public string GenerateSHA256(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            SHA256Managed sHA256 = new SHA256Managed();
            byte[] hash = sHA256.ComputeHash(bytes);

            string hashString = string.Empty;
            foreach (byte x in hash)
                hashString += string.Format("{0:x2}", x);

            return hashString;
        }

        #region Rindjael

        public string GenerateKey()
        {
            SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged();
            symmetricAlgorithm.KeySize = 128;
            symmetricAlgorithm.GenerateKey();

            return Convert.ToBase64String(symmetricAlgorithm.Key);
        }


        public string Encrypt(string message, string key)
        {
            SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged();
            symmetricAlgorithm.KeySize = 128;
            symmetricAlgorithm.Key = Convert.FromBase64String(key);
            symmetricAlgorithm.Mode = CipherMode.ECB;

            ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor();

            Byte[] data = Encoding.UTF8.GetBytes(message);

            var dataEncrypted = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(dataEncrypted);
        }


        public string Decrypt(string message, string key)
        {
            SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged();
            symmetricAlgorithm.KeySize = 128;
            symmetricAlgorithm.Key = Convert.FromBase64String(key);
            symmetricAlgorithm.Mode = CipherMode.ECB;

            ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor();

            Byte[] data = Convert.FromBase64String(message);
            Byte[] dataDecrypted = cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            return Encoding.UTF8.GetString(dataDecrypted);
        }
        #endregion
    }
}
