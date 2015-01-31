using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class Users
    {
        public int id_users { get; set; }

        public bool active { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public int fk_first_stage { get; set; }

        public int fk_second_stage { get; set; }

        public int fk_third_stage { get; set; }

        public int fk_fourth_stage { get; set; }

        public int fk_fifth_stage { get; set; }

    }

}