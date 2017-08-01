using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class CompaniesController
    {
        public static List<Company> GetAllCompanies()
        {
            return Database.Companies.GetAllCompanies();
        }

        private static Company AddUpdateCompany(int id, string name, string address, string OIB)
        {
            return Database.Companies.AddUpdateCompany(id, name, address, OIB);
        }

        public static Company AddUpdateCompany(Company company)
        {
            return Database.Companies.AddUpdateCompany(company.Id, company.Name, company.Address, company.OIB);
        }

        public static void DeleteCompany(Company selectedCompany)
        {
            Database.Companies.DeleteCompany(selectedCompany.Id);
        }
    }
}
