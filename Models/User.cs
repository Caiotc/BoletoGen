using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoletoGen2.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
        public string Photo { get; set; }
        public string SocialProfile { get; set; }
        public IRType IRType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Int64 FacebookId { get; set; }
        public string DeviceToken { get; set; }
        public UserType UserType { get; set; }
        public List<Permission> Permissions { get; set; }
        public DateTime BirthDate { get; set; }
        public string HouseNumber { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public string Company { get; set; }
        public Guid IdCompany { get; set; }
        public string GoogleId { get; set; }
    }
}