using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace exp15
{
    public class Contact
  {
        [PrimaryKey, AutoIncrement]
        public int    ID        { get; set; }
        public string Name      { get; set; }
        public string LastName  { get; set; }
        public string Telephone { get; set; }
  }
}
