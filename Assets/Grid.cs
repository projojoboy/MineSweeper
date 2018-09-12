using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public static int w = 10;
    public static int h = 14;
    public static Elements[,] elements = new Elements[w, h];

    public static void UncoverMines()
    {
        foreach (Elements elem in elements)
        {
            if (elem.mine)
                elem.LoadTexture(0);
        }
    }

    public static bool MineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].mine;

        return false;
    }

    public static int AdjacentMines(int x, int y)
    {
        int count = 0;

        if (MineAt(x, y + 1))       ++count; // top
        if (MineAt(x + 1, y + 1))   ++count; // top-right
        if (MineAt(x + 1, y))       ++count; // right
        if (MineAt(x + 1, y - 1))   ++count; // bottom-right
        if (MineAt(x, y - 1))       ++count; // bottom
        if (MineAt(x - 1, y - 1))   ++count; // bottom-left
        if (MineAt(x - 1, y))       ++count; // left
        if (MineAt(x - 1, y + 1))   ++count; // top-left

        return count;
    }

    public static void FFuncover(int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            if (visited[x, y])
                return;

            elements[x, y].LoadTexture(AdjacentMines(x, y));

            if (AdjacentMines(x, y) > 0)
                return;

            visited[x, y] = true;

            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }

    public static bool IsFinished()
    {
        foreach (Elements elem in elements)
        {
            if (elem.IsCovered() && !elem.mine)
                return false;
        }
        return true;
    }
}
