using System;

namespace GlobalCalc.UI.Infrastructure;

public class MapperField : IEquatable<MapperField>
{
    public string? Name { get; set; }

    public MapperField(string fieldName)
    {
        Name = fieldName;
    }

    public bool Equals(MapperField? other) => other != null && Name == other.Name;

    public static implicit operator MapperField?(string? name) => name == null ? null : new MapperField(name);

    public static MapperField Field(string fieldName) => new MapperField(fieldName);

    public static MapperField Property(string propertyName) => new MapperField($"<{propertyName}>k__BackingField");
}
