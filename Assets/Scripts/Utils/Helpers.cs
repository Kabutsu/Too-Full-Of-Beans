using System.ComponentModel;
using System.Reflection;
using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Helpers
    {
        public static Constants.Scores CalculateScore(this Collider2D collider, Transform playerTransform)
        {
            var distance = Vector2.Distance(
                collider.transform.position,
                playerTransform.position);

            if (distance >= 0.5f) return Constants.Scores.Diss;
            if (distance >= 0.25f) return Constants.Scores.FarOut;
            if (distance >= 0.01f) return Constants.Scores.Crunk;

            return Constants.Scores.Tubular;
        }

        public static string GetName(Enum value)
        {
           
            Type enumType = value.GetType(); string name = Enum.GetName(enumType, value);

            if (name != null)
            {
                FieldInfo field = enumType.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                    else
                    {
                        // If there is no Description attribute, format the enum value with spaces
                        return FormatEnumValue(name);
                    }
                }
            }

            // If for some reason the value is not found or other unexpected scenario, return the raw enum value
            return value.ToString();
        }

        public static string FormatEnumValue(string input)
        {
            // Add spaces between PascalCase words
            string output = string.Join(" ", System.Text.RegularExpressions.Regex.Split(input, @"(?<!^)(?=[A-Z])"));
            return output;
        }
    }
}
