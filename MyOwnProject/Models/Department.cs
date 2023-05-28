﻿namespace MyOwnProject.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentAddress { get; set; }
        public int CompanyId { get; set; }
        public Company ? Company { get; set; }
    }
}