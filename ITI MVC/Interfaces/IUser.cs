namespace ITI_MVC.Interfaces
{
    public interface IUser
    {
        void Create(string Name, string Email, string Password);
        bool Login(string UserName, string Password);
    }
}
