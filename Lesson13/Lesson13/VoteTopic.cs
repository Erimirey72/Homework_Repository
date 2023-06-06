class VoteTopic
{
    public string Topic { get; set; }
    public List<string> Options { get; set; }

    public VoteTopic(string topic)
    {
        Topic = topic;
        Options = new List<string>();
    }

    public void AddOption(string option)
    {
        Options.Add(option);
    }
}
