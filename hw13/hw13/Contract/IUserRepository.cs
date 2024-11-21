using hw13.Entity;
using hw13.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Contract;

public interface IUserRepository
{
    public bool Login(string username, string password);
    public void Register(User user);

    public List<User> GetAll();
    public User Get(int id);
    public void Update(User user,int days);
}
