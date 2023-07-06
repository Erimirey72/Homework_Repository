public class Meeting
{
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Meeting(string title, DateTime startTime, DateTime endTime)
    {
        Title = title;
        StartTime = startTime;
        EndTime = endTime;
    }
}
