using hw13.Contract;
using hw13.DBContex;
using hw13.Entity;
using hw13.Enum;
using hw13.SeedUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDBContex _appContext;
    public UserRepository()
    {
        _appContext = new AppDBContex();
    }

    public List<User> GetAll()
    {


        var users = _appContext.Users.AsNoTracking().ToList();
        return users;

    }

    public bool Login(string username, string password)
    {

        var isTrue = _appContext.Users.Where(l => l.UserName == username && l.Password == password).AsNoTracking().Any();

        if (isTrue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Register(User user)
    {

        _appContext.Users.Add(user);
        _appContext.SaveChanges();
    }
    public User Get(int id)
    {
        var users = _appContext.Users.Where(t => t.Id == id).FirstOrDefault();
        return users;
    }

    public void Update(User user, int days)
    {
        var users = _appContext.Users.FirstOrDefault(p => p.Id == user.Id);
        users.BorrowLimitEndDate = users.BorrowLimitEndDate.AddDays(days);
        _appContext.SaveChanges();
    }
}
