namespace SimTECH.PageModels.Stats;

public class ChartData
{
    public string Label { get; set; }

    public ApexCharts.SeriesStroke Stroke { get; set; }

    public List<DataPoint> DataPoints { get; set; } = [];
}

public class DataPoint(object x, int y)
{
    public object XData { get; set; } = x;

    public int YData { get; set; } = y;
}
