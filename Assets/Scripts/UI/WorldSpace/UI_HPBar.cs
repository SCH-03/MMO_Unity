using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects 
    { 
        HPBar
    }

    Stat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));        
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.Hp / (float)_stat.MaxHp; // 인트 끼리의 계산은 소수점을 날리기때문에 0.5인 경우 0이 되어 소수점 반영을 위해 둘중 하나를 float형으로 캐스팅
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
        
}
