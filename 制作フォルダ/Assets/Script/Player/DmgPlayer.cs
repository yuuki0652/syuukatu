using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject dmgUI;//�v���C���̃_���[�W�\��
    [SerializeField]
    private GameObject Cam;

    public void Damage(Collider col)
    {
        //DmgUI���C���X�^���X���A�o��ʒu�́A�ڐG�����R���C�_�̒��S����J���������ɏ����񂹂��ʒu
        var obj = Instantiate<GameObject>(dmgUI, col.bounds.center - Cam.transform.forward * 0.2f, Quaternion.identity);
    }
}
