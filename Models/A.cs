using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIN.Access.OpenIA.Models;

using System;

public class ChatCompletionResponse
{
    public string Id { get; set; } = string.Empty;
    public string Object { get; set; } = string.Empty;
    public long Created { get; set; }
    public string Model { get; set; } = string.Empty;
    public Choice[] Choices { get; set; } = [];
    public Usage Usage { get; set; } = null!;
    public string SystemFingerprint { get; set; } = string.Empty;
}


public class Choice
{
    public int Index { get; set; }
    public Message Message { get; set; } = null!;
    public object Logprobs { get; set; } = null!;
    public string FinishReason { get; set; } = string.Empty;
}


public class Usage
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
}
