using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    public Transform Pivot1 = null;
    public Transform LookAt1 = null;
    public Transform Pivot2 = null;
    public Transform LookAt2 = null;
    public Transform MovingPivot = null;
    public Transform Camera = null;

    public float Duration = 2f;
    public float DurationRotaion = 2f;

    public Ease MovingPivotEasing = Ease.Linear;

    IEnumerator Start()
    {
        yield return null;

        Camera.position = Pivot1.position;
        Camera.LookAt(LookAt1);
        MovingPivot.position = LookAt1.position;

        yield return null;

        var s = DOTween.Sequence();
        //s.Append(Camera.DOMove(Pivot2.position, Duration).OnUpdate(()=>{Camera.LookAt(MovingPivot.position);}));
        //s.Join(MovingPivot.DOMove(LookAt2.position, DurationRotaion).SetEase(MovingPivotEasing));
        //s.Append(Camera.DOMove(Pivot1.position, Duration).OnUpdate(() => { Camera.LookAt(MovingPivot.position); }));
        //s.Join(MovingPivot.DOMove(LookAt1.position, DurationRotaion).SetEase(MovingPivotEasing));

        s.Append(MovingPivot.DOMove(LookAt1.position+(LookAt2.position - LookAt1.position) *0.5f, DurationRotaion).SetEase(MovingPivotEasing).OnUpdate(() => { Camera.LookAt(MovingPivot.position); }));
        s.Append(MovingPivot.DOMove(LookAt2.position, DurationRotaion));
        s.Join(Camera.DOMove(Pivot2.position, Duration).OnUpdate(() => { Camera.LookAt(MovingPivot.position); }));

        s.Append(MovingPivot.DOMove(LookAt2.position + (LookAt1.position - LookAt2.position) * 0.5f, DurationRotaion).SetEase(MovingPivotEasing).OnUpdate(() => { Camera.LookAt(MovingPivot.position); }));
        s.Append(MovingPivot.DOMove(LookAt1.position, DurationRotaion));
        s.Join(Camera.DOMove(Pivot1.position, Duration).OnUpdate(() => { Camera.LookAt(MovingPivot.position); }));

        s.SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
