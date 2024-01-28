using UnityEngine;
using UnityEngine.UI;

public class TrackManager : MonoBehaviour
{
    public static TrackManager instance;

    public XMLData xmlData;
    public Image backgroundImage;

    private void Awake()
    {
        // Ensure only one instance of XMLDataManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set the XML data
    public void SetXMLData(XMLData data)
    {
        xmlData = data;
    }

    // Method to get the XML data
    public XMLData GetXMLData()
    {
        return xmlData;
    }
}
