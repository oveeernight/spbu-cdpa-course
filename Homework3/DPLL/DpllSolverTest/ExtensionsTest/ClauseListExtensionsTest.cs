using Dpll.Extensions;
using Dpll.SatModels;

namespace DpllTest.ExtensionsTest;

public class ClauseListExtensionsTest
{
    private static object[] testCases =
    {
        new object[]
        {
            new List<Clause> 
            { new(new List<int> { 1, 2}), new(new List<int> { 1, -2 }), new(new List<int> { 1 }) }, 
            new List<int>{1}
        },
        new object[]
        {
            new List<Clause>
            { new(new List<int> { 1, 2, 3}), new(new List<int> { -2, -3 }), new(new List<int> { -1, 4 }) },
            new List<int>{4}
        },
        new object[]
        {
            new List<Clause>
            { new(new List<int> { 1, 2, 3, -4, 5 }) },
            new List<int>{1, 2, 3, -4, 5}
        },
        new object[]
        {
            new List<Clause>
            { new(new List<int> { 1, 2, 3, -4, 5 }), new (new List<int> {-1, 2, -3, -4, 7}) },
            new List<int>{2, -4, 5, 7 }
        },
        new object[]
        {
            new List<Clause> 
                { new(new List<int> { 1, 2, -4, 5 }), new (new List<int>{6, 7}) },
            new List<int>{1, 2, -4, 5, 6, 7}
        },
        new object[]
        {
            new List<Clause> 
                { new(new List<int> { 1, 2, 3 }), new (new List<int>{-1, -2, -3}) },
            new List<int>()
        },
    };
    
    [Test]
    [TestCaseSource(nameof(testCases))]
    public void Test_PureLiterals_EvaluatedCorrectly(List<Clause> clauses, IList<int> expectedPureLiterals)
    {
        var pureLiterals = clauses.GetPureLiterals();
        Assert.AreEqual(expectedPureLiterals.Count, pureLiterals.Count);
        for (var i = 0; i < expectedPureLiterals.Count; i++)
        {
            Assert.AreEqual(expectedPureLiterals[i], pureLiterals[i]);
        }
    }
}