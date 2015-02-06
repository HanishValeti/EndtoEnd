using System.Linq;
using EndtoEnd.Entity;
using EndtoEnd.Models;

namespace EndtoEnd
{
    public class SecuritiesMfRepository : RepositoryBase<AccountsAtAGlanceEntities>, ISecuritiesMfRepository
    {
        public SecurityMutualFundViewModel GetSecurityMf(string symbol)
        {
            var listmf = (from sec in DataContext.Securities
                join secmf in DataContext.Securities_MutualFund
                    on sec.Id equals secmf.Id
                    where sec.Symbol == symbol
                    select new SecurityMutualFundViewModel
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

        public IQueryable<SecurityMutualFundViewModel> GetListSecurityMf()
        {
            var listmf = (from sec in DataContext.Securities
                          join secmf in DataContext.Securities_MutualFund
                              on sec.Id equals secmf.Id
                          select new SecurityMutualFundViewModel
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
    }
}