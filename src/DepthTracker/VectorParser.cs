using DepthTracker.Utils;

namespace DepthTracker;

public class VectorParser
{
    public CourseVector CreateFrom(string command)
    {
        return command.Split(" ") switch
        {
            ("forward", var magnitude) => new CourseVector { X = int.Parse(magnitude) },
            ("down", var magnitude) => new CourseVector { Z = int.Parse(magnitude) },
            ("up", var magnitude) => new CourseVector { Z = int.Parse("-" + magnitude) },
            _ => throw new NotImplementedException()
        };
    }
}