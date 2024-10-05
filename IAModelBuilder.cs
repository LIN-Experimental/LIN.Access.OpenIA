using LIN.Access.OpenIA.Models;
using LIN.Types.Emma.Models;

namespace LIN.Access.OpenIA;

public class IAModelBuilder
{

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
    public async Task<ResponseIAModel> Reply(string message)
    {
        try
        {

            var lista = new List<Message>();

            // lista.Add(GetActualContext());
            lista.AddRange(Context);


            lista.Add(Message.FromUser(message));


            var completionResult = await Service.Http.Ask(lista);



            return new()
            {
                Content = completionResult?.Content ?? "Error de Emma al contestar",
                IsSuccess = completionResult is not null
            };

        }
        catch (Exception)
        {
            return new()
            {
                Content = "Emma actualmente esta presentando problemas.",
                IsSuccess = false
            };
        }


    }


    /// <summary>
    /// Responder
    /// </summary>
    /// <param name="message">Mensaje</param>
    public async Task<ResponseIAModel> Reply()
    {
        try
        {

            // Lista de mensajes.
            var lista = new List<Message>();

            //  lista.Add(GetActualContext());
            lista.AddRange(Context);

            var completionResult = await Service.Http.Ask(lista);

            return new()
            {
                Content = completionResult?.Content ?? "Error de Emma al contestar",
                IsSuccess = completionResult is not null
            };

        }
        catch (Exception)
        {
            return new()
            {
                Content = "Emma actualmente esta presentando problemas.",
                IsSuccess = false
            };
        }


    }






}