using DG.Tweening;
using UnityEngine;

public class ScalableButton : CustomButtonBase
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    [SerializeField] private float scaleBy = 0.1f;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private Transform target; // optional
    public override void Awake()
    {
        base.Awake();
        if (target == null) target = transform;
        originalScale = target.localScale;
        Vector3 scaleVector = new(Mathf.Sign(originalScale.x) * scaleBy, Mathf.Sign(originalScale.y) * scaleBy, Mathf.Sign(originalScale.z) * scaleBy);
        targetScale = originalScale + scaleVector;
    }
    public override void Normal()
    {
        base.Normal();
        target.DOScale(originalScale, duration).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public override void Pressed()
    {
        base.Pressed();
        target.DOScale(originalScale, duration).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public override void Selected()
    {
        base.Selected();
        target.DOScale(targetScale, duration).SetEase(Ease.InOutSine).SetUpdate(true);
    }
    private void OnDestroy()
    {
        DOTween.Kill(target);
    }
}