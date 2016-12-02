using System.Linq;
using FinancialThing.Models;

namespace FinancialThing.Utilities
{
    public class CompanyDataMerger: IDataMerger<Company>
    {
        public void Merge(Company oldCompany, Company newCompany)
        {
            oldCompany.FullName = newCompany.FullName;
            if (oldCompany.Financials == null)
            {
                oldCompany.Financials = newCompany.Financials;
                oldCompany.Financials.Company = oldCompany;
                return;
            }
            foreach (var page in newCompany.Financials.Pages)
            {
                var srcPage = oldCompany.Financials.Pages.FirstOrDefault(p => p.Dictionary.Code == page.Dictionary.Code);
                foreach (var statement in page.Statements)
                {
                    var srcStatement =
                        srcPage.Statements.FirstOrDefault(s => s.Dictionary.Code == statement.Dictionary.Code);
                    foreach (var datum in statement.Data)
                    {
                        var srcDatum = srcStatement.Data.FirstOrDefault(d => d.Dictionary.Code == datum.Dictionary.Code);
                        foreach (var value in datum.Values)
                        {
                            var srcValue = srcDatum.Values.FirstOrDefault(v => v.Year == value.Year);
                            if (srcValue==null)
                            {
                                srcDatum.Values.Add(value);
                            }
                            else
                            {
                                srcValue.DataValue = value.DataValue;
                            }
                        }
                    }
                }
            }
        }
    }
}