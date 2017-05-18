using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour
{
    public bool vivo = true;
    public float velHorizontal;
    public Animator anim;
    private float _input;
    private Rigidbody2D _rb;
    public Transform visual;
    public float pulo;
    public bool ar = false;
    public bool quedaRapida = false;

    public float gravidadePadrao = 5;
    public float gravidadeQueda = 8;
    public float gravidadeQuedaRapida = 5;
    public float velMaxQueda = 15;

    public LayerMask mascaraColisao;
    public Transform sensorPe;
    public float raioPe;

    public bool subindo = false;
    public float velSubindo;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        vivo = true;
    }

    void Update()
    {
        if (!vivo)
        {
            return;
        }

        _input = Input.GetAxis("Horizontal");
        if (_input != 0)
        {
            visual.localScale = new Vector3(Mathf.Sign(_input), 1, 1);
        }
        if (Input.GetButtonDown("Jump") && !ar)
        {
            _rb.velocity += new Vector2(0, pulo);
            ar = true;
        }


        if (subindo)
        {
            ar = false;
            var vel = _rb.velocity;

            if (Input.GetButtonDown("Jump"))
            {
                vel += new Vector2(0, pulo);
                ar = true;
                _rb.velocity = vel;
                _rb.gravityScale = gravidadePadrao;
                subindo = false;
                return;
            }

            //print("climb mode");
            if (Mathf.Abs(Input.GetAxis("Vertical")) >= 0.1f)
            {
                //print("vert mov");
                vel.y = velSubindo * Input.GetAxis("Vertical");
            }
            else
            {
                vel.y = 0;
            }

            _rb.velocity = vel;
            _rb.gravityScale = 0;
            return;
        }


        var hit = Physics2D.OverlapCircle(sensorPe.position, raioPe, mascaraColisao);
        //Debug.Log(hit);
        if (hit == null)
        {
            ar = true;
            transform.SetParent(null);
        }
        else
        {
            //Debug.Log(hit.GetComponent<Collider>());
            if (ar /* && hit.collider.CompareTag("Ground")*/)
            {
                ar = false;
                //Debug.Log("ar");
                if (Mathf.Abs(_rb.velocity.y) < 0.1f)
                {
                    Camera.main.SendMessage("GroundTouched", SendMessageOptions.DontRequireReceiver);
                }
                if (hit.CompareTag("PlataformaMovel"))
                {
                    //Debug.Log("plataforma");
                    transform.SetParent(hit.transform);
                }
            }
        }

        //anim.SetBool("climbing", subindo);

    }

    void LateUpdate()
    {
        if (_rb.velocity.y < -gravidadeQuedaRapida)
        {
            quedaRapida = true;
        }
        else
        {
            quedaRapida = false;
        }
    }

    void FixedUpdate()
    {
        var vel = _rb.velocity;
        vel.y = Mathf.Clamp(vel.y, -velMaxQueda, Mathf.Infinity);
        _rb.velocity = new Vector2(_input * velHorizontal, vel.y);
        anim.SetFloat("velocidadeX", Mathf.Abs(_rb.velocity.x));
        anim.SetFloat("velocidadeY", Mathf.Abs(_rb.velocity.y));
        if (subindo)
        {
            return;
        }
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = gravidadeQueda;
        }
        else
        {
            _rb.gravityScale = gravidadePadrao;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sensorPe.position, raioPe);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Morte"))
        {
            vivo = false;
            Debug.Log("GAME OVER");
            GameManager.i.SendMessage("Reload");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            //print("escada");
            SetSubindo(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            SetSubindo(false);
        }
    }

    void SetSubindo(bool state)
    {
        subindo = state;
    }
}
