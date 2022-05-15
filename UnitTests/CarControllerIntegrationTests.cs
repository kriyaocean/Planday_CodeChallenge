using CarFactory;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass, TestCategory("IntegrationTests")]
    public class CarControllerIntegrationTests
    {

        private static HttpClient _client;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            var application = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder => { });

            _client = application.CreateClient();
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_Stripe()
        {

            var jsonObj = CreateJsonString(paintType: "Stripe", baseColor: "Blue", stripeColor: "Orange", dotColor: null);
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent(jsonObj, Encoding.UTF8, "application/json")); 
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_stripe()
        {
            var jsonObj = CreateJsonString(paintType: "stripe", baseColor: "Blue", stripeColor: "Orange", dotColor: null);
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent(jsonObj, Encoding.UTF8, "application/json")); 
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_Dot()
        {
            var jsonObj = CreateJsonString(paintType: "Dot", baseColor: "Blue", stripeColor: null, dotColor: "Gold");
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent(jsonObj, Encoding.UTF8, "application/json")); 
            var responseString = await response.Content.ReadAsStringAsync();


            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsTrue(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_dOT()
        {
            var jsonObj = CreateJsonString(paintType: "dOT", baseColor: "Blue", stripeColor: null, dotColor: "Gold");
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent(jsonObj, Encoding.UTF8, "application/json")); 
            var responseString = await response.Content.ReadAsStringAsync();


            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsTrue(responseString.Contains("\"DotColor\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_Single()
        {
            var jsonObj = CreateJsonString(paintType: "Single", baseColor: "Blue", stripeColor: null, dotColor: null);
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent(jsonObj, Encoding.UTF8, "application/json")); 
            var responseString = await response.Content.ReadAsStringAsync();


            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
            Assert.IsTrue(responseString.Contains("\"Color\""));
        }

        [TestMethod]
        public async Task CarController_Post_canHandle_PaintType_single()
        {
            var jsonObj = CreateJsonString(paintType: "SinGle", baseColor: "Blue", stripeColor: null, dotColor: null);
            HttpResponseMessage response = await _client.PostAsync("/Car", new StringContent(jsonObj, Encoding.UTF8, "application/json")); 
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsFalse(responseString.Contains("\"StripeColor\""));
            Assert.IsFalse(responseString.Contains("\"DotColor\""));
            Assert.IsTrue(responseString.Contains("\"Color\""));
        }

        private string CreateJsonString(string paintType, string baseColor, string stripeColor, string dotColor)
        {
            object paint = new { type = paintType, baseColor, stripeColor, dotColor };
            object frontWindowSpeakers = new { numberOfSubWoofers = 1, speakers = new { numberOfSpeakers = 2, speakerType = "Standard" } };
            object specification = new { numberOfDoors = 5, paint, manufacturer = "PlanfaRomeo", frontWindowSpeakers };
            object cars = new object[] { new { amount = 2, specification } };
            object carsObj = new { cars };

            return JsonConvert.SerializeObject(carsObj);
        }
    }
}
