using System.ComponentModel.DataAnnotations;

namespace BdConectionCore.BDModels
{
    public class Accounts
    {
        [Key]
        public int Accountid { get; set; }
        public string AccountName { get; set; }
        public string ClientName { get; set; }
        public string OperatorName { get; set; }
        public int Teamid { get; set; }
    }
}
