using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.DBModels
{
    public class CompaniesComparer : IEqualityComparer<Company>
    {
        public bool Equals(Company x, Company y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            return x.name == y.name;
        }
        public int GetHashCode(Company company)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(company, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = company.name == null ? 0 : company.name.GetHashCode();



            //Calculate the hash code for the product.
            return hashProductName;
        }
    }
}
