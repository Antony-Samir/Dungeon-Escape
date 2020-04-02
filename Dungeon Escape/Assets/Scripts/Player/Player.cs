using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    //[SerializeField]
    //private AudioClip _jumpClip;
    
    [SerializeField]
    private AudioClip winClip;
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private AudioClip attackClip;
    [SerializeField]
    private AudioClip attackFireClip;


    public int diamonds;
    private Rigidbody2D _rigid;

    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;
    private bool _grounded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public bool fireAttack = false;
    public int Health { get; set; }
 
    
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        Health = 4;
    }

    void Update()
    {
        if (Health < 1)
        {
            return;
        }
        EndGame();
        Movement();

        //for Android
        if ((CrossPlatformInputManager.GetButtonDown("A_Button")) && IsGrounded() == true)
        {
            if (GameManager.Instance.player.fireAttack == true)
            {
                AudioSource.PlayClipAtPoint(attackFireClip, this.transform.position, 1f);
            }
            else
            {
                AudioSource.PlayClipAtPoint(attackClip, this.transform.position, 1f);
            }
            _playerAnim.Attack();
        }
        //for Windows
        /*if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            if (GameManager.Instance.player.fireAttack == true)
            {
                AudioSource.PlayClipAtPoint(attackFireClip, this.transform.position, 1f);
            }
            else
            {
                AudioSource.PlayClipAtPoint(attackClip, this.transform.position, 1f);
            }
            _playerAnim.Attack();
        }*/

    }

    private void EndGame()
    {
        
        if ((this.transform.position.x >= 127.785f) && (this.transform.position.y <= -9f))
        {
            if (GameManager.Instance.HasKeyToCastle == true)
            {
                Debug.Log("YOU WIN!");
            }
            else
            {//No Key Yet
                this.transform.position = new Vector3(127.785f, -9.535f, 0f);
            }
        }

        


    }
    public Text winTxt;
    public void ShowKey()
    {
        winTxt.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        if (other.tag == "WinDiamond")
        {
            AudioSource.PlayClipAtPoint(winClip, this.transform.position, 1f);
            ShowKey();
            Debug.Log("WINNNNNNNNNNNNNN");
        }
        if (other.tag == "Hole")
        {
            Damage();
            this.transform.position = new Vector3(-30f, 0.5f, 0f);
        }
        if (other.tag == "Lava")
        {
            Damage();
            this.transform.position = new Vector3(80f, -10.535f, 0f);
        }

    }





    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Android release
        //float move = Input.GetAxisRaw("Horizontal"); //Windows release

        _grounded = IsGrounded(); 

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }
        //For Both (Android & Windows) release
        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded() == true)
        {
            //Debug.Log("Jump!");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpNeededRoutine());
            _playerAnim.Jump(true);
            //AudioSource.PlayClipAtPoint(_jumpClip, this.transform.position, 1f);
        }        

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnim.Move(move);
    }
    

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);

        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    void Flip(bool faceRight)
    {

        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;

        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }


    }




    IEnumerator ResetJumpNeededRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }

        Debug.Log("Player::Damage()");
        Health--;
        UIManager.Instance.UpdateLives(Health);

        if (Health < 1)
        {
            AudioSource.PlayClipAtPoint(deathClip, this.transform.position, 1f);

            _playerAnim.Death();
        }

    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.updateGemCount(diamonds);
    }
          
}
