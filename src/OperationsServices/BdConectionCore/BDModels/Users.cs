using System;
using System.ComponentModel.DataAnnotations;

namespace BdConectionCore.BDModels
{
    public class Users
    {
        [Key]
        public int Userid { get; set; }
        public string FullName { get; set; }
        public string EnglishLevel { get; set; }
        public string KnowlEdge { get; set; }
        public DateTime InsertDate { get; set; }
        public string UrlResume { get; set; }
        public bool isActive { get; set; }
        public int Roleid { get; set; }
        public string Email { get; set; }
        //[NotMapped]
        public string Salt { get; set; }
        //[NotMapped]
        public string Password { get; set; }
        public string UserLogin { get; set; }

    }
}
