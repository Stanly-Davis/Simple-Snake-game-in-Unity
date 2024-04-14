
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{

    private Rigidbody2D rb2;

    private GameObject[] go;
    private Rigidbody2D[] rb;

    private float[,] position;


    public Texture2D tex;
    private Sprite mySprite;
    private SpriteRenderer sr;
    private int SnakeLength=5; // 0 to 4 ,according to array


    
    private int BodyObject=-1;
    private int collision=0;

    private bool BorderCollision = false;
    private int ButtonNumber=1;
    private float head_x=0,head_y=4;

    void Start()
    {

        rb2= GetComponent<Rigidbody2D>();
        rb2.position =new Vector2(head_x,head_y);

        position= new float[SnakeLength,2];
        // position= new int[4,2] { {0,0},{0,0},{0,0},{0,0} };

        go = new GameObject[SnakeLength];
        rb = new Rigidbody2D[SnakeLength];

        ObjectCreate();


    }


    void Update()
    {

        if(Input.GetKeyDown(KeyCode.UpArrow))
	    {
            ButtonNumber=1;
	    }
        if(Input.GetKeyDown(KeyCode.DownArrow))
	    {
            ButtonNumber=2;
	    }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
	    {
            ButtonNumber=3;
	    }
        if(Input.GetKeyDown(KeyCode.RightArrow))
	    {
            ButtonNumber=4;
	    }


    }


    void FixedUpdate()
    {

            if(ButtonNumber==1) 
            {
                head_y+=0.5f;
            }
            if(ButtonNumber==2)
            {
                head_y+=-0.5f;
            }
            if(ButtonNumber==3)
            {
                head_x+=-0.5f;
            }
            if(ButtonNumber==4)
            {
                head_x+=0.5f;
            }


            // rb2.position = new Vector2(head_x,head_y)*Time.fixedDeltaTime;
            rb2.position = new Vector2(head_x,head_y);


            if(BodyObject>=0 && BodyObject<SnakeLength)
            {

                for(int i=0; i<=BodyObject; i++)
                {
                    // Debug.Log("      rb["+i+"]            "+position[i,0]+" "+position[i,1]);

                //    rb[i].position = new Vector2(position[i,0],position[i,1])*Time.fixedDeltaTime;
                   rb[i].position = new Vector2(position[i,0],position[i,1]);

                   if(head_x == position[i,0] && head_y==position[i,1])
                   {
                       Debug.Log("collision");
                   }

                }


                for(int i=BodyObject ; i>=0; i--)
                {
                    if(i==0)
                    {
                        position[i,0] = head_x;
                        position[i,1] = head_y;
                    }
                    else
                    {
                        position[i,0] = position[i-1,0];
                        position[i,1] = position[i-1,1];
                    }

                }
                

            }


            // if(BorderCollision)
            // if(head_x >= 28.02 || head_x <= -25.14 || head_y >= 25.77 || head_y <= -25.77 )

            if(head_x >=8.92 || head_x <= -5.04 || head_y >= 6.64 || head_y <= -6.67 )
        {

            if(ButtonNumber==1)
            {
                head_y = -6.52f;
                // head_y = -25.52f;
                BorderCollision=false;
            }
            if(ButtonNumber==2)
            {
                head_y = 6.52f;
                // head_y = 25.52f;
                BorderCollision=false;
            }
            if(ButtonNumber==3)
            {
                head_x = 8.9f;
                // head_x = 28.9f;
                BorderCollision=false;
            }
            if(ButtonNumber==4)
            {
                head_x = -4.98f;
                // head_x = -25.98f;
                BorderCollision=false;
            }
        }

    }



    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Box1")
        {
            Destroy(other.gameObject.GetComponent<BoxCollider2D>());
    
            BodyObject += 1;

            collision += 1;
            ObjectCreate();
        }

        // if(other.gameObject.tag == "Box2")
        // {
        //     BorderCollision=true;

        //     if(ButtonNumber==1)
        //     {
        //         head_y = -6.52f;
        //         // head_y =-25.52f;
        //     }
        //     if(ButtonNumber==2)
        //     {
        //         head_y = 6.52f;
        //         // head_y = 25.52f;
        //     }
        //     if(ButtonNumber==3)
        //     {
        //         head_x = 8.9f;
        //         // head_x = 28.9f;
        //     }
        //     if(ButtonNumber==4)
        //     {
        //         head_x = -4.98f;
        //         // head_x = -25.98f;
        //     }

        // }


    }

    private void ObjectCreate()
    {
        if(collision<SnakeLength)
       {
            go[collision] = new GameObject();
            go[collision].name = "Object_"+collision;
            go[collision].tag = "Box1";

    
            go[collision].AddComponent<Rigidbody2D>();
            rb[collision] = go[collision].GetComponent<Rigidbody2D>();
            rb[collision].bodyType = RigidbodyType2D.Kinematic;


            sr = go[collision].AddComponent<SpriteRenderer>();
            sr.color = Color.black;
            tex = new Texture2D(128,128);
            mySprite=Sprite.Create(tex,new Rect(0.0f,0.0f,tex.width,tex.height),new Vector2(0.5f,0.5f),110.0f);
            sr.sprite=mySprite;


            position[collision,0] =Random.Range(-5,5);
            position[collision,1] =Random.Range(-5,5);
            

            rb[collision].position  = new Vector2( position[collision,0],position[collision,1]);

            // rb[collision].position  = new Vector2(Random.Range(-5,5),Random.Range(-5,5) );
          

            go[collision].AddComponent<BoxCollider2D>();

        }

    }





}