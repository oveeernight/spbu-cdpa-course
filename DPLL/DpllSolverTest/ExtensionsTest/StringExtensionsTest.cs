using Dpll.Extensions;

namespace DpllTest.ExtensionsTest;

public class StringExtensionsTest
{
    private static object[] testCases = {
        new object[] { "1 2 5 -4 0", new List<int>() { 1, 2, 5, -4 } },
        new object[] { "1 0", new List<int>() { 1 } },
        new object[] { "-1 0", new List<int>() { -1 } },
    };
    
    [Test]
    [TestCaseSource(nameof(testCases))]
    public void Test_ToClause_ConvertsCorrectly(string s, IList<int> expectedValues)
    {
        var clause = s.ToClause();
        Assert.AreEqual(expectedValues.Count,clause.Literals.Count);
        for (var i = 0; i < expectedValues.Count; i++)
        {
            Assert.AreEqual(expectedValues[i], clause.Literals[i]);
        }
    }
}