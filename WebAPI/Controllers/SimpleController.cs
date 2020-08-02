using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleDLL;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class SimpleController : ControllerBase
    {
        private ILogger<SimpleController> Logger { get; }

        private ISimpleDLLFeedbackProxy SimpleDLLFeedbackProxy { get; }

        private Func<string, ISimpleDLLFeedback> SimpleDLLFeedback { get; }
        
        public SimpleController(ILogger<SimpleController> logger,
                                ISimpleDLLFeedbackProxy simpleDLLFeedbackProxy)
        {
            Logger = logger;
            SimpleDLLFeedbackProxy = simpleDLLFeedbackProxy;
            SimpleDLLFeedback = SimpleDLLFeedbackProxy.Get;
        }

        [HttpGet]
        [Route("simplemethod")]
        public IActionResult CallSimpleMethod([FromQuery][Required] string subid)
        {
            Logger.LogInformation($"CallSimpleMethod API called for subscriber {subid}");
            SimpleDLLFeedback(subid).SimpleMethod();
            return Ok();
        }
    }
}
