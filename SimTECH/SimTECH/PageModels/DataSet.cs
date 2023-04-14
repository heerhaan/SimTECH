namespace SimTECH.PageModels
{
    public class DataSet
    {
        public string Label { get; set; }
        public ApexCharts.SeriesStroke Stroke { get; set; }
        public List<DataPoint> DataPoints { get; set; } = new();
    }
    
    public class DataPoint
    {
        public DataPoint(int x, int y)
        {
            XData = x;
            YData = y;
        }

        public int XData { get; set; }
        public int YData { get; set; }
    }
}
