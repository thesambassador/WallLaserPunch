using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamUtilities {

    public static bool Between(float val, float lower, float upper, bool inclusive = true)
    {
        if (inclusive && (val == lower || val == upper))
        {
            return true;
        }
        else
        {
            return (val > lower && val < upper);
        }
    }

    public static bool Between(int val, int lower, int upper, bool inclusive = true)
    {
        if (inclusive && (val == lower || val == upper))
        {
            return true;
        }
        else
        {
            return (val > lower && val < upper);
        }
    }

    //assumes that the x of each vector is always smaller than the y
    public static bool RangesOverlap(Vector2 range1, Vector2 range2)
    {
        if (range1.x > range2.x)
        {
            Vector2 swap = range2;
            range2 = range1;
            range1 = swap;
        }

        return range2.x <= range1.y;
    }

    //returns an array containing values from start(inclusive) to end(exclusive) in a random order
    //so calling this with start=2 and end=6 will give an array [2,3,4,5] but with the numbers in a random order.
    public static int[] RandomShuffledRange(int start, int end)
    {
        int length = end - start;
        int[] result = new int[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = i + start;
        }

        result.Shuffle<int>();

        return result;
    }


}
