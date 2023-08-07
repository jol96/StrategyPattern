public class Program
{
    record Game(
        string Title,
        decimal Price,
        decimal Rating,
        DateTime ReleaseDate,
        bool IsAvailable);

    static Func<Game,bool> SelectStrategy(FilteringType filteringType, string searchWord)
    {
        switch (filteringType)
        {
            case FilteringType.ByTitle:
                return game => game.Title.Contains(searchWord);
            case FilteringType.BestGame:
                return game => game.Rating > 95;
            case FilteringType.GamesOfThisYear:
                return game => game.ReleaseDate.Year == DateTime.Now.Year;
            default:
                throw new ArgumentException("invalid option");
        }
    }

    static IEnumerable<Game> FindBy(Func<Game, bool> strategy, IEnumerable<Game> games) 
    {
        return games.Where(g => g.IsAvailable && strategy(g));
    }

    public static void Main()
    {
        var games = new List<Game>()
        {
            new Game("Stardew Valley", 19.99m, 68, new DateTime(2016, 2, 26), true),
            new Game("Red Dead Redemption II", 60.99m, 72, new DateTime(2018, 12, 26), true),
            new Game("Packman", 1.99m, 98, new DateTime(1980, 1, 15), true),
            new Game("Mario", 9.99m, 96, new DateTime(1975, 11, 16), true),
        };

        var selectedOption = FilteringType.BestGame;
        var searchWord = "Red";

        var strategy = SelectStrategy(selectedOption, searchWord);  

        var filteredGames = FindBy(strategy, games);
        foreach(var game in filteredGames)
        {
            Console.WriteLine(game);
        }
    }
}

public enum FilteringType
{
    ByTitle,
    BestGame,
    GamesOfThisYear
}

