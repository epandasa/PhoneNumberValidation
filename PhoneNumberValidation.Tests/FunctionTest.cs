using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Pinpoint.Model;
using PhoneNumberValidation.Model;

namespace PhoneNumberValidation.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();

            var req = new PhoneNumberValidationRequestModel() 
            {
                Country = "DE", 
                PhoneNumber = "+49012345568798" 
            };

            var result = function.FunctionHandler(req, context).GetAwaiter().GetResult();

            Assert.NotNull(result);
            Assert.IsType<NumberValidateResponse>(result);
        }
    }
}
