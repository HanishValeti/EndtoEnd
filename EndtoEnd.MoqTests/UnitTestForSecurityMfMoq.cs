using System.Collections.Generic;
using System.Linq;
using EndtoEnd.Entity;
using EndtoEnd.Repository;
using Moq;
using NUnit.Framework;

namespace EndtoEnd.MoqTests
{
    [TestFixture]
    public class UnitTestForSecurityMfMoq
    {
        [Test]
        public void QueryAllSecuritiesMfNoExceptionMoqTest()
        {
            IList<SecurityMutualFundDto> securityMutualFundDtos = new List<SecurityMutualFundDto>();
                
            var moqsecurityrepository = new Mock<ISecuritiesMfRepository> {CallBase = true};
            moqsecurityrepository.Setup(x => x.GetListSecurityMf())
                .Returns(securityMutualFundDtos.AsQueryable());
            
            moqsecurityrepository.Verify(x=>x.GetListSecurityMf(),Times.Never);

        }

        [Test]
        public void QuerySingleSecuritiesMfNoExceptionMoqTest()
        {
            
            SecurityMutualFundDto securityMutualFundDto = new SecurityMutualFundDto();
            var moqsecurityrepository = new Mock<ISecuritiesMfRepository> { CallBase = true };
            moqsecurityrepository.Setup(x => x.GetSecurityMfBySymbol(It.IsAny<string>()))
                .Returns(securityMutualFundDto);

            securityMutualFundDto = moqsecurityrepository.Object.GetSecurityMfBySymbol("ITRGX");
            Assert.IsNotNull(securityMutualFundDto);
            Assert.IsInstanceOf<SecurityMutualFundDto>(securityMutualFundDto);

            moqsecurityrepository.VerifyAll();

        }
    }
}
