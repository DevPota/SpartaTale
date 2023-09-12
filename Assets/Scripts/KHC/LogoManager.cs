using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;

public class LogoManager : MonoBehaviour
{
    PlayableDirector PD;

    int markerIndex = 0;

    private void Start()
    {
        PD = GetComponent<PlayableDirector>();
        PD.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            var timelineAsset = PD.playableAsset as TimelineAsset;
            var markers = timelineAsset.markerTrack.GetMarkers().ToArray();
            if (markers.Length > markerIndex)
            {
                PD.Play();
                PD.time = markers[markerIndex].time;
            }
        }
    }

    public void AddMarkerIndex()
    {
        markerIndex++;
    }

    public void ToMenuScene()
    {
        SceneManager.LoadScene(1);
    }
}
