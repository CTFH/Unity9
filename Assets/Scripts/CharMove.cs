using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    Animator animator;
    CharacterController cc;

    Vector3 dir = Vector3.zero;　//高さの方向（y軸方向）のことしたいから用意（ジャンプの概念の為）
    public float gravity = 20.0f;　//インスペクターから調整できるようにpublicにしている
    public float speed = 4.0f;
    public float rotSpeed = 300.0f;
    public float jumpPower = 8.0f;
    void Start()
    {
        animator = GetComponent<Animator>();　//コンポーネントの操作できるように自身のコンポーネントの取得（Awakeでするとデメリットがより少ない）
        cc = GetComponent<CharacterController>();

    }

    void Update()
    {
        //前進成分を取得(0~1),今回はバックはしない
        //acc≒accelerate
        float acc = Mathf.Max(Input.GetAxis("Vertical"), 0f);
        //MathfMaxは大きい数値を返す
        //Input GetAxis（vertical） は上下の矢印キーで下押したときー１上押したとき１が返ってくる
        
        //接地していたら（trueを返す）
        if (cc.isGrounded)
        {
            //左右キーで回転
            float rot = Input.GetAxis("Horizontal");
            //右押すと１が入って左だとー１が返る
            
            //前進、回転が入力されていた場合大きい方の値をspeedにセットする(転回のみをするときも動くモーションをする)
            animator.SetFloat("speed", Mathf.Max(acc, Mathf.Abs(rot)));
            //Animatorはスピードの値によって歩いたり走ったりする

            //回転は直接トランスフォームをいじる
            transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);
            //１秒間に３００度回転　rotSpeed=300

            if (Input.GetButtonDown("Jump"))//Unityで定義されたJump
            {
                //ジャンプモーション開始
                animator.SetTrigger("jump");//自分がつけた名前のjump
                //Triggerは１回Trueになると1回ジャンプして勝手にFalseになる
                //boolは自分で設定しないとtrue false変わらない
            }
        }

        //下方向の重力成分
        dir.y -= gravity * Time.deltaTime;

        //CharacterControllerはMoveでキャラを移動させる。
        cc.Move((transform.forward * acc * speed + dir) * Time.deltaTime);
        //transform forward Unityちゃんが向いてる向き　に　acc=0~1(押してないときは進まない)
        //dir がないと落ちてこなくなっちゃう

        //移動した後着していたらy成分を0にする
        if (cc.isGrounded)
        {
            dir.y = 0;
        }
    }

    //ジャンプモーションで地面から足が離れたときに呼ばれるイベント
    public void OnJumpStart()
    {
        //足が離れたらトランスフォームを上方に移動する。
        dir.y = jumpPower;
    }
}
