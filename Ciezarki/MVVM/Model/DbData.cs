using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciezarki.MVVM.Model
{
    static class DbData
    {
        public static int UserId { get; set; } = 1;
        //DbData.UserId - aby pobrać id obecnie zalogowanego użytkownika
        public static void SetUserId(int userId)
        {
            UserId = userId;
        }
    }
}
