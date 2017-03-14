using DealerConceptsApp.Services.Interfaces;
using DealerConceptsApp.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DealerConceptsApp.Models.Responses;
using DealerConceptsApp.Classes.Exceptions;
using System.Net;
using DealerConceptsApp.Models;

namespace DealerConceptsApp.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UserRegistrationApiController : BaseApiController
    {
        private IUserService _userService;
        private ISignInService _signInService;

        public UserRegistrationApiController(IUserService userService, ISignInService signInService)
        {
            _userService = userService;
            _signInService = signInService;
        }

        [Route, HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(UserRegistrationCreateRequest model)
        {
            if (!IsValidRequest(model))
            {
                return GetErrorResponse(model);
            }

            try
            {
                _userService.CreateUser(model.Email, model.Password);

                bool signedIn = _signInService.Signin(model.Email, model.Password, true);

                if (signedIn)
                {
                    ItemResponse<bool> responseData = new ItemResponse<bool>();

                    responseData.Item = signedIn;
                    responseData.IsSuccessful = true;
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, responseData);
                }
                else
                {
                    throw new Exception("Unexpected error");
                }
            }
            catch (IdentityResultException e)
            {
                string message = String.Join(" ", e.Result.Errors);

                ErrorResponse response = new ErrorResponse(e.Result.Errors);

                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex)
            {
                ErrorResponse exceptionResponse = new ErrorResponse(ex.Message);

                return Request.CreateResponse(HttpStatusCode.BadRequest, exceptionResponse);
            }
        }

        [Route("logout"), HttpGet][AllowAnonymous]
        public HttpResponseMessage LogOut()
        {
            SuccessResponse responseData = new SuccessResponse();
            _userService.Logout();
            return Request.CreateResponse(HttpStatusCode.OK, responseData);
        }

        [Route("reset"), HttpPut]
        public HttpResponseMessage Reset(UserPasswordRequest upr)
        {
            ItemResponse<bool> responseData = new ItemResponse<bool>();
            string userId;

            if (!IsValidRequest(upr))
            {
                return GetErrorResponse(upr);
            }

            if (_userService.IsLoggedIn())
            {
                userId = _userService.GetCurrentUserId();
                responseData.Item = _userService.ChangePassword(userId, upr.Password);
            }

            return Request.CreateResponse(HttpStatusCode.OK, responseData);
        }

        [Route("recover/{token:Guid}"), HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage ConfirmRecover(UserPasswordRecover upr, Guid token)
        {
            ItemResponse<bool> responseData = new ItemResponse<bool>();

            upr.Token = token;

            if (!IsValidRequest(upr))
            {
                return GetErrorResponse(upr);
            }

            _userService.IsValidToken(token);

            if (true)
            {
                _userService.UpdateConfirmation(token);
            }

            responseData.Item = true;
            return Request.CreateResponse(HttpStatusCode.OK, responseData);
        }

        [Route("login"), HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage LogIn(UserRegistrationCreateRequest model)
        {
            if (!IsValidRequest(model))
            {
                return GetErrorResponse(model);
            }

            try
            {
                bool ok = _signInService.Signin(model.Email, model.Password);

                if (ok)
                {
                    ApplicationUser user = _userService.GetUser(model.Email);
                    string userId = user.Id;

                    ItemResponse<IList<string>> rolesResponse = new ItemResponse<IList<string>>();
                    rolesResponse.Item = _userService.GetRoles(userId);

                    return Request.CreateResponse(HttpStatusCode.OK, rolesResponse);
                }
                else
                {
                    ErrorResponse errorResponse = new ErrorResponse("An error has occured");
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, errorResponse);
                }
            }

            catch (Exception ex)
            {
                ErrorResponse err = new ErrorResponse(ex.Message);

                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

        [AllowAnonymous]
        [Route("{token:Guid}"), HttpDelete]
        public HttpResponseMessage Update(Guid token)
        {
            string userId = _userService.UpdateConfirmation(token);

            if (userId == string.Empty)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid token");
            }

            SuccessResponse responseData = new SuccessResponse();
            return Request.CreateResponse(HttpStatusCode.OK, responseData);
        }


        [Route("recover"), HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Recover(UserRegistrationCreateRequest model)
        {
            _userService.UpdateUserById(model.Email);

            SuccessResponse responseData = new SuccessResponse();

            return Request.CreateResponse(HttpStatusCode.OK, model.Email);
        }
    }
}