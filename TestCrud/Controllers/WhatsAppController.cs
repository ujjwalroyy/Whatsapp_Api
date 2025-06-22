using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestCrud.Dto;
using TestCrud.Models;
using TestCrud.Services;

namespace TestCrud.Controllers
{
    [RoutePrefix("api/whatsapp")]
    public class WhatsAppController : ApiController
    {
        private readonly WhatsAppService _service = new WhatsAppService();

        [HttpPost]
        [Route("send")]
        public async Task<IHttpActionResult> Send(WhatsAppMessage message)
        {
            if (message == null || string.IsNullOrEmpty(message.PhoneNumber))
                return BadRequest("Phone number is required");

            var result = await _service.SendMessage(message);
            return Ok(result);
        }

        [HttpPost]
        [Route("send-image")]
        public async Task<IHttpActionResult> SendImage([FromBody] SendMediaRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrEmpty(request.MediaId))
                return BadRequest("Invalid request");

            var result = await _service.SendImageMessageAsync(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("send-file")]
        public async Task<IHttpActionResult> SendFile([FromBody] SendMediaRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrEmpty(request.MediaId))
                return BadRequest("Invalid request");

            var result = await _service.SendDocumentMessageAsync(request);
            return Ok(result);
        }


        [HttpGet]
        [Route("webhook")]
        public HttpResponseMessage VerifyWebhook(
        [FromUri(Name = "hub.mode")] string hub_mode,
        [FromUri(Name = "hub.verify_token")] string hub_verify_token,
        [FromUri(Name = "hub.challenge")] string hub_challenge)
        {
            if (hub_mode == "subscribe" && hub_verify_token == "ujjwal_verify_token")
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(hub_challenge, Encoding.UTF8, "text/plain");
                return response;
            }

            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        [HttpPost]
        [Route("webhooks")]
        public async Task<IHttpActionResult> ReceiveWebhook([FromBody] dynamic data)
        {
            string json = data.ToString();
            System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/App_Data/webhook_log.json"), json);
            return await Task.FromResult(Ok());
        }
        
    }
}
