﻿namespace Eliassen.Search.Models;

public record SearchResultWithSummaryModel : SearchResultModel
{
    public string Summary { get; init; }
}
