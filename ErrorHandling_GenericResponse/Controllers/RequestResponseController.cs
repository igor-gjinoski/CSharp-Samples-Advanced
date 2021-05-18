using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ErrorHandling_GenericResponse.Controllers
{


    public interface IGenericActionResponse<T>
    {
        bool RequestSuccessful { get; }

        ActionResponseCode ResponseCode { get; }

        public T Item { get; }
    }



    public class GenericActionResponse<T> : IGenericActionResponse<T>
    {
        private bool _requestSuccessful;
        private ActionResponseCode _actionResponseCode;

        public T Item { get; set; }


        public GenericActionResponse(bool success, ActionResponseCode actionResponseCode)
            : this(success, actionResponseCode, default(T))
        {
            _requestSuccessful = success;
            _actionResponseCode = actionResponseCode;
            Item = default(T);
        }

        public GenericActionResponse(bool success, ActionResponseCode actionResponseCode, T item)
        {
            Item = item;
        }

        public bool RequestSuccessful
        {
            get { return _requestSuccessful; }
        }

        public ActionResponseCode ResponseCode
        {
            get { return _actionResponseCode; }
        }
    }




    [Route("[controller]")]
    [ApiController]
    public class RequestResponseController : ControllerBase
    {


        [HttpGet]
        public Task<ActionResult> ValidatePassword(string password = "TEST")
        {
            var result = (IGenericActionResponse<string>)IsPasswordValid(password);

            if (result.RequestSuccessful)
            {
                return PasswordOkResponse.GenerateResult(result);

                // return View("~/Areas/Public/Views/ResetPassword/PasswordChangeForm.cshtml", model);
            }
            else
            {
                return PasswordOkResponse.GenerateResult(result);
            }
        }

        public GenericActionResponse<T> IsPasswordValid<T>(T item)
            => 
            new GenericActionResponse<T>(true, ActionResponseCode.Success, item);
    }


    
    public static class PasswordOkResponse
    {
        public static Task<ActionResult> GenerateResult<T>(IGenericActionResponse<T> response)
        {
            var objResult = new ObjectResult(response.Item)
            {
                StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created
            };

            return Task.FromResult((ActionResult)objResult);
        }
    }


   

    public enum ActionResponseCode
    {
        Success,
        RecordNotFound,
        InvalidCreateHash,
        ExpiredCreateHash,
        ExpiredModifyHash,
        UnableToCreateRecord,
        UnableToUpdateRecord,
        UnableToSoftDeleteRecord,
        UnableToHardDeleteRecord,
        UserAlreadyExists,
        EmailCannotBeBlank,
        PasswordCannotBeBlank,
        PasswordResetHashExpired,
        AccountNotActivated,
        InvalidEmail,
        InvalidPassword,
        InvalidPageAction
    }
}
