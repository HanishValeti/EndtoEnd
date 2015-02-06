using System;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using EndtoEnd.Entity;

namespace EndtoEnd.Repository
{
    public class SecuritiesMfRepository : RepositoryBase<AccountsAtAGlanceEntities>, ISecuritiesMfRepository
    {
        public SecurityMutualFundDto GetSecurityMfBySymbol(string symbol)
        {
            var listmf = (from sec in GetList<Security>()
                          join secmf in GetList<Securities_MutualFund>()
                    on sec.Id equals secmf.Id
                    where sec.Symbol == symbol
                    select new SecurityMutualFundDto
                    {
                        Id = sec.Id,
                        Symbol = sec.Symbol,
                        MorningStarRating = secmf.MorningStarRating,
                        Company = sec.Company,
                        PercentChange = sec.PercentChange,
                        Shares = sec.Shares,
                        RetrievalDateTime = sec.RetrievalDateTime
                    }).SingleOrDefault();
            return listmf;
        }

        public SecurityMutualFundDto GetSecurityMfById(int id)
        {
            var listmf = (from sec in GetList<Security>()
                          join secmf in GetList<Securities_MutualFund>()
                    on sec.Id equals secmf.Id
                          where sec.Id == id
                          select new SecurityMutualFundDto
                          {
                              Id = sec.Id,
                              Symbol = sec.Symbol,
                              MorningStarRating = secmf.MorningStarRating,
                              Company = sec.Company,
                              PercentChange = sec.PercentChange,
                              Shares = sec.Shares,
                              RetrievalDateTime = sec.RetrievalDateTime
                          }).SingleOrDefault();
            return listmf;
        }

        public IQueryable<SecurityMutualFundDto> GetListSecurityMf()
        {
            var listmf = (from sec in GetList<Security>()
                          join secmf in GetList<Securities_MutualFund>()
                              on sec.Id equals secmf.Id
                          select new SecurityMutualFundDto
                          {
                              Id = sec.Id,
                              Symbol = sec.Symbol,
                              MorningStarRating = secmf.MorningStarRating,
                              Company = sec.Company,
                              PercentChange = sec.PercentChange,
                              Shares = sec.Shares,
                              RetrievalDateTime = sec.RetrievalDateTime
                          });
            return listmf;
        }

        public OperationStatus UpdateSecurityMf(SecurityMutualFundDto updateSecurityMutualFundDto)
        {
            var opStatus = new OperationStatus { Status = false };
            if (updateSecurityMutualFundDto != null)
            {
                //var securityfromdb = GetList<Security>().SingleOrDefault(s=>s.Id == updateSecurityMutualFundDto.Id);
                var securitymffromdb = GetList<Securities_MutualFund>().Include(s=>s.Security).SingleOrDefault(s => s.Id == updateSecurityMutualFundDto.Id);
                
                if (securitymffromdb != null)
                {
                    securitymffromdb.Security.PercentChange = updateSecurityMutualFundDto.PercentChange;
                    securitymffromdb.Security.Shares = updateSecurityMutualFundDto.Shares;
                    securitymffromdb.Security.Symbol = updateSecurityMutualFundDto.Symbol;
                    securitymffromdb.Security.Company = updateSecurityMutualFundDto.Company;
                    securitymffromdb.Security.RetrievalDateTime = updateSecurityMutualFundDto.RetrievalDateTime;
                    securitymffromdb.MorningStarRating = updateSecurityMutualFundDto.MorningStarRating;
                    DataContext.Entry(securitymffromdb).State = EntityState.Modified;
                }
                try
                {
                    opStatus = Save<Securities_MutualFund>(securitymffromdb);
                }
                catch (Exception exp)
                {
                    return OperationStatus.CreateFromException("Error updating security quote.", exp);
                }
            }
            else
            {
                opStatus.Status = false;
            }
            return opStatus;
        }

        public OperationStatus InsertSecurityMfData(SecurityMutualFundDto insertSecurity)
        {
            var optStatus = new OperationStatus { Status = false };
            if (insertSecurity != null)
            {
                Security sec = new Security();
                {
                    sec.Change = 0.00m;
                    sec.Company = insertSecurity.Company;
                    sec.Last = 0.00m;
                    sec.PercentChange = insertSecurity.PercentChange;
                    sec.RetrievalDateTime = insertSecurity.RetrievalDateTime;
                    sec.Shares = insertSecurity.Shares;
                    sec.Symbol = insertSecurity.Symbol;
                }

                Securities_MutualFund secmf = new Securities_MutualFund();
                {
                    secmf.MorningStarRating = insertSecurity.MorningStarRating;
                    secmf.Security = sec;

                };
                using (var ts = new TransactionScope())
                {
                    optStatus = Add<Securities_MutualFund>(secmf);
                    ts.Complete();
                }
            }
            else
            {
                optStatus.Status = false;
            }

            return optStatus;
        }

        public OperationStatus DeleteSecurityMfData(int id)
        {
            OperationStatus optStatus = new OperationStatus{Status = false};
            {
                var deletesecmf = GetList<Securities_MutualFund>(x => x.Id == id).SingleOrDefault();
                using (var ts = new TransactionScope())
                {
                    if (deletesecmf != null)
                    { 
                        optStatus = Delete<Securities_MutualFund>(deletesecmf);
                        optStatus.RecordsAffected = id;
                    }
                    ts.Complete();
                }
            }

            return optStatus;
        }
        
    }
}