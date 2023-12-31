using ApexCharts;
using FormulaPredictions.Shared.Models.Charting;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public abstract class BaseChartComponent : BaseRclComponent
{
    [Parameter, EditorRequired]
    public List<ChartDataPoint> Data { get; set; } = default!;

    [Parameter, EditorRequired]
    public EventCallback<ChartDataPoint> SelectionCallback { get; set; }

    protected ApexChart<ChartDataPoint>? Chart;
    protected ApexChartOptions<ChartDataPoint> Options { get; set; } = new();

    protected async Task OnChartItemSelected(SelectedData<ChartDataPoint> selectedData)
    {
        var index = selectedData.DataPointIndex;
        var item = Data[index];

        await SelectionCallback.InvokeAsync(item);
    }
}
