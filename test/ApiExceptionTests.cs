using Algorand;
using NUnit.Framework;
using System.Collections.Generic;

namespace test
{
    [TestFixture]
    public class ApiExceptionTests
    {
        [Test]
        public void testMessageAndToStringDoNotLeakResponseBody()
        {
            string secretResponseBody = "{\"private_key\":\"super-secret-exported-key-material\"}";
            var headers = new Dictionary<string, IEnumerable<string>>();

            var ex = new ApiException("Could not deserialize the response body string as Foo.", 200, secretResponseBody, headers, null);

            Assert.That(ex.Message, Does.Not.Contain(secretResponseBody));
            Assert.That(ex.Message, Does.Not.Contain("super-secret-exported-key-material"));
            Assert.That(ex.ToString(), Does.Not.Contain(secretResponseBody));
            Assert.That(ex.ToString(), Does.Not.Contain("super-secret-exported-key-material"));

            // full body remains available for callers who deliberately want it
            Assert.That(ex.Response, Is.EqualTo(secretResponseBody));
        }

        [Test]
        public void testMessageHandlesNullResponse()
        {
            var headers = new Dictionary<string, IEnumerable<string>>();
            var ex = new ApiException("Something went wrong.", 500, null, headers, null);

            Assert.That(ex.Message, Does.Contain("Something went wrong."));
            Assert.That(ex.Response, Is.Null);
        }
    }
}
