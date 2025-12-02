namespace AdventOfCode.Common;

public static class Direction4Extensions
{
    public static Direction4 Turn(this Direction4 self, Turn2 turn)
        => turn switch
        {
            Turn2.Left => self.TurnLeft(),
            Turn2.Right => self.TurnRight(),
        };

    public static Direction4 Turn(this Direction4 self, Turn3 turn)
        => turn switch
        {
            Turn3.Left => self.TurnLeft(),
            Turn3.Right => self.TurnRight(),
            Turn3.Around => self.TurnAround(),
        };

    public static Direction4 TurnLeft(this Direction4 self)
        => (Direction4)(((int)self + 3) % 4);

    public static Direction4 TurnRight(this Direction4 self)
        => (Direction4)(((int)self + 1) % 4);

    public static Direction4 TurnAround(this Direction4 self)
        => (Direction4)(((int)self + 2) % 4);
}