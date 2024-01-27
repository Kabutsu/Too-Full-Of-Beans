using System.ComponentModel;
using System.Reflection;
using System;
using UnityEngine;
using System.Collections.Generic;

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

        public static int ScoreToBeanSpawn(Constants.Scores score)
        {
            return score switch
            {
                Constants.Scores.Diss => 1,
                Constants.Scores.FarOut => UnityEngine.Random.Range(1, 3),
                Constants.Scores.Crunk => UnityEngine.Random.Range(2, 4),
                _ => UnityEngine.Random.Range(4, 6),
            };
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

        public static IEnumerable<Note> NormalizeJSONPitch(IEnumerable<Note> notes)
        {
            List<Note> normalizedNotes = new();

            // Find the minimum and maximum pitch values in the notes
            float minPitch = float.MaxValue;
            float maxPitch = float.MinValue;
            foreach (var note in notes)
            {
                if (note.Pitch < minPitch)
                    minPitch = note.Pitch;
                if (note.Pitch > maxPitch)
                    maxPitch = note.Pitch;
            }

            // Normalize each pitch value to be between 1 and 8
            foreach (var note in notes)
            {
                float normalizedPitch = Remap(note.Pitch, minPitch, maxPitch, 1f, 8f);
                normalizedNotes.Add(new Note { Pitch = normalizedPitch, Time = note.Time });
            }

            return normalizedNotes;
        }

        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}
