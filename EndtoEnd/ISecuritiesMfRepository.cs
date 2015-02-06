using System.Linq;
using EndtoEnd.Entity;
using EndtoEnd.Models;

namespace EndtoEnd
{
    public interface ISecuritiesMfRepository
    {
        SecurityMutualFundViewModel GetSecurityMf(string symbol);
        IQueryable<SecurityMutualFundViewModel> GetListSecurityMf();
    }
}