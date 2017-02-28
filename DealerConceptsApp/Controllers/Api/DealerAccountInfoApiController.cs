using DealerConceptsApp.Controllers.Api;
using DealerConceptsApp.Domain;
using DealerConceptsApp.Models.Request;
using DealerConceptsApp.Models.Responses;
using DealerConceptsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DealerConceptsApp.Controllers.Api
{
    [RoutePrefix("api/dealer")]
    public class DealerAccountInfoApiController : BaseApiController
    {
        private IDealerAccountInfoService _dealerAccountInfoService = null;
        private IUserService _userService = null;
        
        public DealerAccountInfoApiController(IDealerAccountInfoService dealerAccountInfoService, IUserService userService)
        {
            _dealerAccountInfoService = dealerAccountInfoService;
            _userService = userService;
        } 

        [Route("accountinfo"), HttpPost]
        public HttpResponseMessage Create(DealerAccountInfoCreateRequest model)
        {
            if(!IsValidRequest(model))
            {
                return GetErrorResponse(model);
            }

            string userId = _userService.GetCurrentUserId();
            int id = _dealerAccountInfoService.CreateDealerAccountInfo(model, userId);
            ItemResponse<int> responseData = new ItemResponse<int>();
            responseData.Item = id;

            return Request.CreateResponse(HttpStatusCode.OK, responseData);
        }

        [Route("accountinfo/{Id:int}"), HttpPut]
        public HttpResponseMessage Update(int id, DealerAccountInfoUpdateRequest model)
        {
            if (!IsValidRequest(model))
            {
                return GetErrorResponse(model);
            }

            _dealerAccountInfoService.UpdateDealerAccountInfo(id, model);
            SuccessResponse sr = new SuccessResponse();

            return Request.CreateResponse(HttpStatusCode.OK, sr);
        }
    }
}
