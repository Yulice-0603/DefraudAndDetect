using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float speed = 6;
    private Vector3 moveInput;
    Quaternion targetRotation;
    Quaternion horizontalRotation;
    private NetworkVariable<Unity.Collections.FixedString64Bytes> _playerName =
         new NetworkVariable<Unity.Collections.FixedString64Bytes>();

    /*public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            SetPlayerNameServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = true)]
    private void SetPlayerNameServerRpc(string playerName)
    {
        _playerName.Value = playerName;
    }*/
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody>();
        if (this.IsOwner)
        {
            GameObject.FindWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = this.gameObject.transform;
            
        }
        
    }

    void Update()
    {
        if (this.IsOwner)
        {
            //xy軸入力
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var Rotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            //入力情報を取得してサーバに送る
            SetMoveInputServerRpc(horizontal, vertical, Rotation);
        }
        Move();
        if (transform.position.y < -10.0f)
        {
            transform.position = new Vector3(Random.Range(-7, 7), 7, Random.Range(-7, 7));
        }
        /*if (this.IsServer)
        {
            Move();
            if (transform.position.y < -10.0f)
            {
                transform.position = new Vector3(Random.Range(-7, 7), 7, Random.Range(-7, 7));
            }
        }*/
    }
    
    [ServerRpc]
    private void SetMoveInputServerRpc(float x,float z, Quaternion rotation)
    {
        moveInput = new Vector3(x, 0, z);
        horizontalRotation = rotation;
    }
   
    private void Move()
    {
        //速度の方向と正規化
        var velocity = horizontalRotation*new Vector3(moveInput.x, 0, moveInput.z).normalized;
        //移動方向
        if (velocity.magnitude > 0.5f)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        //1フレーム最大の角度を制限して、向きの変更をスムーズに
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500f * Time.deltaTime);
        //rb.velocity = new Vector3(velocity.x,0,velocity.z) * speed;
        transform.position += velocity * speed * Time.deltaTime;
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);
       
    }

}
