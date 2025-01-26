using UnityEngine;
using UnityEngine.Events;
public class UIPULSE : MonoBehaviour
{
    UnityEvent pulseTrigger;
    public ParticleSystem particlePulse;

    void Start()
    {
        if (pulseTrigger == null)
        pulseTrigger = new UnityEvent();

        pulseTrigger.AddListener(Pulse);
    }



    void Pulse()
    {
        particlePulse.Play(); 
    }

}
