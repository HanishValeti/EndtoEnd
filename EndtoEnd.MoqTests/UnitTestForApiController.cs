using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using EndtoEnd.Controllers;
using EndtoEnd.Entity;
using EndtoEnd.Repository;
using Moq;
using NUnit.Framework;

namespace EndtoEnd.MoqTests
{
    [TestFixture]
    public class UnitTestForApiController
    {
        [Test]
        public void ApiAllSecuritiesMfNoExceptionMoqTest()
        {
            var moqsecurityrepository = new Mock<ISecuritiesMfRepository>();
            IQueryable<SecurityMutualFundDto> securityMutualFundDtos = GetsecuritiesMfsList().AsQueryable();
            moqsecurityrepository.Setup(x => x.GetListSecurityMf()).Returns(securityMutualFundDtos.AsQueryable());
            var apicontroller = new SecuritiesWebApiMfController(moqsecurityrepository.Object);

            var secmfs = apicontroller.GetIQueryable();
            Assert.AreEqual(secmfs.Count(),securityMutualFundDtos.Count());
            Assert.IsNotNull(secmfs);
            Assert.IsInstanceOf<IQueryable<SecurityMutualFundDto>>(secmfs);

            moqsecurityrepository.VerifyAll();
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApiAllSecuritiesMfExceptionExpectedMoqTest()
        {
            var moqsecurityrepository = new Mock<ISecuritiesMfRepository>();
            IQueryable<SecurityMutualFundDto> securityMutualFundDtos = GetsecuritiesMfsList().AsQueryable();
            moqsecurityrepository.Setup(x => x.GetListSecurityMf()).Throws<ArgumentNullException>();
            var apicontroller = new SecuritiesWebApiMfController(moqsecurityrepository.Object);

            var secmfs = apicontroller.GetIQueryable();
            Assert.IsInstanceOf<IQueryable<SecurityMutualFundDto>>(secmfs);

            moqsecurityrepository.VerifyAll();
        }

        [Test]
        public void ApiSingleSecurityMfNoExceptionMoqTest()
        {
            var moqsecurityrepository = new Mock<ISecuritiesMfRepository>();
            SecurityMutualFundDto securityMutualFundDto = GetsecuritiesMfsList().FirstOrDefault();
            moqsecurityrepository.Setup(x => x.GetSecurityMfBySymbol("Demo1")).Returns(securityMutualFundDto);
            var apicontroller = new SecuritiesWebApiMfController(moqsecurityrepository.Object);

            var secmf = apicontroller.GetobjSecurityMutualFundDto("Demo1");
            if (securityMutualFundDto != null) 
            Assert.AreEqual(secmf.Id, securityMutualFundDto.Id);
            Assert.IsNotNull(secmf);
            Assert.IsInstanceOf<SecurityMutualFundDto>(secmf);

            moqsecurityrepository.VerifyAll();
        }

        [Test]
        public void ApiAllSecuritiesMfNoExceptionHttpResponseMessageMoqTest()
        {
            var moqsecurityrepository = new Mock<ISecuritiesMfRepository>();
            IQueryable<SecurityMutualFundDto> securityMutualFundDtos = GetsecuritiesMfsList().AsQueryable();
            moqsecurityrepository.Setup(x => x.GetListSecurityMf()).Returns(securityMutualFundDtos.AsQueryable());
            var apicontroller = new SecuritiesApiMfController(moqsecurityrepository.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };


            var response = apicontroller.Get();
            IQueryable<SecurityMutualFundDto> testsecurityMutualFundDtos;
            Assert.IsTrue(response.TryGetContentValue<IQueryable<SecurityMutualFundDto>>(out testsecurityMutualFundDtos));

            Assert.AreEqual(testsecurityMutualFundDtos.Count(), securityMutualFundDtos.Count());
            Assert.IsNotNull(testsecurityMutualFundDtos);
            Assert.IsInstanceOf<IQueryable<SecurityMutualFundDto>>(testsecurityMutualFundDtos);

            moqsecurityrepository.VerifyAll();
        }

        [Test]
        public void ApiSingleSecurityMfNoExceptionHttpResponseMessageMoqTest()
        {
            var moqsecurityrepository = new Mock<ISecuritiesMfRepository>();
            SecurityMutualFundDto securityMutualFundDto = GetsecuritiesMfsList().FirstOrDefault();

            moqsecurityrepository.Setup(x => x.GetSecurityMfBySymbol("Demo1")).Returns(securityMutualFundDto);

            var apicontroller = new SecuritiesApiMfController(moqsecurityrepository.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = apicontroller.Get("Demo1");
            SecurityMutualFundDto testsecurityMutualFundDto;
            Assert.IsTrue(response.TryGetContentValue<SecurityMutualFundDto>(out testsecurityMutualFundDto));

            Assert.IsNotNull(securityMutualFundDto);
            Assert.AreEqual(testsecurityMutualFundDto.Id, securityMutualFundDto.Id);
            Assert.IsInstanceOf<SecurityMutualFundDto>(testsecurityMutualFundDto);

            moqsecurityrepository.VerifyAll();

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
                }
                
            };

            return testsecuritiesMfs;
        }
    }
}
