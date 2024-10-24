using System.Reflection;

namespace Playwright.DotNet.Configuration.Options;

/// <summary>
/// Provides an easy and safe way to convert the timeout settings from seconds to milliseconds.
/// </summary>
public static class TimeoutOptionsExtensionss
{
    public static TimeoutOptions InMilliseconds(this TimeoutOptions settings)
    {
        TimeoutOptions clonedObject = new();

        PropertyInfo[] properties = typeof(TimeoutOptions).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType == typeof(int))
            {
                int originalValue = (int)property.GetValue(settings);

                property.SetValue(clonedObject, originalValue * 1000);
            }
        }

        return clonedObject;
    }
}
