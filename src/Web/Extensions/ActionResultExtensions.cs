using System.Web.Mvc;
using Depot.Web.Helpers;

namespace Depot.Web.Extensions
{
    public static class ActionResultExtensions
    {
         public static FlashActionResult<TActionResult> WithFlash<TActionResult>(this TActionResult instance, FlashMessageType messageType, string message)
             where TActionResult: ActionResult
         {
             return new FlashActionResult<TActionResult>(instance, messageType, message);
         }
    }

    public class FlashActionResult<TActionResult> : ActionResult where TActionResult: ActionResult
    {
        private readonly TActionResult _actionResult;
        private readonly FlashMessageType _messageType;
        private readonly string _message;

        public FlashActionResult(TActionResult actionResult, FlashMessageType messageType, string message)
        {
            _actionResult = actionResult;
            _messageType = messageType;
            _message = message;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var flashMessageData = new FlashMessageData(context.Controller.TempData);
            flashMessageData.Add(_messageType, _message);
            _actionResult.ExecuteResult(context);
        }
    }
}