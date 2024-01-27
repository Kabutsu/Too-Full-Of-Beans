using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class MusicReader
    {
        public static void Read(string xmlContent)
        {
            JObject xmlObject = JObject.Parse(
                JsonConvert.SerializeXmlNode(
                    new XmlDocument { InnerXml = xmlContent }));

            var track = JsonConvert.DeserializeObject<XmlTrack>(
                xmlObject["score-partwise"]["part"].ToString());

            Debug.Log(JsonConvert.SerializeObject(track));
            Debug.Log(track.Measure.First().Direction?.Sound.Tempo);
            Debug.Log($"Tempo: {float.Parse(track.Measure.First().Direction?.Sound.Tempo)}");
        }
    }

    class XmlTrack
    {
        public ICollection<XmlMeasure> Measure { get; set; }
    }

    class XmlMeasure
    {
        public XmlDirection? Direction { get; set; } = null;
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
        public XmlPitch Pitch { get; set; }
        public int Duration { get; set; }
        public string Type { get; set; }
    }

    class XmlPitch
    {
        public string Step { get; set; }
        public int Octave { get; set; }
        public int? Alter { get; set; } = null;
    }
}
