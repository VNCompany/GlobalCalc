using GlobalCalc.DataLayer;

namespace GlobalCalc.Web.BL
{
    internal class ProfilesBL
    {
        private readonly DataContext _db;

        public ProfilesBL(DataContext db)
        {
            _db = db;
        }

        public IEnumerable<Models.Profile> GetProfiles()
            => _db.ProfilePositions.GetAll()
            .GroupBy(_ => _.Profile.Id)
            .Select(group =>
            {
                Models.Profile profile = new ProfileMapper().ToModel(group.First().Profile);
                profile.Colors = group
                    .Select(pp =>
                    {
                        Models.ProfileColor profileColor = new ProfileColorMapper().ToModel(pp.Color);
                        profileColor.Price = pp.Price;
                        return profileColor;
                    })
                    .ToArray();
                return profile;
            });
    }
}
