using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource walking;
    public AudioSource pad;

    public void RunWalkingSound() 
    {
        walking.Play();
    }

    public void RunJumpPadSound() 
    {
        pad.Play();
    }
}