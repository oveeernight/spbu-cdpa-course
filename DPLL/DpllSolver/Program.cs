using System.Diagnostics;
using Dpll.Solver;

ISatSolver solver = new DpllSatSolver();
var path = args[0];
var timer = new Stopwatch();
var measurements = new List<double>(40);
var pathToMeasurements = "/home/reflection/study/dotnet/spbu-cdpa-course/Benchmark/python/measurements.txt";
using var sw = new StreamWriter(pathToMeasurements, false);
for (int j = 0; j < 80; j++)
{
    timer.Start();
    var (result, suit) = solver.Solve(path);
    var elapsed = timer.Elapsed;
    Console.WriteLine($"{elapsed.Seconds:00}.{elapsed.Milliseconds:000}");
    timer.Reset();
    if (j > 39)
    {
        sw.WriteLine($"{elapsed.Seconds:00}.{elapsed.Milliseconds:000}");
        measurements.Add(elapsed.Seconds + (elapsed.Milliseconds / 1000.0));
    }
}
