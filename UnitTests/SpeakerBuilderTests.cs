using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Interior.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using static CarFactory_Factory.CarSpecification;

namespace UnitTests
{
    [TestClass, TestCategory("UnitTests")]
    public class SpeakerBuilderTests
    {
        [TestMethod]
        public void SpeakerBuilder_CanBuildMoreThan2SpeakersOfCorrectType()
        {
            var sp = new SpeakerBuilder();
            var result = sp.BuildSpeakers(new List<SpeakerSpecification>
            {
                new SpeakerSpecification { SpeakerType = SpeakerSpecType.Standard },
                new SpeakerSpecification { SpeakerType = SpeakerSpecType.Standard },
                new SpeakerSpecification { SpeakerType = SpeakerSpecType.SubWoofer },
                new SpeakerSpecification { SpeakerType = SpeakerSpecType.Normal }
            });

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(1, result.Where(x => x is SubWoofer).Count());
            Assert.AreEqual(2, result.Where(x => x is StandardSpeaker).Count());
            Assert.AreEqual(1, result.Where(x => x is NormalSpeaker).Count());
        }
    }
}
