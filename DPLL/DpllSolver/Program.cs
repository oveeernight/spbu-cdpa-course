using System.Diagnostics;
using System.Threading.Channels;
using Dpll.SatModels;
using Dpll.Solver;

ISatSolver solver = new DpllSatSolver();
var path = args[0];
var timer = new Stopwatch();
var means = new List<double>(40);
var measurements = new List<double>(40);
var pathToMeasurements = "/home/reflection/study/dotnet/spbu-cdpa-course/Benchmark/python/measurements.txt";
for (int i = 0; i < 40; i++)
{
    using var sw = new StreamWriter(pathToMeasurements, false);
    for (int j = 0; j < 45; j++)
    {
        timer.Start();
        var (result, suit) = solver.Solve(path);
        var elapsed = timer.Elapsed;
        Console.WriteLine($"{elapsed.Seconds:00}.{elapsed.Milliseconds:000}");
        timer.Reset();
        if (j > 5)
        {
            sw.WriteLine($"{elapsed.Seconds:00}.{elapsed.Milliseconds:000}");
            measurements.Add(elapsed.Seconds + (elapsed.Milliseconds / 1000.0));
        }
    }
    
    means.Add(measurements.Sum() / 40);
    measurements.Clear();
    Console.WriteLine($"i = {i}");
}

var std_mean = means.Sum() / 40;
var std_std = Math.Sqrt(means.Select(m => Math.Pow(m - std_mean, 2)).Sum() / 40);
Console.WriteLine(std_std);







