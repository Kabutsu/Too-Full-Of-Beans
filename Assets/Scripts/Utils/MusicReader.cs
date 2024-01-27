using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Assets.Scripts.Utils
{
    public static class MusicReader
    {
        private const float TIME_MULTIPLIER = 5.5f;

        public static IEnumerable<Note> Read(string xmlContent)
        {
            JObject xmlObject = JObject.Parse(
                JsonConvert.SerializeXmlNode(
                    new XmlDocument { InnerXml = xmlContent }));

            var track = JsonConvert.DeserializeObject<XmlTrack>(
                xmlObject["score-partwise"]["part"].ToString());

            var tempo = float.Parse(track.Measure.First().Direction?.Sound.Tempo ?? "90.00");
            var noteLength = (60f / tempo) * TIME_MULTIPLIER;

            var notesList = new List<Note>();
            float currentTime = 0.0f;

            foreach (var measure in track.Measure)
            {
                foreach (var note in measure.Note)
                {
                    if (note.Pitch != null)
                    {
                        notesList.Add(new Note
                        {
                            Pitch = note.Pitch.ToMidiNumber(),
                            Time = currentTime,
                            Length = (note.Pitch.Step == "half" || note.Pitch.Step == "quarter")
                                ? note.Type.InSeconds(noteLength)
                                : null,
                        });
                    }

                    currentTime += note.Type.InSeconds(noteLength);
                }
            }

            return notesList;
        }

        private static float InSeconds(this string noteType, float fullNoteLength)
        {
            return noteType switch
            {
                "half" => fullNoteLength / 2f,
                "quarter" => fullNoteLength / 4f,
                "eighth" => fullNoteLength / 8f,
                "16th" => fullNoteLength / 16f,
                _ => throw new System.NotImplementedException(),
            };
        }

        private static int ToMidiNumber(this XmlPitch pitch)
        {
            int baseNote = pitch.Step switch
            {
                "C" => 0,
                "D" => 2,
                "E" => 4,
                "F" => 5,
                "G" => 7,
                "A" => 9,
                "B" => 12,
                _ => throw new System.NotImplementedException(),
            };

            return (pitch.Octave + 1) * 12 + baseNote + (pitch.Alter ?? 0);
        }
    }

    class XmlTrack
    {
        public ICollection<XmlMeasure> Measure { get; set; }
    }

    class XmlMeasure
    {
        public XmlDirection? Direction { get; set; } = null;

        [JsonProperty("note")]
        [JsonConverter(typeof(NoteConverter))]
        public ICollection<XmlNote> Note { get; set; }
    }

    class XmlDirection
    {
        public XmlSound Sound { get; set; }
    }

    class XmlSound
    {
        [JsonProperty("@tempo")]
        public string Tempo { get; set; }
    }

    class XmlNote
    {
        public XmlPitch? Pitch { get; set; }
        public int Duration { get; set; }
        public string Type { get; set; }
    }

    class XmlPitch
    {
        public string Step { get; set; }
        public int Octave { get; set; }
        public int? Alter { get; set; } = null;
    }

    public class NoteConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(XmlNote));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<XmlNote>>();
            }
            else
            {
                var note = token.ToObject<XmlNote>();
                return new List<XmlNote> { note };
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
