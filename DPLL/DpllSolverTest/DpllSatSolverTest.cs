using Dpll.Extensions;
using Dpll.SatModels;
using Dpll.Solver;

namespace DpllTest;

public class Tests
{
    private ISatSolver solver = new DpllSatSolver();
    
    [SetUp]
    public void Setup()
    {
        solver = new DpllSatSolver();
    }
    
    private static IEnumerable<string> unsatCases = new[]
    {
        "TestFiles/unsat_1_2.txt", "TestFiles/unsat_2_3.txt", "TestFiles/unsat_50_100.txt",
        "TestFiles/unsat_100_160.txt", "TestFiles/unsat_100_200.txt", "TestFiles/unsat_155_1135.txt",
    };

    [Test]
    [TestCaseSource(nameof(unsatCases))]
    public void Test_UnsatCase_ReturnsUnsat(string filePath)
    {
        var (result, _) = solver.Solve(filePath);
        Assert.AreEqual(SatResult.Unsat, result);
    }

    private static IEnumerable<string> satCases = new[]
    {
        "TestFiles/sat_200_680.txt", "TestFiles/example.txt", "TestFiles/examplePureLiteral.txt", "TestFiles/exampleUnitLiteral.txt",
        "TestFiles/sat_20_91.txt", "TestFiles/sat_50_80.txt","TestFiles/sat_50_80_2.txt", "TestFiles/sat_50_170.txt",
        "TestFiles/sat_200_1200.txt", "TestFiles/sat_100_499.txt",  "TestFiles/sat_100_449.txt"
    };
    
    [Test]
    [TestCaseSource(nameof(satCases))]
    public void Test_SatCase_ReturnsCorrectSuit(string filePath)
    {
        var file = File.Open(filePath, FileMode.Open);
        using var streamReader = new StreamReader(file);
        var satFormula = streamReader.ToSatFormula();
        file.Close();
        var (result, suit) = solver.Solve(filePath);
        Assert.AreEqual(SatResult.Sat, result);
        Assert.True(suit?.IsSatisfiableFor(satFormula));
    }
}