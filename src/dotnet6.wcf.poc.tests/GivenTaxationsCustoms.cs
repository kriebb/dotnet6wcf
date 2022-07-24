using System.ComponentModel;
using System.Threading.Tasks;
using dotnet6.wcf.poc.Soap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace dotnet6.wcf.poc.tests
{
    public class GivenTaxationsCustoms
    {
        protected const string Url = "https://ec.europa.eu/taxation_customs/tin/services/checkTinService.wsdl";
        public class GivenTheService: GivenPublications
        {
            private readonly ITestOutputHelper _testOutputHelper;

            public GivenTheService(ITestOutputHelper testOutputHelper)
            {
                _testOutputHelper = testOutputHelper;
            }
            [Fact]
            [Category("IntegrationTests")]
            public async Task Test1()
            {
                var configuratonBuilder = new ConfigurationBuilder();
                var configuration = configuratonBuilder.Build();

                var serviceCollection = new ServiceCollection();
                serviceCollection.AddSoapClient(configuration);
                serviceCollection.AddLogging(builder => builder.AddXunit(_testOutputHelper));
                var provider = serviceCollection.BuildServiceProvider(true);

                var soapClient = provider.GetRequiredService<ISoapServiceClient>();

                checkTinRequest input = new checkTinRequest();

                input.countryCode = "BE";
                input.tinNumber = "71.09.07–213–64"; //https://www.quora.com/What-do-the-11-digits-in-a-Belgian-TIN-tax-identification-number-specify-Do-each-of-them-mean-something
                var response = await soapClient.ExecuteAsync<checkTinPortType, checkTinRequest, checkTinResponse?>(CheckTin, input, GivenTaxationsCustoms.Url);

                _testOutputHelper.WriteLine(JsonConvert.SerializeObject(response));

            }

            private async Task<checkTinResponse> CheckTin(checkTinPortType arg1, checkTinRequest arg2)
            {
                return await arg1.checkTinAsync(arg2);

            }
        }

    }
}