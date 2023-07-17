using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.DAL.Entities
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string Value { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        [ForeignKey("User")]
        public int OwnerId;
        public User Owner { get; set; }
    }
}
