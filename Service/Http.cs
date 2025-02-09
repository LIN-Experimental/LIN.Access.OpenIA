using LIN.Access.OpenIA.Models;
using LIN.Types.Cloud.OpenAssistant.Abstractions;
using Newtonsoft.Json;

namespace LIN.Access.OpenIA.Service;

internal class Http
{

    /// <summary>
    /// Preguntar.
    /// </summary>
    /// <param name="messages">Lista de mensajes.</param>
    /// <param name="schema">Esquema.</param>
    public static async Task<Message?> Ask(List<Message> messages, string schema)
    {

        // Servicio.
        var client = new Global.Http.Services.Client("https://api.openai.com/v1/chat/completions");

        // Headers.
        client.AddHeader("Authorization", $"Bearer {OpenIA.ApiKey}");

        // Request.
        var requestData = new
        {
            model = "gpt-4o-mini",
            messages = messages.Select(t => new
            {
                role = t.Rol.ToString().ToLower(),
                content = t.Content
            }),
            temperature = 1,
            max_tokens = 256,
            top_p = 1,
            frequency_penalty = 0,
            presence_penalty = 0,
            response_format = JsonConvert.DeserializeObject(schema)
        };

        // Obtener respuesta.
        var response = await client.Post<ChatCompletionResponse>(requestData);

        return response?.Choices.FirstOrDefault()?.Message;
    }

}