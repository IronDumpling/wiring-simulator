using System;

public class Dice{
    private int max = 1;
    private int min = 1;
    public int Max { get { return max;}}
    public int Min { get { return min;}}

    public Dice(int max) {
        this.max = max;
    }

    public Dice(int max, int min) {
        this.max = max;
        this.min = min;
    }

    public int Roll(){
        Random rand = new Random();
        return rand.Next(min, max + 1);
    }
}
