namespace AdventOfCode.Common;

public static class Direction4Extensions
{
    public static Direction4 Turn(this Direction4 self, Turn turn)
        => turn switch
        {
            Common.Turn.Left => self.TurnLeft(),
            Common.Turn.Right => self.TurnRight(),
            Common.Turn.Around => self.TurnAround(),
        };

    public static Direction4 TurnLeft(this Direction4 self)
        => (Direction4)(((int)self + 3) % 4);

    public static Direction4 TurnRight(this Direction4 self)
        => (Direction4)(((int)self + 1) % 4);

    public static Direction4 TurnAround(this Direction4 self)
        => (Direction4)(((int)self + 2) % 4);
}