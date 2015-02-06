using System;
using System.Collections.Generic;
using System.Linq;
using EndtoEnd.Entity;
using EndtoEnd.Repository;
using NUnit.Framework;

namespace EndtoEnd.Tests
{
    //Integration unit tests
    [TestFixture]
    public class UnitTestForSecurityMf
    {
        private ISecuritiesMfRepository _repository;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _repository = new SecuritiesMfRepository();
        }
        
        [Test]
        public void QueryAllSecuritiesMfNoExceptionTest()
        {
            var slist = _repository.GetListSecurityMf();
            Assert.IsInstanceOf<IQueryable<SecurityMutualFundDto>>(slist);
        }

        [Test]
        public void QuerySingleSecurityMfNoExceptionTest()
        {
            var list = _repository.GetSecurityMfBySymbol("FCNTX");
            Assert.That(list.Id, Is.EqualTo(2153));
            Assert.That(list.MorningStarRating, Is.EqualTo(4));
            Assert.IsInstanceOf<SecurityMutualFundDto>(list);
            
        }

        [Test]
        public void DeleteSingleSecurityMfNoExceptionTest()
        {
            var status = _repository.DeleteSecurityMfData(2153);
            Assert.AreEqual(status.Status,true);
        }
        [Test]
        public void InsertSingleSecurityMfNoExceptionTest()
        {
            var testsecuritiesMf = GetsecuritiesMfsList();
            var status = _repository.InsertSecurityMfData(testsecuritiesMf.FirstOrDefault());
            Assert.AreEqual(status.Status, true);
        }

        [Test]
        public void UpdateSingleSecurityMfNoExceptionTest()
        {
            var testsecurityMf = GetsecuritiesMfsList().Where(x => x.Id == 2206);
            var status = _repository.UpdateSecurityMf(testsecurityMf.FirstOrDefault());
            Assert.AreEqual(status.Status,true);
        }

        private List<SecurityMutualFundDto> GetsecuritiesMfsList()
        {
            var testsecuritiesMfs = new List<SecurityMutualFundDto>
            {
                new SecurityMutualFundDto
                {
                    Id = 1,
                    Symbol = "Demo1",
                    MorningStarRating = 4,
                    Company = "DemoCompany1",
                    PercentChange = -0.46m,
                    Shares = 0.00m,
                    RetrievalDateTime = DateTime.Now
                },
                new SecurityMutualFundDto
                {
                    Id = 2,
                    Symbol = "Demo2",
                    MorningStarRating = 4,
                    Company = "DemoCompany2",
                    PercentChange = -0.46m,
                    Shares = 0.00m,
                    RetrievalDateTime = DateTime.Now
                },
                new SecurityMutualFundDto
                {
                    Id = 2206,
                    Symbol = "Demo2",
                    MorningStarRating = 4,
                    Company = "DemoCompany2",
                    PercentChange = -0.46m,
                    Shares = 0.00m,
                    RetrievalDateTime = DateTime.Now
                }
                
            };

            return testsecuritiesMfs;
        }
    }
}
