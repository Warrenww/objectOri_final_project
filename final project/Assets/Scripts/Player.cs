using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour, IObservable
    {
        public static Player instance;
        public Rigidbody rb;

        public Texture health_0;
        public Texture health_05;
        public Texture health_1;

        private int HitPoint;
        private int Max_HP = 20;
        private float jumpHeight = 5.0f;
        private float walkSpeed = 4.5f;
        private float startJumpHeight;
        private bool onTheGround = true;

        private Item RightHandItem = new Item();
        private List<Item> ItemList = new List<Item>();
        private List<int> ItemCount = new List<int>();
        private int ItemListIndex = 0;
        void Start()
        {
            instance = this;
            rb = GetComponent<Rigidbody>();
            HitPoint = Max_HP;
        }
        void Update()
        {
            // Player is inside the void, continuously take damage
            if (transform.position.y < -50)
            {
                takeDamage(1);
            }
            // Die
            if (HitPoint <= 0)
            {
                transform.position = new Vector3(0, 20, 0);
                rb.velocity = new Vector3(0, 0, 0);
                HitPoint = Max_HP;
            }
            // Make Player face at mouse
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            if ((lookPos.x - transform.position.x) < 0)
            {
                transform.right = new Vector3(-1, 0, 0);
            }
            else
            {
                transform.right = new Vector3(1, 0, 0);
            }
            if (Block._destroying)
            {
                Destroy();
            }
            if (Block._constructing)
            {
                Construct();
                Block._constructing = false;
            }
            // Change right hand tool
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                GameObject rightHandTool = transform.GetChild(0).gameObject; // Get the hand
                ItemListIndex -- ;
                if (ItemListIndex < 0) ItemListIndex = ItemList.Count-1;
                Item activeItem = ItemList[ItemListIndex];
                RightHandItem = activeItem;
                rightHandTool.GetComponent<Renderer>().material = activeItem.getMaterial();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                GameObject rightHandTool = transform.GetChild(0).gameObject; // Get the hand
                ItemListIndex ++ ;
                if (ItemListIndex > ItemList.Count-1) ItemListIndex = 0;
                Item activeItem = ItemList[ItemListIndex];
                RightHandItem = activeItem;
                rightHandTool.GetComponent<Renderer>().material = activeItem.getMaterial();
            }
        }
        void FixedUpdate()
        {
            Vector3 originalVelocity = rb.velocity;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector3(walkSpeed, originalVelocity.y, originalVelocity.z);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector3(-walkSpeed, originalVelocity.y, originalVelocity.z);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (onTheGround)
                {
                    onTheGround = false; // Prevent double jump
                    startJumpHeight = transform.position.y; // Restore height value when starting jump
                    rb.velocity = new Vector3(originalVelocity.x, jumpHeight, originalVelocity.z);
                }
            }

        }
        void OnGUI()
        {
            // Draw Hp
            float currentHP = HitPoint;
            for (int i = 0; i < 10; i++)
            {
                if (currentHP > 1)
                    GUI.DrawTexture(new Rect(10 + (i * 20), 10, 20, 20), health_1, ScaleMode.ScaleToFit, true, 0);
                else if (currentHP > 0)
                    GUI.DrawTexture(new Rect(10 + (i * 20), 10, 20, 20), health_05, ScaleMode.ScaleToFit, true, 0);
                else
                    GUI.DrawTexture(new Rect(10 + (i * 20), 10, 20, 20), health_0, ScaleMode.ScaleToFit, true, 0);
                currentHP -= 2;
            }
            int itemCount = 0;
            foreach(var item in ItemList)
            {
                // Display Item
                float size = 20;
                if (itemCount == ItemListIndex) size = 25;
                GUI.DrawTexture(new Rect(10, 30 * itemCount + 35, size, size),item.getMaterial().mainTexture, ScaleMode.ScaleToFit, true, 0);
                GUI.Label(new Rect(40, 30 * itemCount + 35, 200, 20), item.displayName()+"*"+ItemCount[itemCount].ToString());
                itemCount ++ ;
            }
        }
        void OnCollisionEnter(Collision other)
        {
            // Hit on block
            if (other.gameObject.tag == "block")
            {
                onTheGround = true; // Reset ability to jimp
                // Calculate height different
                float currentHeight = transform.position.y;
                float deltaHeight = startJumpHeight / 2 - currentHeight / 2;
                if (deltaHeight > 0)
                {
                    takeDamage((int)deltaHeight); // Taking damage
                }
                startJumpHeight = currentHeight; // Reset jumpHeight to prevent continuously take damage
            }
        }
        public void takeDamage(int damage)
        {
            HitPoint -= damage;
        }
        public void PickUpItem(Item item)
        {
            if (ItemList.Count == 0)
            {
                GameObject rightHandTool = transform.GetChild(0).gameObject; // Get the hand
                // Put item on the hand
                rightHandTool.SetActive(true); // Show the hand
                rightHandTool.GetComponent<Renderer>().material = item.getMaterial(); // Display the item
                RightHandItem = item; // Restore item
            }           
            // Add picked up item to item list, if exist add number
            if(ItemList.IndexOf(item) == -1)
            {
                ItemList.Add(item);
                ItemCount.Add(1);
            }
            else
            {
                ItemCount[ItemList.IndexOf(item)] += 1;
            }
            //DisplayObjData.Instance.RenderItemName(listcontent);
        }

        List<IObserve> observers = new List<IObserve>();
        public void Register(IObserve o)
        {
            observers.Add(o);
        }

        public void Remove(IObserve o)
        {
            observers.Remove(o);
        }
        public void Destroy()
        {
            string ItemType = RightHandItem.getItemType();
            foreach (var o in observers)
            {
                o.Destroy(ItemType);
            }
        }

        public void Construct()
        {
            string ItemType = RightHandItem.getItemType();
            if (ItemType != "Block") return;
            GameObject block = (RightHandItem as BlockItem).selfBlock;
            foreach (var o in observers)
            {
                o.Construct(ItemType,block);
            }
        }
    }
}