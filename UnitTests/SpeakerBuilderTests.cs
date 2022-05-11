using CarFactory_Interior.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using static CarFactory_Factory.CarSpecification;

namespace UnitTests
{
    [TestClass]
    public class SpeakerBuilderTests
    {
        [TestMethod]
        public void SpeakerBuilder_CanBuildMoreThan2SpeakersOfCorrectType()
        {
            var sp = new SpeakerBuilder();
            var result = sp.BuildFrontWindowSpeakers(new List<SpeakerSpecification>
            {
                new SpeakerSpecification { IsSubwoofer = true },
                new SpeakerSpecification { IsSubwoofer = false },
                new SpeakerSpecification { IsSubwoofer = false }
            });
            var isSub = result.Where(x => x.IsSubwoofer);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result.Where(x => x.IsSubwoofer).Count());
            Assert.AreEqual(2, result.Where(x => !x.IsSubwoofer).Count());
        }
    }
}
