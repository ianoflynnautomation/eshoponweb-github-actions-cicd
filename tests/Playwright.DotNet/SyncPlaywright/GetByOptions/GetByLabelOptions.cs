﻿using System.Text.Json.Serialization;

namespace Playwright.DotNet.SyncPlaywright.GetByOptions;

#nullable enable

public class GetByLabelOptions : IOptions
{
    /// <summary>
    /// <para>
    /// Whether to find an exact match: case-sensitive and whole-string. Default to false.
    /// Ignored when locating by a regular expression. Note that exact match still trims
    /// whitespace.
    /// </para>
    /// </summary>
    [JsonPropertyName("exact")]
    public bool? Exact { get; set; }
}

#nullable disable
