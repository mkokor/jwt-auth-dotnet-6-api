using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Utilities.CryptoService
{
    public interface ICryptoService
    {
        public void Hash(string value, out byte[] valueHash, out byte[] valueSalt);

        public bool Compare(string value, byte[] valueHash, byte[] valueSalt);
    }
}