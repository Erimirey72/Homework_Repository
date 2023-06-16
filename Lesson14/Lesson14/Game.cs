class Game
{
    private const int Width = 40;
    private const int Height = 20;
    private const char SnakeSymbol = '0';
    private const char FoodSymbol = '+';

    private int width;
    private int height;
    private Snake snake;
    private List<Food> foods;
    private Random random;
    private int score;
    private bool gameOver;

    public Game()
    {
        width = Width;
        height = Height;
        snake = new Snake(width / 2, height / 2);
        foods = new List<Food>();
        random = new Random();
        score = 0;
        gameOver = false;
    }

    public void Start()
    {
        gameOver = false;

        Console.Clear();

        DrawStartScreen();
        Console.ReadKey(true);

        Console.Clear();
        InitializeFoods();

        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                snake.ChangeDirection(key);
            }

            snake.Move();
            CheckCollision();
            CheckFood();

            if (!gameOver)
            {
                Draw();
                Thread.Sleep(100);
            }
        }

        Console.Clear();
        DrawGameOverScreen();
        Console.ReadKey(true);
    }

    private void Draw()
    {
        Console.Clear();
        DrawSnake();
        DrawFoods();
        DrawScore();
    }

    private void DrawSnake()
    {
        foreach (var pos in snake.Body)
        {
            Console.SetCursorPosition(pos.X + 1, pos.Y + 1);
            Console.Write(SnakeSymbol);
        }
    }

    private void DrawFoods()
    {
        foreach (var food in foods)
        {
            Console.SetCursorPosition(food.Position.X + 1, food.Position.Y + 1);
            Console.Write(FoodSymbol);
        }
    }

    private void DrawScore()
    {
        Console.SetCursorPosition(0, height + 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Score: " + score);
    }

    private void InitializeFoods()
    {
        int x = random.Next(1, width);
        int y = random.Next(1, height);

        foods.Add(new Food(x, y));
    }

    private void CheckCollision()
    {
        Position head = snake.Body.First();
        int headX = head.X;
        int headY = head.Y;

        if (headX == 0 || headY == 0 || headX == width + 1 || headY == height + 1)
        {
            gameOver = true;
            return;
        }

        if (snake.Body.Skip(1).Any(pos => pos.X == headX && pos.Y == headY))
        {
            gameOver = true;
            return;
        }
    }

    private void CheckFood()
    {
        Position head = snake.Body.First();
        int headX = head.X;
        int headY = head.Y;

        Food food = foods.FirstOrDefault(f => f.Position.X == headX && f.Position.Y == headY);

        if (food != null)
        {
            snake.Eat();
            score += 100;

            foods.Remove(food);

            if (foods.Count == 0)
                InitializeFoods();
        }
    }

    private void DrawStartScreen()
    {
        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.WriteLine("Snake Game");
        Console.SetCursorPosition(width / 2 - 8, height / 2 + 2);
        Console.WriteLine("Press any key to start");
    }

    private void DrawGameOverScreen()
    {
        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.WriteLine("Game Over");
        Console.SetCursorPosition(width / 2 - 7, height / 2 + 2);
        Console.WriteLine("Score: " + score);
        Console.SetCursorPosition(width / 2 - 11, height / 2 + 4);
        Console.WriteLine("Press any key to exit");
    }
}
