using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
public class BootAnim : MonoBehaviour
{
    public Sprite[] logos;
    private float delayTime;
    private bool isReady = false;
    private bool animOver = false;
    private int logoIndex = 0;
    private Image imageRenderer;
    private float startTime;
    private LogoState logoState = LogoState.Start;
    private bool mobileTouch = false;
    // Start is called before the first frame update
    void Start()
    {
        imageRenderer = GetComponent<Image>();
        //LevelChanger.Instance.FadeIn();
		EnhancedTouchSupport.Enable();
        isReady = true;
		InputSystem.onAnyButtonPress.CallOnce(_ => animOver = true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady && animOver)
        {
            isReady = false; // no need to call it multiple times
            //LevelChanger.Instance.FadeToLevel("MainMenu");
			SceneManager.LoadScene("MainMenu");
        }

        if (Touch.activeTouches.Count > 0)
        {
            if (Touch.activeTouches[0].phase == TouchPhase.Began) mobileTouch = true;
            else mobileTouch = false;
        }

        if (mobileTouch) animOver = true;

        if (animOver) return;
        // handle state change
        if (Time.unscaledTime > startTime + delayTime)
        {
            startTime = Time.unscaledTime;
            switch (logoState)
            {
                case LogoState.Start:
                    logoState = LogoState.FadeIn;
                    imageRenderer.color = Helper.TransparentColor(imageRenderer.color);
                    delayTime = 0f;
                    break;
                case LogoState.FadeIn:
                    logoState = LogoState.FadeOut;
                    imageRenderer.sprite = logos[logoIndex];
                    imageRenderer.DOFade(1f, 1f).SetUpdate(true);
                    delayTime = 2f;
                    break;
                case LogoState.FadeOut:
                    logoState = LogoState.End;
                    imageRenderer.DOFade(0f, 1f).SetUpdate(true);
                    delayTime = 1f;
                    break;
                case LogoState.End:
                    delayTime = 0f;
                    logoState = LogoState.Start;
                    logoIndex++;
                    if (logoIndex >= logos.Length)
                    {
                        animOver = true;
                    }
                    break;
            }
        }
    }

    enum LogoState
    {
        Start,
        FadeIn,
        FadeOut,
        End
    }
}
