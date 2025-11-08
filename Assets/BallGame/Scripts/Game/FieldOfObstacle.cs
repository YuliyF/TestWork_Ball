using LitMotion;
using LitMotion.Extensions;
using System.Collections.Generic;
using UnityEngine;
/// <summary> Field of obstacles </summary>
/// -----------------------------------------------------------------------------------------------------------------------
public class FieldOfObstacle : MonoBehaviour
{
    [SerializeField] private Obstacle[] Obstacles;
    List<Obstacle> _listHits;

    [SerializeField] private Material MatDefault;
    [SerializeField] private Material MatHit;
///-----------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        _listHits = new List<Obstacle>();
        foreach (var obstacle in Obstacles) {
            obstacle.OnTrigger = HitItem;
            obstacle.StartPos = obstacle.Tr.position;
        }
    }
    public void ResetObstacles()
    {
        foreach(var ob in _listHits)
            ob.SetNewMaterial(MatDefault);
        _listHits.Clear();
    }
    public void Restart()
    {
        foreach (var obstacle in Obstacles)
        {
            obstacle.SetNewMaterial(MatDefault);
            obstacle.gameObject.SetActive(true);
            obstacle.Tr.position = obstacle.StartPos;
        }
        _listHits.Clear();
    }
/// -----------------------------------------------------------------------------------------------------------------------
    void HitItem(Obstacle obst) {
        _listHits.Add(obst);
    }
    public void SelectionItems()
    {
        foreach(var ob in _listHits)
            ob.SetNewMaterial(MatHit);
    }
    public void BlustItems()
    {
        for(var i=0; i<_listHits.Count; i++)
        {
            var ob = _listHits[i];
            var endValue = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));

            LMotion.Create(ob.Tr.position, endValue, i * 0.1f)
                .WithDelay(i * 0.1f)
                .WithEase(Ease.OutQuad)
                .BindToPosition(ob.Tr);
        }
    }
    public void HideItems()
    {
        foreach(var ob in _listHits)
            ob.gameObject.SetActive(false);
    }
   
}
