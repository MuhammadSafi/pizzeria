using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using pizzeria.DTO;
using pizzeria.ValidationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.Validation
{
    public class ProductValidator
    {
        private IEnumerable<Response> validationErrors;

        public IEnumerable<Response> ValidationErrors(string json)
        {

            validationErrors = new List<Response>();
            var JsonValidator = new JsonValidator();
            var validationErrorsLst = (List<Response>)validationErrors;
            var error = JsonValidator.IsValidJsonFormat(json);
            if (error.Count() > 0)
            {
                return error;
            }
            try
            {
                var jsonObject = JArray.Parse("[]");
                var orders = JsonConvert.DeserializeObject<Product[]>(jsonObject.ToString());

                if (orders is not null)
                {
                    foreach (var product in orders)
                    {
                        if (product.Price <= 0)
                        {
                            validationErrorsLst.Add(new Response { ResponseCode = ResponseCodes.PriceError, ResponseDescription = string.Format("(0)", ResponseDescription.PriceError) });
                            return validationErrorsLst;
                        }
                    }

                }
            }
            catch (Exception jsonObjErr)
            {

                validationErrorsLst.Add(new Response { ResponseCode = ResponseCodes.JsonObjError, ResponseDescription = string.Format("{0}-{1}", ResponseDescription.JsonObjError, jsonObjErr.Message) });
                return validationErrorsLst;

            }

            return validationErrorsLst;
        }
    }
}