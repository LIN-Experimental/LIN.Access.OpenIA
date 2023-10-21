using LIN.Types.Emma.Models;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace LIN.Access.OpenIA;


public class IAModelBuilder
{

    /// <summary>
    /// Servicio de Open IA
    /// </summary>
    private OpenAIService Service { get; set; }


    /// <summary>
    /// Lista base de mensajes
    /// </summary>
    private List<ChatMessage> Context { get; set; }



    /// <summary>
    /// Nueva IA
    /// </summary>
    /// <param name="apiKey">Api key de OpenIA</param>
    public IAModelBuilder(string apiKey)
    {
        Service = new(new()
        {
            ApiKey = apiKey
        });

        Service.SetDefaultModelId(Models.Davinci);
        Context = new();
    }




    /// <summary>
    /// Carga un contexto.
    /// </summary>
    /// <param name="message">Mensaje</param>
    public void Load(ChatMessage message)
    {
        Context.Add(message);
    }


    /// <summary>
    /// Carga un contexto.
    /// </summary>
    /// <param name="message">Mensaje</param>
    public void Load(string message)
    {
        Context.Add(ChatMessage.FromSystem(message));
    }


    public void LoadFromUser(string message, string name)
    {
        Context.Add(ChatMessage.FromUser(message, name));
    }



    /// <summary>
    /// Carga la personalidad de Emma.
    /// </summary>
    private ChatMessage GetActualContext()
    {

        var context = $"""
                       Importante para el contexto:
                       - La fecha actual es {DayMont(DateTime.Now.DayOfWeek)}{DateTime.Now.Day} de {DateTime.Now:MMMM} del año {DateTime.Now:yyyy}
                       """;

        return ChatMessage.FromSystem(context);
    }


    private string DayMont(DayOfWeek day)
    {

        if (day == DayOfWeek.Sunday)
            return "domingo";
        if (day == DayOfWeek.Friday)
            return "viernes";
        if (day == DayOfWeek.Wednesday)
            return "miércoles";
        if (day == DayOfWeek.Monday)
            return "lunes";
        if (day == DayOfWeek.Tuesday)
            return "martes";
        if (day == DayOfWeek.Saturday)
            return "Sabado";
        if (day == DayOfWeek.Thursday)
            return "jueves";

        return day.ToString();
    }


    /// <summary>
    /// Responder
    /// </summary>
    /// <param name="message">Mensaje</param>
    public async Task<ResponseIAModel> Reply(string message)
    {
        try
        {

            var lista = new List<ChatMessage>();
            lista.AddRange(Context);

            lista.Add(GetActualContext());
            lista.Add(ChatMessage.FromUser(message));


            var completionResult = await Service.ChatCompletion.CreateCompletion(new()
            {
                Messages = lista,
                Model = Models.Gpt_3_5_Turbo_16k_0613
            });


            return new()
            {
                Content = completionResult.Choices.FirstOrDefault()?.Message.Content ?? "Error de Emma al contestar",
                IsSuccess = completionResult.Successful
            };

        }
        catch (Exception ex)
        {
            return new()
            {
                Content = ex.Message + " | " + ex.StackTrace,
                IsSuccess = false
            };
        }


    }



}
