using System.Security.Cryptography;
using System.Text;

namespace JwtAuth.BLL.Utilities.CryptoService
{
    public class CryptoService : ICryptoService
    {
        public void Hash(string value, out byte[] valueHash, out byte[] valueSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                valueSalt = hmac.Key;
                valueHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        public bool Compare(string value, byte[] valueHash, byte[] valueSalt)
        {
            using (var hmac = new HMACSHA512(valueSalt))
            {
                var computedValueHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
                if (computedValueHash.SequenceEqual(valueHash))
                    return true;
                return false;
            }
        }
    }
}