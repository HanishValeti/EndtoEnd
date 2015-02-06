using System.Linq;
using EndtoEnd.Entity;

namespace EndtoEnd
{
    public class SecuritiesRepository : RepositoryBase<AccountsAtAGlanceEntities>, ISecuritiesRepository
    {
        public Security GetSecurity(string symbol)
        {
            return DataContext.Securities.SingleOrDefault(s => s.Symbol == symbol);
            
        }

        public IQueryable<Security> GetListSecurity()
        {
            return DataContext.Securities.Select(s => s);
        }
    }
}