using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    [Table("UserInfo")]
    public class UserEntity : BaseEntity
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDay { get; set; }

        public string TelNumber { get; set; }

        public string NickName { get; set; }

        public string Bio { get; set; }


    }
}
