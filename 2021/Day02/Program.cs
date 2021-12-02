int horizontalPosition = 0;
int depthPosition = 0;
int aim = 0;

foreach (string line in System.IO.File.ReadLines("./commands.txt"))
{
    string[] commandArgs = line.Split(" ");
    var length = int.Parse(commandArgs[1]);
    switch (commandArgs[0])
    {
        case "forward":
            horizontalPosition += length;
            break;
        case "down":
            depthPosition += length;
            break;
        case "up":
            depthPosition -= length;
            break;
        default:
            break;
    }
}

Console.WriteLine("Task one result = {0}", horizontalPosition*depthPosition);

horizontalPosition = 0;
depthPosition = 0;
aim = 0;
foreach (string line in System.IO.File.ReadLines("./commands.txt"))
{
    string[] commandArgs = line.Split(" ");
    var length = int.Parse(commandArgs[1]);
    switch (commandArgs[0])
    {
        case "forward":
            horizontalPosition += length;
            depthPosition += (aim * length);
            break;
        case "down":
            aim += length;
            break;
        case "up":
            aim -= length;
            break;
        default:
            break;
    }
}

Console.WriteLine("Task one result = {0}", horizontalPosition*depthPosition);