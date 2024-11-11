using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrapApp
{
    public class User
    {  
        public int Userid {  get; set; }
        public required string? Username { get; set; } 
        public int Score { get; set; }
        public bool IsAdmin { get; set; }
        public int Health { get; set; }
        public int InventoryId { get; set; }
    }
}
