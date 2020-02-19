using System;

public class HitObject
{
    private int x;
    private int offset;
    private int endOffset;

    public HitObject()
    {
    }

    public int GetX()
    {
        return x;
    }

    public int GetOffset()
    {
        return offset;
    }

    public int GetEndOffset()
    {
        return endOffset;
    }

    public void SetX(string input)
    {
        x = Convert.ToInt32(input);
    }

    public void SetOffset(string input)
    {
        offset = Convert.ToInt32(input);
    }

    public void SetEndOffset(string input)
    {
        if (input != "")
        {
            endOffset = Convert.ToInt32(input);
        }
        
    }
}
