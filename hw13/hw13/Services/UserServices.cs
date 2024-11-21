using hw13.Contract;
using hw13.Entity;
using hw13.Enum;
using hw13.Repository;
using hw13.SeedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Services;

public class UserServices : IUserServices
{
    IUserRepository _userRepository;
    public UserServices()
    {
        _userRepository = new UserRepository();
    }
    public void Login(string username, string password)
    {
        try
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user == null)
            {
                throw new Exception("Invalid username or password.");
            }
            else
            {
                InMemoryDB.CurrentUser = user;

            }

        }
        catch (Exception ex)
        {
            throw new Exception($"Error : {ex.Message}", ex);

        }
    }
    public void Register(string username, string password, RoleUserEnum Role)
    {
        try
        {
            bool isSpecial = password.Any(s => (s >= 33 && s <= 47) || s == 64);

            if (password.Length < 5 || password.Length > 10 || !isSpecial)
            {
                throw new Exception("Password > 4  Char And One Special Character");
            }


            var user = new User
            {
                UserName = username,
                Password = password,
                Role = Role

            };

            _userRepository.Register(user);
        }

        catch (Exception ex)
        {
            throw new Exception($"Error : {ex.Message}", ex);
        }
    }
    public User? GetCurrentUser()
    {
        return InMemoryDB.CurrentUser;
    }

    public List<User> ShowList()
    {
        var users = _userRepository.GetAll()
            .OrderBy(u => u.Id)
            .ToList();

        return users;
    }

    public int EndTime(int id)
    {
        var user = _userRepository.Get(id);
        var endT = user.BorrowLimitEndDate - DateTime.Now;
        return endT.Days;
    }

    public void Charge(int userID, int days)
    {
        try
        {
            if (days ==0)
            {
                throw new Exception("Just Can Enter +Number");
            }
            else if (days<0)
            {
                throw new Exception("Cant Enter -Number");
            }
            
            var user = _userRepository.Get(userID);
            _userRepository.Update(user, days);
        }catch (Exception ex)
        {
            throw new Exception($"Error : {ex.Message}", ex);
        }
       
    }


}
