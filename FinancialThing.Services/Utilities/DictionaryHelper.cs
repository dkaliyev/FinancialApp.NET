using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;
using FinancialThing.Configuration;
using FinancialThing.DataAccess;
using FinancialThing.Models;

namespace FinancialThing.Services.Utilities
{
    public class DictionaryHelper
    {
        public static void InsertDicionaries(IRepository<Dictionary, Guid> repo)
        {
            var data = DataMappingConfiguration.Instance.Pages;
            foreach (var page in data)
            {
                repo.Add(new Dictionary()
                {
                    Code = page.Code,
                    DisplayName = page.DisplayName,
                    Order = page.Order,
                    ParentCode = "FI"
                });
                foreach (var section in page.Sections)
                {
                    repo.Add(new Dictionary()
                    {
                        Code = section.Code,
                        DisplayName = section.DisplayName,
                        Order = section.Order,
                        ParentCode = page.Code
                    });
                    foreach (var datum in section.Data)
                    {
                        repo.Add(new Dictionary()
                        {
                            Code = datum.Code,
                            DisplayName = datum.DisplayName,
                            Order = datum.Order,
                            ParentCode = section.Code
                        });
                    }
                }
            }
            
        }
    }
}