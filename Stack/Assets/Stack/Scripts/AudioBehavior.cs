using UnityEngine;


public class AudioBehavior : MonoBehaviour {


    [SerializeField] private AudioClip clipBrick;
    [SerializeField] private AudioClip clipGuitarHigh;
    [SerializeField] private AudioClip clipGuitarMedium;
    [SerializeField] private AudioClip clipWood;
    [SerializeField] private AudioClip clipGuitarXylophone;
    [SerializeField] private AudioClip clipAchievement;


    private AudioSource audioSourceDefault;
    private AudioSource audioSourceBadStack;
    private AudioSource audioSourcePerfectStack;
    private AudioSource audioSourceGrow;

    ///the pitches of the 7 music notes for the first range
    private float[] pitchByMusicNote = {
        1/12f, //C - do
        3/12f, //D - ré
        5/12f, //E - mi
        6/12f, //F - fa
        8/12f, //G - sol
        10/12f, //A - la
        12/12f, //B - si
    };


    void Start() {

        audioSourceDefault = GetComponents<AudioSource>()[0];
        audioSourceBadStack = GetComponents<AudioSource>()[1];
        audioSourcePerfectStack = GetComponents<AudioSource>()[2];
        audioSourceGrow = GetComponents<AudioSource>()[3];
    }

    public void PlaySoundStart() {

        audioSourceDefault.PlayOneShot(clipGuitarHigh);
    }

    public void PlaySoundRetry() {

        audioSourceDefault.PlayOneShot(clipGuitarMedium);
    }

    public void PlaySoundBadStack() {

        audioSourceBadStack.pitch = Random.Range(0.8f, 1.2f);
        audioSourceBadStack.PlayOneShot(clipWood);
    }

    public void PlaySoundPerfectStack(int perfectStackCount) {

        if (perfectStackCount <= 0) {
            throw new System.ArgumentException("Perfect stack count must be positive");
        }

        //the pitch shift start at 0 and the perfect stack count start at 1
        var pitchShift = perfectStackCount - 1;

        //calculate the incrementing pitch depending on the perfect stack count
        var musicNoteCount = (float) pitchByMusicNote.Length;
        var pitchForFirstRange = pitchByMusicNote[(int)(pitchShift % musicNoteCount)];

        audioSourcePerfectStack.pitch = 1 + Mathf.Floor(pitchShift / musicNoteCount) + pitchForFirstRange;
        audioSourcePerfectStack.PlayOneShot(clipGuitarXylophone);
    }

    public void PlaySoundGrowBlock() {

        audioSourceGrow.pitch = Random.Range(0.8f, 1.2f);
        audioSourceGrow.PlayOneShot(clipBrick);
    }

    public void PlaySoundHighScore()
    {

        audioSourceDefault.PlayOneShot(clipAchievement);
    }
}
