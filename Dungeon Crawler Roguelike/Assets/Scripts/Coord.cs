using System;

public class Coord
{
    public int tileX;
    public int tileY;

    public Coord()
    {

    }

    public Coord(int x, int y)
    {
        tileX = x;
        tileY = y;
    }

    public int Distance(Coord other)
    {
        return (int) Math.Ceiling(Math.Sqrt(Math.Pow(tileX - other.tileX, 2) + Math.Pow(tileY - other.tileY, 2)));
    }
}
