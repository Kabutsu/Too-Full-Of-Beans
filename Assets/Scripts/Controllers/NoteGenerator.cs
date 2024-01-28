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

        public TextAsset TrackXML;

        [SerializeField]
        private GameObject NoteObject;

        private float y;
        private float StartTime;
        private int zIndex = -2;

        private IEnumerable<Note> notes = new List<Note>();

        void Start()
        {
            y = transform.position.y;

            notes = Helpers.NormalizeJSONPitch(MusicReader.Read(TrackXML.text));

            StartTime = Time.time;

            StartCoroutine(OutputNotes());
        }

        public void Generate(float x)
        {
            Instantiate(
                NoteObject,
                new Vector3(x, y, zIndex),
                transform.rotation);

            Instantiate(
                NoteObject,
                new Vector3(-x, y, zIndex),
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
