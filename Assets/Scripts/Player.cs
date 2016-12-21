using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    //Spaceshipコンポーネント
    Spaceship spaceship;

    //Startメドソッドをコルーチンとして呼び出す
    private IEnumerator Start()
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

    // Update is called once per frame

	void Update () {

    //右・左
    float x = Input.GetAxisRaw("Horizontal");

    float y = Input.GetAxisRaw("Vertical");

        //移動する向きを決める
        Vector2 direction = new Vector2(x, y).normalized;

        // 移動
        spaceship.Move(direction);
        
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
            //爆発する
            spaceship.Explosion();

            //プレーヤーを削除
            Destroy(gameObject);
        }
        
    }
}
