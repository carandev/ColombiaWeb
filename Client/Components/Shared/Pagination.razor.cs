using Data.Utils;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Shared;

public partial class Pagination : ComponentBase
{
    [Parameter] public int PageNumber { get; set; }
    
    [Parameter] public int PageSize { get; set; }
    
    [Parameter] public int Total { get; set; }
    
    [Parameter] public int PageCount { get; set; }
}