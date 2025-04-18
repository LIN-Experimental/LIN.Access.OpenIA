﻿using LIN.Types.Cloud.OpenAssistant.Abstractions;

namespace LIN.Access.OpenIA.Models;

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
    public string FinishReason { get; set; } = string.Empty;
}

public class Usage
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
}