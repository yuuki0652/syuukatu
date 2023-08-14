using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject dmgUI;//プレイヤのダメージ表示
    [SerializeField]
    private GameObject Cam;

    public void Damage(Collider col)
    {
        //DmgUIをインスタンス化、登場位置は、接触したコライダの中心からカメラ方向に少し寄せた位置
        var obj = Instantiate<GameObject>(dmgUI, col.bounds.center - Cam.transform.forward * 0.2f, Quaternion.identity);
    }
}
