using Amazon.Lambda.Core;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using PhoneNumberValidation.Model;
using System;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PhoneNumberValidation
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="req"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<NumberValidateResponse> FunctionHandler(PhoneNumberValidationRequestModel req, ILambdaContext context)
        {
            if (req != null)
            {
                try
                {
                    using (var client = new AmazonPinpointClient())
                    {
                        var res = await client.PhoneNumberValidateAsync(new PhoneNumberValidateRequest()
                        {
                            NumberValidateRequest = new NumberValidateRequest()
                            {
                                IsoCountryCode = req.Country,
                                PhoneNumber = req.PhoneNumber
                            }
                        });

                        if (res.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return res.NumberValidateResponse;
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Logger.Log(ex.Message);
                    context.Logger.Log(ex.StackTrace);
                }
            }
            return null;
        }
    }
}