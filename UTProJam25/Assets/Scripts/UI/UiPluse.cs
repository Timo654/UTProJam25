using UnityEngine;
public class UIPULSE : MonoBehaviour
{
    public ParticleSystem particlePulse;

    private void OnEnable()
    {
        SendUIBPMEvent.OnBeat += Pulse;
    }

    private void OnDisable()
    {
        SendUIBPMEvent.OnBeat -= Pulse;
    }

    void Pulse()
    {
        particlePulse.Play();
    }

}
