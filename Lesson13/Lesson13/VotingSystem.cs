class VotingSystem
{
    private Dictionary<string, VoteTopic> voteTopics;
    private List<Voter> voters;

    public VotingSystem()
    {
        voteTopics = new Dictionary<string, VoteTopic>();
        voters = new List<Voter>();
    }

    public void CreateVoteTopic(string topic)
    {
        if (voteTopics.ContainsKey(topic))
        {
            Console.WriteLine("Vote topic already exists.");
            return;
        }

        VoteTopic newTopic = new VoteTopic(topic);
        voteTopics.Add(topic, newTopic);
        Console.WriteLine("Vote topic created successfully.");
    }

    public void AddOptionToVoteTopic(string topic, string option)
    {
        if (!voteTopics.ContainsKey(topic))
        {
            Console.WriteLine("Vote topic not found.");
            return;
        }

        VoteTopic voteTopic = voteTopics[topic];
        voteTopic.AddOption(option);
    }

    public void RegisterVoter(string name)
    {
        Voter newVoter = new Voter { Name = name };
        voters.Add(newVoter);
        Console.WriteLine("Voter registered successfully.");
    }

    public void VoteOnTopic(string voterName, string topic, string option)
    {
        if (!voteTopics.ContainsKey(topic))
        {
            Console.WriteLine("Vote topic not found.");
            return;
        }

        VoteTopic voteTopic = voteTopics[topic];

        Voter voter = voters.Find(v => v.Name == voterName);
        if (voter == null)
        {
            Console.WriteLine("Voter not found.");
            return;
        }

        if (!voteTopic.Options.Contains(option))
        {
            Console.WriteLine("Invalid option.");
            return;
        }

        voter.Vote(option);
        Console.WriteLine("Vote registered successfully.");
    }

    public void ViewVotingResults(string topic)
    {
        if (!voteTopics.ContainsKey(topic))
        {
            Console.WriteLine("Vote topic not found.");
            return;
        }

        VoteTopic voteTopic = voteTopics[topic];

        Console.WriteLine($"Vote Topic: {voteTopic.Topic}");

        Console.WriteLine("Voting Results:");
        foreach (string option in voteTopic.Options)
        {
            int voteCount = voters.Count(v => v.VotedOption == option);
            Console.WriteLine($"{option}: {voteCount} votes");
        }
    }
}
