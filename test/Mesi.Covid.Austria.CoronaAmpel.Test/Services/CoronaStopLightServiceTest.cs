using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;
using Mesi.Covid.Austria.CoronaAmpel.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Mesi.Covid.Austria.CoronaAmpel.Test.Services
{
    public class CoronaStopLightServiceTest
    {
        private readonly Mock<ICoronaStopLightRepository> coronaStopLightRepository;
        private readonly Mock<ILogger<CoronaStopLightService>> logger;
        private readonly CoronaStopLightService sut;

        public CoronaStopLightServiceTest()
        {
            coronaStopLightRepository = new Mock<ICoronaStopLightRepository>();
            logger = new Mock<ILogger<CoronaStopLightService>>();
            
            sut = new CoronaStopLightService(coronaStopLightRepository.Object, logger.Object);
        }
        
        [Fact]
        public async void ItShallReturnCommuneData()
        {
            // given
            const string communeId = "123";
            var expected = new CommuneCoronaStopLightStatus("commune", communeId, CoronaStopLightLevel.Green);

            coronaStopLightRepository.Setup(repo => repo.GetByCommuneId(communeId)).ReturnsAsync(expected);
            
            // when
            var actual = await sut.GetCommuneStatus(communeId);
            
            // then
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async void ItShallReturnGreenIfCommuneNotFound()
        {
            // given
            const string communeId = "123";

            coronaStopLightRepository.Setup(repo => repo.GetByCommuneId(communeId)).ReturnsAsync(null as CommuneCoronaStopLightStatus);
            
            // when
            var actual = await sut.GetCommuneStatus(communeId);
            
            // then
            Assert.NotNull(actual);
            Assert.Equal(CoronaStopLightLevel.Green, actual.WarningLevel);
            Assert.Equal(communeId, actual.CommuneId);
        }
    }
}