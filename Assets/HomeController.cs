using DG.Tweening;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup1;
    [SerializeField] private CanvasGroup _canvasGroup2;
    [SerializeField] private Transform capsul;
    
    public void StartMove()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_canvasGroup1.DOFade(0, 1f).OnComplete(() => _canvasGroup1.gameObject.SetActive(false)));
        sequence.AppendInterval(0.5f);
        sequence.Append(capsul.DOMoveZ(3, 5));
        sequence.AppendInterval(0.5f);
        sequence.Append(capsul.DOMoveY(2, 1)).Join(capsul.DORotate(new Vector3(0, 90, 0), 1));
        sequence.Append(capsul.DOMoveY(1f, 1)).Join(capsul.DORotate(new Vector3(0, 180, 0), 1));
        sequence.AppendInterval(0.5f);
        sequence.Append(capsul.DOMoveZ(0f, 5));
        sequence.AppendInterval(0.5f);
        sequence.Append(_canvasGroup2.DOFade(1, 1f).OnStart(() => _canvasGroup2.gameObject.SetActive(true)));
    }
}
