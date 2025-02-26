using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPopupBoard : MonoBehaviour
{
    [SerializeField] private float scaleTime;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private List<Transform> animatedObjects;
    [SerializeField] private List<float> waitTimeList;
    [SerializeField] private Button nextButton;

    public void PlayOpeningAnimation(Action onAnimationEnd)
    {
        nextButton.interactable = false;
        ZeroTheScale();
        var sequence = DOTween.Sequence();

        for (var i = 0; i < animatedObjects.Count; i++)
        {
            var obj = animatedObjects[i];
            sequence.AppendCallback(() => obj.DOScale(Vector3.one, scaleTime).SetEase(scaleEase));
            sequence.AppendInterval(waitTimeList[i]);
        }

        sequence.OnComplete(() =>
        {
            nextButton.interactable = true;
            onAnimationEnd?.Invoke();
        });
        sequence.Play();
    }

    private void ZeroTheScale()
    {
        foreach (var obj in animatedObjects)
        {
            obj.localScale = Vector3.zero;
        }
    }
}