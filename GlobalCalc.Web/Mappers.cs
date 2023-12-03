using Riok.Mapperly.Abstractions;

using Entities = GlobalCalc.DataLayer.Entities;
using Models = GlobalCalc.Models;

namespace GlobalCalc.Web
{
    [Mapper]
    public partial class ScrewMapper
    {
        public partial Models.Screw ToModel(Entities.Screw entity);
    }

    [Mapper]
    public partial class MillingMapper
    {
        public partial Models.Milling ToModel(Entities.Milling entity);
    }

    [Mapper]
    public partial class ProfileMapper
    {
        public partial Models.Profile ToModel(Entities.Profile entity);
    }

    [Mapper]
    public partial class ProfileColorMapper
    {
        public partial Models.ProfileColor ToModel(Entities.ProfileColor entity);
    }
}
