using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private Transform _RightHandAnchor;

    [SerializeField]
    private Transform _LeftHandAnchor;

    [SerializeField]
    private Transform _CenterEyeAnchor;

    [SerializeField]
    private float _MaxDistance = 100.0f;

    [SerializeField]
    private LineRenderer _LaserPointerRenderer;

    private Transform Pointer
    {
        get
        {
            // 現在アクティブなコントローラーを取得
            var controller = OVRInput.GetActiveController();
            switch (controller)
            {
                case OVRInput.Controller.RTrackedRemote:
                    Debug.Log("Right");
                    return _RightHandAnchor;

                case OVRInput.Controller.LTrackedRemote:
                    Debug.Log("LEft");
                    return _LeftHandAnchor;

                default:
                    return _CenterEyeAnchor; // どちらも取れなければ目の間からビームが出る
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
        var pointer = Pointer;
        if (pointer == null || _LaserPointerRenderer == null)
        {
            return;
        }
        // コントローラー位置からRayを飛ばす
        Ray pointerRay = new Ray(pointer.position, pointer.forward);

        // レーザーの起点
        _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

        RaycastHit hitInfo;
        if (Physics.Raycast(pointerRay, out hitInfo, _MaxDistance))
        {
            // Rayがヒットしたらそこまで
            _LaserPointerRenderer.SetPosition(1, hitInfo.point);

            Debug.Log(hitInfo.collider.gameObject);
            Debug.Log(hitInfo.transform.gameObject);

            if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
                Destroy(hitInfo.collider.gameObject); 
                Destroy(hitInfo.transform.gameObject); 
            }

        }
        else
        {
            // Rayがヒットしなかったら向いている方向にMaxDistance伸ばす
            _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MaxDistance);
        }

	}
}
