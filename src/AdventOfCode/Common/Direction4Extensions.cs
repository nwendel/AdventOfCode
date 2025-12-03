namespace AdventOfCode.Common;

public static class Direction4Extensions
{
    extension(Direction4 self)
    {
        public Direction4 Turn(Turn2 turn)
            => turn switch
            {
                Turn2.Left => self.TurnLeft(),
                Turn2.Right => self.TurnRight(),
            };

        public Direction4 TurnLeft()
            => (Direction4)(((int)self + 3) % 4);

        public Direction4 TurnRight()
            => (Direction4)(((int)self + 1) % 4);
    }
}
