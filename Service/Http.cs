using LIN.Access.OpenIA.Models;
using System.Text;

namespace LIN.Access.OpenIA.Service;

internal class Http
{


    public static async Task<Message?> Ask(List<Message> messages)
    {

        try
        {
            string apiUrl = "https://api.openai.com/v1/chat/completions";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {OpenIA.ApiKey}");


            var requestData = new
            {
                model = "gpt-3.5-turbo-0125",
                messages = messages.Select(t => new
                {
                    role = t.Role.ToString().ToLower(),
                    content = t.Content
                }),
                temperature = 1,
                max_tokens = 256,
                top_p = 1,
                frequency_penalty = 0,
                presence_penalty = 0
            };


            var requestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            var content = new StringContent(requestDataJson, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                ChatCompletionResponse chatCompletion = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatCompletionResponse>(responseContent) ?? new();

                return chatCompletion.Choices[0].Message;
            }
        }
        catch
        {
        }

        return null;
    }

}
