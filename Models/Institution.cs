using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoletoGen2.Models
{
    public class Institution
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SmallDescription { get; set; }

        public string LargeDescription { get; set; }

        public string CNPJ { get; set; }

        public string Adress { get; set; }

        public string AdressComplement { get; set; }

        public string City { get; set; }

        public string ZIP { get; set; }

        public string Neighborhood { get; set; }

        public string Country { get; set; }

        public int YearFounded { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Guid CategoryId { get; set; }

        public string Logo { get; set; }

        public Guid? ResponsibleUserId { get; set; }

        public User ResponsibleUser { get; set; }

        public string WebSite { get; set; }

        public string FacebookProfile { get; set; }

        public string TwitterProfile { get; set; }

        public string InstagramProfile { get; set; }

        public string PinterestProfile { get; set; }

        public int InboundType { get; set; }

        public string InboundAnswer { get; set; }

        public double ValueRaisedLastYear { get; set; }

        public string Mission { get; set; }

        public string MainFoundingSource { get; set; }

        public double ValueSpentLastYear { get; set; }

        public bool AcceptedTerms { get; set; }

        public bool AcceptedResponsabilityTerms { get; set; }

        public string StateDocument { get; set; }

        public string GovernmentDocument { get; set; }

        public string CEBASDocument { get; set; }

        public int BankCode { get; set; }

        public int BankAgency { get; set; }

        public int BankAgencyDigit { get; set; }

        public int BankAccount { get; set; }

        public int BankAccountDigit { get; set; }

        public bool Approved { get; set; }

        public bool Active { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? AlteredBy { get; set; }

        public DateTime? AlteredAt { get; set; }

        public InstitutionType InstitutionType { get; set; }
        public Category Category { get; set; }

    }
}