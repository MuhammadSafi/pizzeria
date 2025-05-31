using Newtonsoft.Json.Linq;
using pizzeria.ValidationResponse;

namespace pizzeria.Validation
{
    public class JsonValidator
    {
        private IEnumerable<Response> validationErrors;

        public IEnumerable<Response> IsValidJsonFormat(string json)
        {
            validationErrors = new List<Response>();
            var validationErrorsLst = (List<Response>)validationErrors;
            var jsonObject = JArray.Parse("[]");
            //if (jsonObject.Count == 0)
            //{

            //    validationErrorsLst.Add(new Response { ResponseCode = ResponseCodes.JsonObjError, ResponseDescription = string.Format("{0}-{1}", ResponseDescription.JsonObjError) });
            //}
            try
            {
                jsonObject = JArray.Parse(json);
            }
            catch (Exception jObjectParsingEx)
            {
                validationErrorsLst.Add(new Response { ResponseCode = ResponseCodes.JsonError, ResponseDescription = string.Format("{0}-{1}", ResponseDescription.JsonError, jObjectParsingEx.Message) });

            }

            return validationErrorsLst;
        }
    }
}