using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class Third_stage
    {
        public int id_third_stage { get; set; }

        public bool active { get; set; }

        public string name { get; set; }

        public int fk_second_stage { get; set; }

    }
}