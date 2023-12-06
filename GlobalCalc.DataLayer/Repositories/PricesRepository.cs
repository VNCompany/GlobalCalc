using Dapper;

using GlobalCalc.DataLayer.Types;

namespace GlobalCalc.DataLayer.Repositories
{
    public class PricesRepository
    {
        private readonly DataContext _c;

        public PricesRepository(DataContext c)
        {
            _c = c;
        }

        public decimal GetPrice(PriceType type)
            => _c.Connection.ExecuteScalar<decimal>("SELECT Price FROM Prices WHERE Id = @Id", new { Id = (int)type });
    }
}
