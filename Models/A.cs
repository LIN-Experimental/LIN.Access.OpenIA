using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIN.Access.OpenIA.Models;

using System;

public class ChatCompletionResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public long Created { get; set; }
    public string Model { get; set; }
    public Choice[] Choices { get; set; }
    public Usage Usage { get; set; }
    public string SystemFingerprint { get; set; }
}

public class Choice
{
    public int Index { get; set; }
    public Message Message { get; set; }
    public object Logprobs { get; set; }
    public string FinishReason { get; set; }
}


public class Usage
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
}
