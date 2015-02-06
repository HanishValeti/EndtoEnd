using System.Linq;
using EndtoEnd.Entity;

namespace EndtoEnd.Repository
{
    public interface ISecuritiesMfRepository
    {
        SecurityMutualFundDto GetSecurityMfBySymbol(string symbol);
        SecurityMutualFundDto GetSecurityMfById(int id);
        IQueryable<SecurityMutualFundDto> GetListSecurityMf();
        OperationStatus UpdateSecurityMf(SecurityMutualFundDto updateSecurityMutualFundDto);
        OperationStatus InsertSecurityMfData(SecurityMutualFundDto insertSecurityMutualFundDto);
        OperationStatus DeleteSecurityMfData(int id);
        
    }
}