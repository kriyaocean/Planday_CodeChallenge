using CarFactory;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass, TestCategory("Integration tests")]
    public class CarControllerIntegrationTests
    {

        private static HttpClient _client;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            var s = string.Empty;
            var application = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder => {  /* ... Configure test services */ });

            _client = application.CreateClient();
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_Stripe()
        {
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent("{ \"cars\": [ { \"amount\": 2, \"specification\": { \"numberOfDoors\": 5, \"paint\": { \"type\": \"Stripe\", \"baseColor\": \"Blue\", \"stripeColor\": \"Orange\", \"dotColor\": null }, \"manufacturer\": \"PlanfaRomeo\", \"frontWindowSpeakers\": { \"numberOfSubWoofers\": 2, \"speakers\": { \"numberOfSpeakers\": 5, \"speakerType\": \"Normal\" } } } } ] }", Encoding.UTF8, "application/json")); 
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_stripe()
        {
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent("{ \"cars\": [ { \"amount\": 2, \"specification\": { \"numberOfDoors\": 5, \"paint\": { \"type\": \"stripe\", \"baseColor\": \"Blue\", \"stripeColor\": \"Orange\", \"dotColor\": null }, \"manufacturer\": \"PlanfaRomeo\", \"frontWindowSpeakers\": { \"numberOfSubWoofers\": 2, \"speakers\": { \"numberOfSpeakers\": 5, \"speakerType\": \"Normal\" } } } } ] }", Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_Dot()
        {
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent("{ \"cars\": [ { \"amount\": 2, \"specification\": { \"numberOfDoors\": 5, \"paint\": { \"type\": \"Dot\", \"baseColor\": \"Blue\", \"stripeColor\": null, \"dotColor\": \"Purple\" }, \"manufacturer\": \"PlanfaRomeo\", \"frontWindowSpeakers\": { \"numberOfSubWoofers\": 2, \"speakers\": { \"numberOfSpeakers\": 5, \"speakerType\": \"Normal\" } } } } ] }", Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsTrue(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_dot()
        {
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent("{ \"cars\": [ { \"amount\": 2, \"specification\": { \"numberOfDoors\": 5, \"paint\": { \"type\": \"dot\", \"baseColor\": \"Blue\", \"stripeColor\": null, \"dotColor\": \"Purple\" }, \"manufacturer\": \"PlanfaRomeo\", \"frontWindowSpeakers\": { \"numberOfSubWoofers\": 2, \"speakers\": { \"numberOfSpeakers\": 5, \"speakerType\": \"Normal\" } } } } ] }", Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsTrue(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_Single()
        {
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent("{ \"cars\": [ { \"amount\": 2, \"specification\": { \"numberOfDoors\": 5, \"paint\": { \"type\": \"Single\", \"baseColor\": \"Blue\", \"stripeColor\": null, \"dotColor\": null }, \"manufacturer\": \"PlanfaRomeo\", \"frontWindowSpeakers\": { \"numberOfSubWoofers\": 2, \"speakers\": { \"numberOfSpeakers\": 5, \"speakerType\": \"Normal\" } } } } ] }", Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
            Assert.IsTrue(responseString.Contains("\"Color\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_single()
        {
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent("{ \"cars\": [ { \"amount\": 2, \"specification\": { \"numberOfDoors\": 5, \"paint\": { \"type\": \"single\", \"baseColor\": \"Blue\", \"stripeColor\": null, \"dotColor\": null }, \"manufacturer\": \"PlanfaRomeo\", \"frontWindowSpeakers\": { \"numberOfSubWoofers\": 2, \"speakers\": { \"numberOfSpeakers\": 5, \"speakerType\": \"Normal\" }  } } } ] }", Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
            Assert.IsTrue(responseString.Contains("\"Color\""));
        }
    }
}
