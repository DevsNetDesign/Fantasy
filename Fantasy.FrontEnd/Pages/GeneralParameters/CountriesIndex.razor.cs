using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.FrontEnd.Repositories;
using Fantasy.Shared.Entites.GeneralParameters;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.FrontEnd.Pages.GeneralParameters;

public partial class CountriesIndex
{
    [Inject] private IRepository Repository { get; set; } = null!;

    // [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private List<Country>? Countries { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var responseHppt = await Repository.GetAsync<List<Country>>("api/countries");
        if (responseHppt.Error)
        {
            var message = await responseHppt.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        Countries = responseHppt.Response!;
    }

    private async Task DeleteAsync(Country country)
    {
        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = "Confirmation",
            Text = string.Format("DeleteConfirm", "Country", country.Name),
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            CancelButtonText = "Cancel"
        });
        var confirm = string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }
        var responseHttp = await Repository.DeleteAsync($"api/countries/{country.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
            }
            return;
        }
        await LoadAsync();
        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000,
            ConfirmButtonText = "Yes"
        });
        await toast.FireAsync(icon: SweetAlertIcon.Success, message: "RecordDeletedOk");
    }
}