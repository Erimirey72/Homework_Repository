class Voter
{
    public string Name { get; set; }
    public string VotedOption { get; set; }

    public void Vote(string option)
    {
        VotedOption = option;
    }
}
