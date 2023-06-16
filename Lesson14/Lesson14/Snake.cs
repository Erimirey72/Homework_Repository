class Snake
{
    private List<Position> body;
    private Direction direction;
    private bool hasEaten;

    public Snake(int startX, int startY)
    {
        body = new List<Position>();
        body.Add(new Position(startX, startY));
        direction = Direction.Right;
        hasEaten = false;
    }

    public List<Position> Body => body;

    public Direction Direction => direction;

    public bool HasEaten => hasEaten;

    public void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.W:
                if (direction != Direction.Down)
                    direction = Direction.Up;
                break;
            case ConsoleKey.S:
                if (direction != Direction.Up)
                    direction = Direction.Down;
                break;
            case ConsoleKey.A:
                if (direction != Direction.Right)
                    direction = Direction.Left;
                break;
            case ConsoleKey.D:
                if (direction != Direction.Left)
                    direction = Direction.Right;
                break;
        }
    }

    public void Move()
    {
        Position head = body.First();
        Position newHead;

        switch (direction)
        {
            case Direction.Up:
                newHead = new Position(head.X, head.Y - 1);
                break;
            case Direction.Down:
                newHead = new Position(head.X, head.Y + 1);
                break;
            case Direction.Left:
                newHead = new Position(head.X - 1, head.Y);
                break;
            case Direction.Right:
                newHead = new Position(head.X + 1, head.Y);
                break;
            default:
                newHead = head;
                break;
        }

        body.Insert(0, newHead);

        if (!hasEaten)
            body.RemoveAt(body.Count - 1);
        else
            hasEaten = false;
    }

    public void Eat()
    {
        hasEaten = true;
    }
}
