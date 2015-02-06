using System.Linq;
using EndtoEnd.Entity;

namespace EndtoEnd
{
    public interface ISecuritiesRepository
    {
        Security GetSecurity(string symbol);
        IQueryable<Security> GetListSecurity();
    }
}