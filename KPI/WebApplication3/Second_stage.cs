using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class Second_stage
    {
        public int id_second_stage { get; set; }

        public bool? active { get; set; }

        public string name { get; set; }

        public int fk_first_stage { get; set; }

    }

}