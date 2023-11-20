using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerMaui.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName{ get; set; }
        public string Email{ get; set; }
        public string Password1 { get; set; }
                
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
                
        public string Token { get; set; }        
    }
}
