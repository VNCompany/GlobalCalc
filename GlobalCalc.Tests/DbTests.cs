using GlobalCalc.DataLayer;

namespace GlobalCalc.Tests;

[TestFixture]
public class DbTests
{
    [Test]
    public void TestInit()
    {
        using (var context = new DataContext())
        {
            foreach (var pos in context.ProfilePositions.GetAll())
                Console.WriteLine($"{{{pos.Profile.Name}, {pos.Color.Name}, {pos.Price}}}");
        }
    }
}