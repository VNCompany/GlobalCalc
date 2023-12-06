namespace GlobalCalc.Models.Test
{
    public class TestFacadeData : FacadeData
    {
        public TestFacadeData()
        {
            WorkPrice = 450;

            Screws = new[]
            {
                new Screw
                {
                    Color = "Чёрный",
                    Description = "4*8",
                    Price = 5m
                },
                new Screw
                {
                    Color = "Латунь",
                    Description = "4*8",
                    Price = 5m
                }
            };

            Millings = new[]
            {
                new Milling { Type = MillingType.None, ProfileType = ProfileType.Wide, Name = "Без фрезеровки", Price = 0 },
                new Milling { Type = MillingType.None, ProfileType = ProfileType.Narrow, Name = "Без фрезеровки", Price = 0 },
                new Milling { Type = MillingType.Canopy, ProfileType = ProfileType.Wide, Name = "Под навес", Price = 100 },
                new Milling { Type = MillingType.Canopy, ProfileType = ProfileType.Narrow, Name = "Под навес", Price = 100 },
                new Milling { Type = MillingType.WideProfile, ProfileType = ProfileType.Wide, Name = "Под петлю (широкая рамка)", Price = 50 },
                new Milling { Type = MillingType.NarrowProfile, ProfileType = ProfileType.Narrow, Name = "Под петлю (узкая рамка)", Price = 300 }
            };

            Profiles = new[]
            {
                new Profile
                {
                    Id = 1,
                    Name = "MZ 07",
                    Type = ProfileType.Narrow,
                    CornerPrice = 60,
                    SealPrice = 30,
                    Colors = new[]
                    {
                        new ProfileColor
                        {
                            Article = "А00",
                            Name = "Серебро",
                            Price = 235
                        },
                        new ProfileColor
                        {
                            Article = "А25",
                            Name = "Чёрный браш",
                            Price = 285
                        }
                    }
                },
                new Profile
                {
                    Id = 2,
                    Name = "MZ 13",
                    Type = ProfileType.Wide,
                    CornerPrice = 45,
                    SealPrice = 20,
                    Colors = new[]
                    {
                        new ProfileColor
                        {
                            Article = "А00",
                            Name = "Серебро",
                            Price = 420
                        },
                        new ProfileColor
                        {
                            Article = "А15",
                            Name = "Чёрный",
                            Price = 455
                        }
                    }
                }
            };
        }
    }
}
