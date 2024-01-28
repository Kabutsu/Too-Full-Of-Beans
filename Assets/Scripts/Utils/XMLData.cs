using UnityEngine;

[CreateAssetMenu(fileName = "New XML Data", menuName = "Data/XML Data")]
public class XMLData : ScriptableObject
{
    public TextAsset xmlFile; // You can store the XML file as a TextAsset
    public Sprite imageFile;

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }
}
