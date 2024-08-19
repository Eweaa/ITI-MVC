using ITI_MVC.Enum;

namespace ITI_MVC.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleType { get; set; }
        public virtual List<User>? Users{ get; set; }
    }
}
