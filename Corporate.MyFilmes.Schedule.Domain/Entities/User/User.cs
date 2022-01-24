using Corporate.MyFilmes.Schedule.Domain.Base;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Corporate.MyFilmes.Schedule.Domain.Entities.User
{
    public class User : Entity
    {
        public User()
        {

        }
        public User(Guid Id)
        {
            this.Id = Id;
        }

        public User(string login, string email,
                    string password)
        {
           
            Login = login;
            Email = email;
            Password = password;          
            SetPasswordHash();
        }
       
        public string Role { get; set; }      
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }  
       
        public void SetPasswordHash()
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(Password));
                Password = Encoding.ASCII.GetString(result);
            }
        }
        public void UpdatePassword(string newPassword)
        {
            this.Password = newPassword;
            SetPasswordHash();
        }
        public bool ValidatePassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                password = Encoding.ASCII.GetString(result);

                return password.Equals(Password);
            }
        }
    }
}
