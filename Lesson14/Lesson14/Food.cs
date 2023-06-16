class Food
{
    private Position position;

    public Position Position => position;

    public Food(int x, int y)
    {
        position = new Position(x, y);
    }
}
