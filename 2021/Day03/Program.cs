class Program
{
    static void Main(string[] args)
    {
        List<uint> report = Diagnostic.GetNumbersFromFile();
        uint mostCommon = Diagnostic.GetMostCommon(report);

        // Print out the most common number both represented as an int and as bits
        Console.WriteLine($"mostCommon:  {Convert.ToString(mostCommon, 2).PadLeft(12, '0')} ({mostCommon})");

        // Negate most common but mask to only the size of the int in bits, so we only count as many bits as we should
        uint leastCommon = (~mostCommon) % (uint)Math.Pow(2, 12);

        // Print the the least common number
        Console.WriteLine($"leastCommon: {Convert.ToString(leastCommon, 2).PadLeft(12, '0')} ({leastCommon})");

        // Solve task
        Console.WriteLine("Task one equals = {0}", mostCommon * leastCommon);

        // Problem 2
        uint OxygenGeneratorRating = Diagnostic.GetOxygenGeneratorRating();
        uint CO2ScrubberRating = Diagnostic.GetCO2ScrubberRating();
        Console.WriteLine("OxygenGenerator Rating: {0}", OxygenGeneratorRating);
        Console.WriteLine("Co2 Scrubber Rating   : {0}", CO2ScrubberRating);

        // Solve task
        Console.WriteLine("Task two equals = {0}", OxygenGeneratorRating * CO2ScrubberRating);
    }
}

class Diagnostic
{
    public static int SizeOfIntInBits = 12;
    public static List<uint> GetNumbersFromFile()
    {
        // Read file with input one line at a time.
        // Convert each line (string) into an integer using ToInt32 with parameter "base 2"
        var lines = System.IO.File.ReadLines("./diagnostic.txt");
        List<uint> diagnosticReport = new List<uint>();
        foreach (var line in lines)
        {
            var number = Convert.ToUInt32(line, 2);
            // Console.WriteLine($"{ Convert.ToString(number, 2).PadLeft(32, '0')}");
            diagnosticReport.Add(number);
        }

        return diagnosticReport;
    }

    public static uint GetMostCommon(List<uint> report, int bitOnEqual=0)
    {
        // For each number of bits go through all lines
        uint mostCommon = 0;
        for (int i = SizeOfIntInBits - 1; i >= 0; i--)
        {
            // Using this mask to only get the bit in the desired place of the int 
            uint mask = (uint)(1 << i);
            int commonBitCounter = 0;
            foreach (var number in report)
            {
                // Using the mask and the number to get the value of the bit in the masked place. Check if 1 or 0
                bool bit = (number & mask) != 0;
                commonBitCounter += bit ? 1 : -1;
            }

            // After going through all lines, check if the most commonBit is a 1
            if (commonBitCounter > 0 || (commonBitCounter == 0 && bitOnEqual == 1))
            {
                // This sets ONLY the bit at the i'th spot to a one, similar to how the mask works
                mostCommon = mostCommon | (uint)(1 << i);
            }
        }

        return mostCommon;
    }

    public static uint GetOxygenGeneratorRating() {
        return GetRating(GetNumbersFromFile(), true, 0);
    }

    public static uint GetCO2ScrubberRating() {
        return GetRating(GetNumbersFromFile(), false, 0);
    }

    public static uint GetRating(List<uint> report, bool mostCommon, int bitPos) {
        if (report.Count() <= 1)
        {
            return report[0];
        }
        
        uint common;
        if (mostCommon)
        {
            common = GetMostCommon(report, 1);
        }
        else
        {
            common = (~GetMostCommon(report, 1)) % (uint)Math.Pow(2, 12);
        }

        // Using this mask to only get the bit in the desired place of the int 
        uint mask = (uint)(1 << (SizeOfIntInBits - 1 - bitPos));
        
        List<uint> newReport = new List<uint>();
        foreach (uint number in report)
        {
            if ((number & mask) == (common & mask))
            {
                newReport.Add(number);   
            }
        }

        return GetRating(newReport, mostCommon, bitPos+1);
    }
}