using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if(Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0){
            // SetTriggerを使用してトリガーを開く
            this.animator.SetTrigger("JumpTrigger"); //animationトリガー
            // transform.rightなら右に飛ぶ
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
        
        // 左右移動
        int key = 0;
        if(Input.acceleration.x > this.threshold) key = 1;
        if(Input.acceleration.x < -this.threshold) key = -1;
        
        // Player Speed
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        
        // Speed制限
        if(speedx < this.maxWalkSpeed){
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }
        
        if(key != 0){
            transform.localScale = new Vector3(key, 1, 1);
        }
        
        //animationトリガー
        //プレイヤの速度に応じてアニメーション速度を変える
        if(this.rigid2D.velocity.y == 0){
            this.animator.speed = speedx / 2.0f;
        } else {
            // 真上にジャンプした時の対策
            this.animator.speed = 1.0f;
        }
        
        if(transform.position.y < -10){
            SceneManager.LoadScene("GameScene");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        print("ゴール！！");
        SceneManager.LoadScene("ClearScene");
    }
}
