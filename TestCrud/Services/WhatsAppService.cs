
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestCrud.Dto;
using TestCrud.Models;

namespace TestCrud.Services
{
    public class WhatsAppService
    {
        private readonly string token = "EAAKdVbWnJF0BPJnHrrSBruGIDV2ZBFn1WVRF2FK1dwOBaa5NXr9nfVxBSxSu0G7A0Y6s8L0HG81E7PfE5sfS6ZCtCICGi1iEmfhAttdHImPbHSTflYaDDLORVS2g4e3kkGciuCZAZCl6wlRQgORSETs0s41iuj1LBRINzrVP8tDAdIlziuOit0TCKmly5fzURQXearDcTyzip6arNgNTYDtkoOBmpoFZB5SLhZCZA5K38i6JAZDZD";
        private readonly string phoneNumberId = "604453616094805";

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public async Task<string> SendMessage(WhatsAppMessage message)
        {
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

            return await SendToWhatsAppApiAsync(payload);
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
            try
            {
                using (var client = CreateHttpClient())
                {
                    var content = new StringContent(
                        JsonConvert.SerializeObject(payload),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        $"https://graph.facebook.com/v19.0/{phoneNumberId}/messages",
                        content
                    );

                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return $"Success: {responseContent}";
                    }
                    else
                    {
                        return $"Error: {response.StatusCode} - {responseContent}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

    }
}
