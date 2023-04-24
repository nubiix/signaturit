using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Newtonsoft.Json;
using SignatureEvaluator;
using SignatureModels.Models;
using System.Reflection;
using Resources = SignatureModels.Models.Resources;

namespace TestSignature
{
    [TestFixture]
    public class Tests
    {
        public IConfiguration configuration{ get; set; }
        public Roles rol = new Roles();
        public Resources resources = new Resources();
        public SignatureEvaluatorService signatureEvaluator;

        [OneTimeSetUp]
        public void init()
        {
            configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();

            configuration.GetSection("Roles").Bind(rol);

            configuration.GetSection("Resources").Bind(resources);
            signatureEvaluator = new SignatureEvaluatorService(Options.Create(rol), Options.Create(resources));
        }

        [Test]
        public void EvaluateSignatureTest()
        {
            var request = new SignatureRequest("KNV", "KKNV");
            var response = signatureEvaluator.EvaluateSignature(request);
            var expected = new SignatureResponse()
            {
                Winner="Defendant",
                ImplicatedParts = new SignatureGroup() {
                    Plaintiff = new User() {
                        Sign = "KNV",
                        Rating = 8
                    },
                    Defendant = new User()
                    {
                        Sign = "KKNV",
                        Rating = 13
                    }
                }

            };
            Assert.AreEqual(JsonConvert.SerializeObject(response), JsonConvert.SerializeObject(expected));
        }

        [Test]
        public void EvaluateSignatureRequirementTest()
        {
            var request = new SignatureRequest("KNV#", "KKNV");
            var response = signatureEvaluator.EvaluateSignatureRequirement(request);
            var expected = new SignatureRequirementResponse()
            {
                SignatureRequirement = "you need this sign: KV",
                ImplicatedParts = new SignatureGroup()
                {
                    Plaintiff = new User()
                    {
                        Sign = "KNV#",
                        Rating = 8
                    },
                    Defendant = new User()
                    {
                        Sign = "KKNV",
                        Rating = 13
                    }
                }
            };
            Assert.AreEqual(JsonConvert.SerializeObject(response), JsonConvert.SerializeObject(expected));
        }
    }
}