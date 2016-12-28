using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// 弾の移動スピード
    public int speed = 10;
    
	// ゲームオブジェクト生成から削除するまでの時間
	public float lifetime = 1;

	// 攻撃力
	public int power = 1;
    
	void Start ()
    {
		// ローカル座標のY軸方向に移動する
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;    

		// lifetime秒後に削除
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
