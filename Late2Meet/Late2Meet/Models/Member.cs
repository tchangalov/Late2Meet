using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Late2Meet.Models
{
    public class Member
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
