using System;
using System.Linq;
using System.Reflection;

namespace GlobalCalc.UI.Infrastructure;

public static class SimpleMapper
{
    public static T CloneObject<T>(T obj, params MapperField?[] ignoredFields) where T : new()
    {
        T newObj = new T();
        foreach (FieldInfo fieldInfo in typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (ignoredFields.Any(field => field?.Name == fieldInfo.Name))
                continue;

            object? fieldValue = fieldInfo.GetValue(obj);
            fieldInfo.SetValue(newObj, fieldValue);
        }
        return newObj;
    }
}
