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
        public static void SetUserId(int userId)
        {
            UserId = userId;
        }
    }
}
