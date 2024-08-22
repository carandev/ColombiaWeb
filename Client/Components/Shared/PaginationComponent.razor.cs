using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Client.Components.Shared;

public partial class PaginationComponent : ComponentBase
{
    /// <summary>
    ///     Estado de la paginación, con la cantidad por página indicado.
    /// </summary>
    [EditorRequired] [Parameter] public PaginationState Pagination { get; set; } = null!;

}