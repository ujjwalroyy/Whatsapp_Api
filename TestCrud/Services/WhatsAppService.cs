
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestCrud.Models;
using TestCrud.Dto;

namespace TestCrud.Services
{
    public class WhatsAppService
    {
        private readonly string token = "EAAKdVbWnJF0BOZBmX87Gw9ckB2LLmP8iDHjlgGHEeWpkEJwWlcV1kOlrtsF3CNcNZCE2BP6zdLhokMIGd6bmriWeq4M4HM3uQCJL7PSWSPbRiptVkhbz1BVgEMwtWddQiMNIUvZCbezmkvZA2icMnLdEePHhkrVm1M8bGvcc2ss46fh6SLurc8s2MznbaQMHMzZALoC5mSzsQ6pg7vfl7zLp7ZCGQ7eSPoAnTkKbizKZBkdSQZDZD";
        private readonly string phoneNumberId = "604453616094805";

        public async Task<string> SendMessage(WhatsAppMessage message)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var payload = new
                {
                    messaging_product = "whatsapp",
                    to = message.PhoneNumber,
                    type = "template",
                    template = new
                    {
                        name = message.TemplateName,
                        language = new { code = message.LanguageCode }
                    }
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    $"https://graph.facebook.com/v19.0/{phoneNumberId}/messages", content);

                return await response.Content.ReadAsStringAsync();
            }
        }
        public async Task<string> SendImageMessageAsync(SendMediaRequest request)
        {
            var payload = new
            {
                messaging_product = "whatsapp",
                to = request.PhoneNumber,
                type = "image",
                image = new
                {
                    id = request.MediaId,
                    caption = request.Caption
                }
            };

            return await SendToWhatsAppApiAsync(payload);
        }

        public async Task<string> SendDocumentMessageAsync(SendMediaRequest request)
        {
            var payload = new
            {
                messaging_product = "whatsapp",
                to = request.PhoneNumber,
                type = "document",
                document = new
                {
                    id = request.MediaId,
                    caption = request.Caption
                }
            };

            return await SendToWhatsAppApiAsync(payload);
        }

        private async Task<string> SendToWhatsAppApiAsync(object payload)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(
                    $"https://graph.facebook.com/v19.0/{phoneNumberId}/messages",
                    content
                );

                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
