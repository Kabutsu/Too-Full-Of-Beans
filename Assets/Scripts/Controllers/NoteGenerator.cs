using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class NoteGenerator : MonoBehaviour
    {
        public float MaxX { get; set; }
        public float MinX { get; set; }

        public TextAsset TrackJson;
        public TextAsset TrackXML;

        [SerializeField]
        private GameObject NoteObject;

        private float y;
        private float StartTime;

        private IEnumerable<Note> notes = new List<Note>();

        void Start()
        {
            y = transform.position.y;

            string fileText = TrackJson.text;

            MusicReader.Read(TrackXML.text);

            notes = JsonConvert.DeserializeObject<IEnumerable<Note>>(fileText);

            notes = Helpers.NormalizeJSONPitch(notes);

            StartTime = Time.time;

            StartCoroutine(OutputNotes());
        }

        public void Generate(float x)
        {
            Instantiate(
                NoteObject,
                new Vector2(x, y),
                transform.rotation);

            Instantiate(
                NoteObject,
                new Vector2(-x, y),
                transform.rotation);
        }

        private IEnumerator OutputNotes()
        {
            foreach (var note in notes)
            {
                // Wait until the game time matches the note's time
                while (Time.time - StartTime < note.Time)
                {
                    yield return null;
                }

                Generate(note.Pitch);
            }
        }
    }
}
