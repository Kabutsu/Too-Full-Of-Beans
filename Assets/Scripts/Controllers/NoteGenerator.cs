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

        [SerializeField]
        private GameObject NoteObject;

        private float y;

        private IEnumerable<Note> notes = new List<Note>();

        void Start()
        {
            y = transform.position.y;

            string fileText = TrackJson.text;
            notes = JsonConvert.DeserializeObject<IEnumerable<Note>>(fileText);

            notes = Helpers.NormalizeJSONPitch(notes);

            StartCoroutine(OutputNotes());
        }

        void Update()
        {

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
                float startTime = Time.time;

                // Wait until the game time matches the note's time
                while (Time.time - startTime < note.Time)
                {
                    yield return null;
                }

                Generate(note.Pitch);
            }
        }
    }
}
