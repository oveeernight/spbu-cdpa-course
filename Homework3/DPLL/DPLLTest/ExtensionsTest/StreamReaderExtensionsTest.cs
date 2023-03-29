using Dpll.Extensions;

namespace DpllTest.ExtensionsTest;

public class StreamReaderExtensionsTest
{
    [Test]
    public void Test_ToSatFormula_ParsesCorrectly()
    {
        var file = File.Open("ExtensionsTest/example.txt", FileMode.Open, FileAccess.Read);
        using var fileStream = new StreamReader(file);
        var satFormula = fileStream.ToSatFormula();
        Assert.False(satFormula.ContainsEmptyClause);
        Assert.False(satFormula.ContainsPureLiteral);
        Assert.False(satFormula.ContainsUnitLiteral);
    }
    
    [Test]
    public void Test_ToSatFormulaWithPureLiteral_ParsesCorrectly()
    {
        var file = File.Open("ExtensionsTest/examplePureLiteral.txt", FileMode.Open);
        var fileStream = new StreamReader(file);
        var satFormula = fileStream.ToSatFormula();
        Assert.False(satFormula.ContainsEmptyClause);
        Assert.True(satFormula.ContainsPureLiteral);
        Assert.False(satFormula.ContainsUnitLiteral);
    }
    
    [Test]
    public void Test_ToSatFormulaWithUnitLiteral_ParsesCorrectly()
    {
        var file = File.Open("ExtensionsTest/exampleUnitLiteral.txt", FileMode.Open);
        using var fileStream = new StreamReader(file);
        var satFormula = fileStream.ToSatFormula();
        Assert.False(satFormula.ContainsEmptyClause);
        Assert.False(satFormula.ContainsPureLiteral);
        Assert.True(satFormula.ContainsUnitLiteral);
    }
}