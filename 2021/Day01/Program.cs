List<int> depths = new List<int>();


var lines = System.IO.File.ReadLines("./depths.txt");
int increases = 0;
for (int i = 1; i < lines.Count(); i++)
{
    if (int.Parse(lines.ElementAt(i-1)) < int.Parse(lines.ElementAt(i))) 
    {
        increases++;
    }
}

Console.WriteLine("Task one equals = {0}", increases);

int increasesV2 = 0;
for (int i = 0; i < lines.Count(); i++)
{
    if (i == (lines.Count() - 3))
    {
        break;
    }

    int curr = int.Parse(lines.ElementAt(i)) + int.Parse(lines.ElementAt(i+1)) + int.Parse(lines.ElementAt(i+2));
    int next = int.Parse(lines.ElementAt(i+1)) + int.Parse(lines.ElementAt(i+2)) + int.Parse(lines.ElementAt(i+3));

    if (curr < next) 
    {
        increasesV2++;
    }
}

Console.WriteLine("Task two equals = {0}", increasesV2);