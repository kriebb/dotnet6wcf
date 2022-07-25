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
    public class GivenPublications
    {
        protected const string Url = "https://www.ebi.ac.uk/europepmc/webservices/soap";
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

                searchPublicationsRequest input = new searchPublicationsRequest();

                  input.queryString = "OPEN_ACCESS:y HAS_UNIPROT:y HAS_REFLIST:y HAS_XREFS:y sort_cited";
 
                input.resultType= "lite";
                input.synonym = "false";
                input.email = "testclient@ebi.ac.uk";
                input.cursorMark = "*";
                var response = await soapClient.ExecuteAsync<WSCitationImpl, searchPublicationsRequest, searchPublicationsResponse1?>(SearchPublications, input, GivenPublications.Url);

                _testOutputHelper.WriteLine(JsonConvert.SerializeObject(response));

            }

            private Task<searchPublicationsResponse1?> SearchPublications(WSCitationImpl arg1, searchPublicationsRequest arg2)
            {
                return  arg1.searchPublicationsAsync(arg2);

            }
        }

    }
}