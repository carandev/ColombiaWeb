using Data.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Client.Components.Shared;

public partial class PaginationComponent : ComponentBase
{
    [EditorRequired] [Parameter] public PaginationState Pagination { get; set; } = null!;

}