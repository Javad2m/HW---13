using hw13.Entity;
using hw13.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Contract;

public interface IUserServices
{
    public void Login(string username, string password);
    public void Register(string username, string password, RoleUserEnum Role);
    public User? GetCurrentUser();
    public List<User> ShowList();
    public int EndTime(int id);

    public void Charge(int userID, int days);
}
