using Data.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Shared;

public partial class Card : ComponentBase
{
    /// <summary>
    ///     Presidente.
    /// </summary>
    [Parameter]
    public President CurrentPresident { get; set; } = null!;
    
    private string PresidentUrl => $"/presidents/{CurrentPresident.Id}";
}