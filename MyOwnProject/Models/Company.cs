using System.ComponentModel.DataAnnotations;

namespace MyOwnProject.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public byte[] CompanyLogo { get; set; }
    }
}
