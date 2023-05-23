using System;
using System.Collections.Generic;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

public class FootballPlayer : Person
{
    public int Number { get; set; }
    public double Height { get; set; }

    public FootballPlayer(string name, int age, int number, double height)
        : base(name, age)
    {
        Number = number;
        Height = height;
    }
}

public class Goalkeeper : FootballPlayer
{
    public Goalkeeper(string name, int age, int number, double height)
        : base(name, age, number, height)
    {
    }
}

public class Defender : FootballPlayer
{
    public Defender(string name, int age, int number, double height)
        : base(name, age, number, height)
    {
    }
}

public class Midfield : FootballPlayer
{
    public Midfield(string name, int age, int number, double height)
        : base(name, age, number, height)
    {
    }
}

public class Striker : FootballPlayer
{
    public Striker(string name, int age, int number, double height)
        : base(name, age, number, height)
    {
    }
}

public class Coach : Person
{
    public Coach(string name, int age)
        : base(name, age)
    {
    }
}

public class Referee : Person
{
    public Referee(string name, int age)
        : base(name, age)
    {
    }
}

public class Team
{
    public Coach Coach { get; set; }
    public List<FootballPlayer> Players { get; set; }

    public Team(Coach coach, List<FootballPlayer> players)
    {
        Coach = coach;
        Players = players;
    }

    public double CalculateAverageAge()
    {
        int totalAge = 0;

        foreach (FootballPlayer player in Players)
        {
            totalAge += player.Age;
        }

        return (double)totalAge / Players.Count;
    }
}

public class Game
{
    public string Team1Name { get; set; }
    public string Team2Name { get; set; }
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public Referee Referee { get; set; }
    public List<Goal> Goals { get; set; }

    public Game(string team1Name, string team2Name, Team team1, Team team2, Referee referee)
    {
        Team1Name = team1Name;
        Team2Name = team2Name;
        Team1 = team1;
        Team2 = team2;
        Referee = referee;
        Goals = new List<Goal>();
    }

    public void AddGoal(int minute, FootballPlayer scorer)
    {
        Goals.Add(new Goal(minute, scorer));
    }

    public void PrintResult()
    {
        Console.WriteLine("Result:");
        Console.WriteLine(Team1Name + " " + GetScoreString(Team1) + " : " + Team2Name + " " + GetScoreString(Team2));

        if (Team1GoalsCount() > Team2GoalsCount())
        {
            Console.WriteLine("Team " + Team1Name + " Wins!");
        }
        else if (Team1GoalsCount() < Team2GoalsCount())
        {
            Console.WriteLine("Team " + Team2Name + " Wins!");
        }
        else
        {
            Console.WriteLine("It's a Draw!");
        }

        Console.WriteLine("Referee: " + Referee.Name + " (Age: " + Referee.Age + ")");
    }

    private string GetScoreString(Team team)
    {
        int teamGoalsCount = GetTeamGoalsCount(team);
        return teamGoalsCount + " goal" + (teamGoalsCount != 1 ? "s" : "");
    }

    private int GetTeamGoalsCount(Team team)
    {
        int count = 0;

        foreach (Goal goal in Goals)
        {
            if (goal.Scorer != null && team.Players.Contains(goal.Scorer))
            {
                count++;
            }
        }

        return count;
    }

    private int Team1GoalsCount()
    {
        return GetTeamGoalsCount(Team1);
    }

    private int Team2GoalsCount()
    {
        return GetTeamGoalsCount(Team2);
    }
}

public class Goal
{
    public int Minute { get; set; }
    public FootballPlayer Scorer { get; set; }

    public Goal(int minute, FootballPlayer scorer)
    {
        Minute = minute;
        Scorer = scorer;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Team One name: ");
        string team1Name = Console.ReadLine();

        Console.Write("Team Two name: ");
        string team2Name = Console.ReadLine();

        Console.Write("Coach for Team 1: ");
        string team1CoachName = Console.ReadLine();

        Console.Write("Coach for Team 2: ");
        string team2CoachName = Console.ReadLine();

        Console.Write("Enter the referee's name: ");
        string refereeName = Console.ReadLine();

        List<FootballPlayer> team1Players = GetTeamPlayers(team1Name);
        List<FootballPlayer> team2Players = GetTeamPlayers(team2Name);

        Coach team1Coach = new Coach(team1CoachName, GenerateRandomAge(40, 60));
        Coach team2Coach = new Coach(team2CoachName, GenerateRandomAge(40, 60));
        Referee referee = new Referee(refereeName, GenerateRandomAge(30, 40));

        Team team1 = new Team(team1Coach, team1Players);
        Team team2 = new Team(team2Coach, team2Players);

        Console.WriteLine();
        Console.WriteLine("Team 1: " + team1Name);
        PrintTeamInfo(team1);
        Console.WriteLine("Average Age: " + team1.CalculateAverageAge().ToString("0.00") + " years");

        Console.WriteLine();

        Console.WriteLine("Team 2: " + team2Name);
        PrintTeamInfo(team2);
        Console.WriteLine("Average Age: " + team2.CalculateAverageAge().ToString("0.00") + " years");

        Console.WriteLine();

        Console.WriteLine("Match Begins!");
        Console.WriteLine();

        Game game = new Game(team1Name, team2Name, team1, team2, referee);
        Random random = new Random();

        for (int minute = 1; minute <= 90; minute++)
        {
            if (minute == 45)
            {
                Console.WriteLine("Half Time!");
            }

            if (random.Next(25) < 1) 
            {
                FootballPlayer scorer = GetRandomPlayer(team1, team2, random);
                game.AddGoal(minute, scorer);

                string goalDescription = scorer.Name + " (#" + scorer.Number + ") scored a goal!";
                Console.WriteLine("Minute " + minute + ": " + goalDescription);
            }

            if (random.Next(35) < 0.3) 
            {
                FootballPlayer cardPlayer = GetRandomPlayer(team1, team2, random);
                Console.WriteLine("Minute " + minute + ": " + cardPlayer.Name + " (#" + cardPlayer.Number + ") received a card.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Match Ended!");
        Console.WriteLine();

        game.PrintResult();
    }

    private static List<FootballPlayer> GetTeamPlayers(string teamName)
    {
        List<FootballPlayer> players = new List<FootballPlayer>();

        Console.WriteLine("Enter Players for " + teamName + ":");

        for (int i = 1; i <= 11; i++)
        {
            Console.Write("Player " + i + " Name: ");
            string playerName = Console.ReadLine();

            int playerAge = GenerateRandomAge(18, 35);
            int playerNumber = GenerateRandomNumber(1, 100);
            double playerHeight = GenerateRandomHeight(1.6, 2.0);

            FootballPlayer player = new FootballPlayer(playerName, playerAge, playerNumber, playerHeight);
            players.Add(player);
        }

        return players;
    }

    private static void PrintTeamInfo(Team team)
    {
        Console.WriteLine("Coach: " + team.Coach.Name + " (Age: " + team.Coach.Age + ")");
        Console.WriteLine("Players:");

        foreach (FootballPlayer player in team.Players)
        {
            string position = GetPlayerPosition(player);
            Console.WriteLine(player.Name + " (#" + player.Number + ") - Age: " + player.Age +
                ", Height: " + player.Height.ToString("0.00") + "m, Position: " + position);
        }
        Console.WriteLine();
    }

    private static string GetPlayerPosition(FootballPlayer player)
    {
        if (player is Goalkeeper)
        {
            return "Goalkeeper";
        }
        else if (player is Defender)
        {
            return "Defender";
        }
        else if (player is Midfield)
        {
            return "Midfield";
        }
        else if (player is Striker)
        {
            return "Striker";
        }

        return "Player";
    }

    private static FootballPlayer GetRandomPlayer(Team team1, Team team2, Random random)
    {
        List<FootballPlayer> allPlayers = new List<FootballPlayer>();
        allPlayers.AddRange(team1.Players);
        allPlayers.AddRange(team2.Players);

        return allPlayers[random.Next(allPlayers.Count)];
    }

    private static int GenerateRandomAge(int minAge, int maxAge)
    {
        Random random = new Random();
        return random.Next(minAge, maxAge + 1);
    }

    private static int GenerateRandomNumber(int minNumber, int maxNumber)
    {
        Random random = new Random();
        return random.Next(minNumber, maxNumber + 1);
    }

    private static double GenerateRandomHeight(double minHeight, double maxHeight)
    {
        Random random = new Random();
        return Math.Round(random.NextDouble() * (maxHeight - minHeight) + minHeight, 2);
    }
}