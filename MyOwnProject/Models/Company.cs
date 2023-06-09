﻿using System.ComponentModel.DataAnnotations;

namespace MyOwnProject.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string ? CompanyLogo { get; set; }
        public IEnumerable<Department> ? Departments { get; set; }
    }
}
