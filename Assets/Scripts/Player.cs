using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    //Spaceshipコンポーネント
    Spaceship spaceship;

    //Startメドソッドをコルーチンとして呼び出す
    IEnumerator Start()
    {
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        while (true)
        {
            //弾をプレーヤーと同じ位置/角度で作成
            spaceship.Shot(transform);

            //shot音を鳴らす
            GetComponent<AudioSource>().Play();

            //ShotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);            
            
        }    
    }

	void Update () {

    //右・左
    float x = Input.GetAxisRaw("Horizontal");

	//上・下
    float y = Input.GetAxisRaw("Vertical");

        //移動する向きを決める
        Vector2 direction = new Vector2(x, y).normalized;

		// 移動の制限
		Move (direction);
        
	}

	//機体の移動
	void Move(Vector2 direction) 
	{
		// 画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		// 画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//プレーヤーの座標を取得
		Vector2 pos = transform.position;

		// 移動量を加える
		pos += direction * spaceship.speed * Time.deltaTime;

		//  プレーヤーの位置が画面内に収まるように制限を掛ける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		//  制限を掛けた値をプレーヤーの位置とする
		transform.position = pos;

	}

    //ぶつかった瞬間に呼び出される
    private void OnTriggerEnter2D(Collider2D c)
    {
        //レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        //レイヤー名がBullet(Enemy)のときは弾を削除
        if (layerName == "Bullet(Enemy)")
        {
            //弾の削除
            Destroy(c.gameObject);
        }

        //レイヤー名がBullet(Enemy)またはEnemyの場合は爆発
        if (layerName == "Bullet(Enemy)" || layerName == "Enemy")
        {
			// Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
			FindObjectOfType<Manager>().GameOver();

            //爆発する
            spaceship.Explosion();

            //プレーヤーを削除
            Destroy(gameObject);
        }
        
    }
}
