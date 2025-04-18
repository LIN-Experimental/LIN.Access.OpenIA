﻿using LIN.Types.Cloud.OpenAssistant.Abstractions;

namespace LIN.Access.OpenIA;

public class IAModelBuilder
{

    /// <summary>
    /// Esquema de respuesta.
    /// </summary>
    public string Schema { get; set; } = "{\"type\": \"text\"}";


    /// <summary>
    /// Lista base de mensajes
    /// </summary>
    private List<Message> Context { get; set; } = [];


    /// <summary>
    /// Carga un contexto.
    /// </summary>
    /// <param name="message">Mensaje</param>
    public void Load(Message message)
    {
        Context.Add(message);
    }


    /// <summary>
    /// Carga un contexto.
    /// </summary>
    /// <param name="message">Mensaje</param>
    public void Load(List<Message> message)
    {
        Context.AddRange(message);
    }


    /// <summary>
    /// Carga un contexto.
    /// </summary>
    /// <param name="message">Mensaje</param>
    public void Load(string message)
    {
        Context.Add(Message.FromSystem(message));
    }


    /// <summary>
    /// Responder
    /// </summary>
    /// <param name="message">Mensaje</param>
    public async Task<LIN.Types.Cloud.OpenAssistant.Api.AssistantResponse> Reply(string message)
    {
        try
        {

            var lista = new List<Message>();

            // lista.Add(GetActualContext());
            lista.AddRange(Context);

            lista.Add(Message.FromUser(message));

            var completionResult = await Service.Http.Ask(lista, Schema);

            return new()
            {
                Content = completionResult?.Content ?? "Error"
            };

        }
        catch (Exception)
        {
            return new()
            {
                Content = "Error"
            };
        }


    }


    /// <summary>
    /// Responder
    /// </summary>
    /// <param name="message">Mensaje</param>
    public async Task<Types.Cloud.OpenAssistant.Api.AssistantResponse> Reply()
    {
        try
        {

            // Lista de mensajes.
            var lista = new List<Message>();

            //  lista.Add(GetActualContext());
            lista.AddRange(Context);

            var completionResult = await Service.Http.Ask(lista, Schema);

            return new()
            {
                Content = completionResult?.Content ?? "Error."
            };

        }
        catch (Exception)
        {
            return new()
            {
                Content = "Error."
            };
        }
    }
}