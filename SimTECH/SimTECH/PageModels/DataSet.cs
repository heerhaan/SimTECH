namespace SimTECH.PageModels
{
    public class DataSet
    {
        public string Label { get; set; }
        public ApexCharts.SeriesStroke Stroke { get; set; }
        public List<DataPoint> DataPoints { get; set; } = new();

        public void ClearDataPoints() => DataPoints.Clear();
    }

    public class DataPoint
    {
        public DataPoint(object x, int y)
        {
            XData = x;
            YData = y;
        }

        public object XData { get; set; }
        public int YData { get; set; }
    }
}
