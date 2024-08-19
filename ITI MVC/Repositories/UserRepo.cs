using ITI_MVC.Context;
using ITI_MVC.Interfaces;
using ITI_MVC.Models;
using Microsoft.CodeAnalysis.Elfie.Extensions;

namespace ITI_MVC.Repositories
{
    public class UserRepo : IUser
    {
        ITIContext db = new ITIContext();

        public void Create(string Name, string Email, string Password)
        {
            string password = Password.ToSHA256String();
            User user = new User() { Name = Name, Password = password, Email = Email };
            db.Add(user);
            db.SaveChanges();
        }

        public bool Login(string Name, string Password) 
        {
            User user = db.Users.SingleOrDefault(u => u.Name == Name);
            string password = Password.ToSHA256String();
            if (user != null && user.Password == password)
            {
                return true;
            }
            return false;
        }
    }
}
