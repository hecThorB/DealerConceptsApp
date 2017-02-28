using DealerConceptsApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DealerConceptsApp.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected bool IsValidRequest(object model)
        {
            bool isValid = true;

            if (model == null)
            {
                isValid = false;
            }
            else if (!ModelState.IsValid)
            {
                isValid = false;
            }

            return isValid;
        }


        protected HttpResponseMessage GetErrorResponse(object model)
        {
            List<string> errMsgs = null;

            if (IsValidRequest(model))
            {
                throw new Exception("Invalid call to GetErrorResponse");
            }


            if (model == null)
            {
                errMsgs = new List<string>();
                errMsgs.Add("Model cannot be empty");

            }
            else if (!ModelState.IsValid)
            {
                errMsgs = new List<string>();
                foreach (var i in ModelState.Values)
                {
                    foreach (var item in i.Errors)
                    {
                        errMsgs.Add(item.ErrorMessage);
                    }
                }

            }

            ErrorResponse errspon = null;
            if (errMsgs != null)
            {
                errspon = new ErrorResponse(errMsgs);
            }


            return Request.CreateResponse(HttpStatusCode.BadRequest, errspon);
        }
    }
}
