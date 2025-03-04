using Fantasy.FrontEnd.Repositories;
using Fantasy.Shared.Entites.GeneralParameters;
using Microsoft.AspNetCore.Components;

namespace Fantasy.FrontEnd.Pages.GeneralParameters;

public partial class CountriesIndex
{
    [Inject] private IRepository Repository { get; set; } = null!;

    private List<Country>? Countries { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Country>>("api/Countries");
        Countries = responseHttp.Response;
    }
}