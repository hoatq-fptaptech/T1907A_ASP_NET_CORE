using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T1907A.Models;
using T1907A.Context;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T1907A.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private DataContext _context = new DataContext();
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Home()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{token}")]
        public LoginResult Get(string token)
        {
            Token t = _context.Tokens.Where(e => e.StrToken.Equals(token)).FirstOrDefault();
            User u = _context.Users.Find(t.UserId);
            if (u == null)
            {
                return new LoginResult() { Status = false, Message = "Login fail" };
            }
            return new LoginResult() { Status = true, Message = "User info", Token = t.StrToken, User = u };
        }

        // POST api/<LoginController>
        [HttpPost]
        public LoginResult Post([FromBody] LoginModel login)
        {
            User u = _context.Users.Where(us =>  us.UserName.Equals(login.UserName) && us.Pwd.Equals(login.Pwd)).FirstOrDefault();   
            if(u == null)
            {
                return new LoginResult() { Status = false,Message="Login fail" };
            }
            string token = new Random().NextDouble().ToString();
            DateTime dt = new DateTime().AddMinutes(120);
            token += dt.GetHashCode();
            Token t = new Token { StrToken = token, UserId = u.Id, ExpireAt = dt.ToShortTimeString() };
            _context.Tokens.Add(t);
            _context.SaveChanges();
            return new LoginResult() { Status = true, Message = "Login success", Token = t.StrToken, User = u };
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // PUT api/<LoginController>/5
        [HttpPut("{token}")]
        public LoginResult ChangePwd(string token, [FromBody] ChangePwd change)
        {
            if (!change.ConfirmPwd.Equals(change.NewPwd))
                return new LoginResult() { Status = false, Message = "Mật khẩu mới không khớp với xác nhận mật khẩu" };
            LoginResult rs = Get(token);
            if (rs.Status && rs.User.Pwd.Equals(change.Current))
            {
                // thay doi mat khau
                User u = _context.Users.Find(rs.User.Id);
                u.Pwd = change.NewPwd;
                _context.Entry(u).State = EntityState.Modified;
                _context.SaveChanges();
                rs.User = u;
                return rs;
            }
            else
            {
                return new LoginResult() { Status = false, Message = "Mật khẩu hiện tại không đúng!" };
            }
        }
    }
}
