using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    
    void LateUpdate()
    {   
        //player.position unityちゃんいる場所
        //player.forward Unityちゃんが向いている方向Z方向
        //player.up プレイヤーから見た上の座標
        //transform.position = player.position + (-player.forward * 3.0f) + (player.up * 1.0f);
        transform.position =
            Vector3.Lerp(transform.position,
            player.position + (-player.forward * 3.0f)
            + (player.up * 1f), Time.deltaTime * 10f);
        //Lerp使用で少し遅れて追いかけるのでよりナチュラルに追いかける　横向き姿少し見えたり

        //LookAt　カメラのZ軸（カメラの向き）を指定した座標（引数）に向けてくれる
        //PlayerPositionだとUnityちゃんの腰らへんだから1メートルプラスする
        //Vector3.up=（0,1,0)
        transform.LookAt(player.position + Vector3.up);
    }
}
