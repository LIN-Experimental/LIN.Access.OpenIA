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
    /// Nueva IA
    /// </summary>
    /// <param name="apiKey">Api key de OpenIA</param>
    public IAModelBuilder()
    {

    }




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


    public void LoadFromUser(string message)
    {
        Context.Add(Message.FromUser(message));
    }

    public void LoadFromEmma(string message)
    {
        Context.Add(Message.FromAssistant(message));
    }


    /// <summary>
    /// Carga la personalidad de Emma.
    /// </summary>
    private Message GetActualContext()
    {

        var context = $"""
                       IMPORTANTE para el contexto:
                       - La fecha actual es {DayMont(DateTime.Now.DayOfWeek)} {DateTime.Now.Day} de {DateTime.Now:MMMM} del año {DateTime.Now:yyyy}
                       """;

        return Message.FromSystem(context);
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