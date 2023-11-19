using GlobalCalc.DataLayer.Entities;
using GlobalCalc.Models;

namespace GlobalCalc.Web.Extensions;

public static class EntityMappers
{
    public static Models.Screw ToModel(this DataLayer.Entities.Screw entity) =>
        new()
        {
            Color = entity.Color,
            Description = entity.Description,
            Price = entity.Price,
        };

    public static Models.Milling ToModel(this DataLayer.Entities.Milling entity) =>
        new()
        {
            Type = (MillingType)entity.Type,
            ProfileType = (ProfileType)entity.ProfileType,
            Name = entity.Name,
            Price = entity.Price
        };

    public static Models.Profile ToModel(
        this DataLayer.Entities.Profile entity 
        , IEnumerable<Models.ProfileColor> colors
    ) =>
        new()
        {
            Id = entity.Id,
            Type = (ProfileType)entity.Type,
            Name = entity.Name,
            SealPrice = entity.SealPrice,
            CornerPrice = entity.CornerPrice,
            Colors = colors as Models.ProfileColor[] ?? colors.ToArray()
        };

    public static Models.ProfileColor ToModel(this DataLayer.Entities.ProfileColor entity, decimal price) =>
        new()
        {
            Article = entity.Article,
            Name = entity.Name,
            Price = price
        };
}
