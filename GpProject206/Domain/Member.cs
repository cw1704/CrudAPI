using System;
using System.ComponentModel.DataAnnotations;

namespace GpProject206.Domain
{
    public class Member : AMongoEntity
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { private get; set; }
        public int Point { get; private set; } = 0;
        public bool IsAdmin { get; private set; } = false;

        public void SetPoint(int pt) => Point = pt;
        public bool VerifyPassword(string input) => input == Password;
        public void Modify(Member m)
        { 
            FristName = m.FristName;
            LastName = m.LastName;            
            Password = m.Password;
            Point = m.Point;
        }
        public void Modify(MemberUpdateObject m)
        {
            FristName = m.FristName;
            LastName = m.LastName;
            Password = m.NewPassword;
        }
    }

    public class MemberUpdateObject
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
    }
    public class LoginObject
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}