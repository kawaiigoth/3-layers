using _EPAM_UserList.BLL.UserLogic;
using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace _EPAM_UserList.PL.WebUI
{
    public class Engine
    {
        public static byte[] Crypt(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return provider.ComputeHash(encoding.GetBytes(password));
        }

        public static bool CanLogin(string login, byte[] password)
        {
            AppUserLogic userlogic = new AppUserLogic();
            AppRoleLogic rolelogic = new AppRoleLogic();
            var user = userlogic.Get(login);
            var invader = new AppUserDTO(login, password);
            if(user.Password.SequenceEqual(invader.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}