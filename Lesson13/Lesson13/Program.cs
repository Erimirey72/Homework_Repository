
class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Voting System!");

        VotingSystem votingSystem = new VotingSystem();

        while (true)
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Create a new vote topic");
            Console.WriteLine("2. Add option to a vote topic");
            Console.WriteLine("3. Register a voter");
            Console.WriteLine("4. Vote on a topic");
            Console.WriteLine("5. View voting results");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice (1-6): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the vote topic: ");
                    string topic = Console.ReadLine();
                    votingSystem.CreateVoteTopic(topic);
                    break;
                case "2":
                    Console.Write("Enter the vote topic: ");
                    string topicToAddOption = Console.ReadLine();
                    Console.Write("Enter the option to add: ");
                    string option = Console.ReadLine();
                    votingSystem.AddOptionToVoteTopic(topicToAddOption, option);
                    break;
                case "3":
                    Console.Write("Enter the voter name: ");
                    string voterName = Console.ReadLine();
                    votingSystem.RegisterVoter(voterName);
                    break;
                case "4":
                    Console.Write("Enter the voter name: ");
                    string voterNameToVote = Console.ReadLine();
                    Console.Write("Enter the vote topic: ");
                    string topicToVote = Console.ReadLine();
                    Console.Write("Enter the option to vote: ");
                    string optionToVote = Console.ReadLine();
                    votingSystem.VoteOnTopic(voterNameToVote, topicToVote, optionToVote);
                    break;
                case "5":
                    Console.Write("Enter the vote topic: ");
                    string topicToViewResults = Console.ReadLine();
                    votingSystem.ViewVotingResults(topicToViewResults);
                    break;
                case "6":
                    Console.WriteLine("Thank you for using the Voting System. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
