namespace LIN.Access.OpenIA.Models;

public class Message
{

    public Roles Role { get; set; }
    public string Content { get; set; } = string.Empty;



    public static Message FromSystem(string content)
    {
        return new()
        {
            Content = content,
            Role = Roles.System,
        };
    }


    public static Message FromUser(string content)
    {
        return new()
        {
            Content = content,
            Role = Roles.User,
        };
    }


    public static Message FromAssistant(string content)
    {
        return new()
        {
            Content = content,
            Role = Roles.Assistant,
        };
    }

}