namespace AdventOfCode._2016;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var instructions = Input.Text.Split(", ");

        var position = new Position2(0, 0);
        var direction = Direction4.North;

        foreach (var instruction in instructions)
        {
            var turn = instruction[0].ToTurn();
            var distance = int.Parse(instruction[1..]);
            direction = direction.Turn(turn);
            position = position.Move(direction, distance);
        }

        return Math.Abs(position.X) + Math.Abs(position.Y);
    }

    protected override object InternalPart2()
    {
        throw new NotImplementedException();
    }
}
