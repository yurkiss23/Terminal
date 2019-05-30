using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Models;

namespace Terminal.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static implicit operator User(UserModel u)
        {
            return new User
            {
                Id = u.Id,
                Money = u.Money,
                Fname = u.Fname,
                Lname = u.Lname,
                Phone = u.Phone,
                Email = u.Email,
                Password = u.Password
            };
        }
    }
}
