﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsInProccessing { get; set; }
        public string InstagramLogin  { get; set; }
        public string InstagramPassword { get; set; }

        public ICollection<TaskAction> Actions { get; set; }
    }
}
