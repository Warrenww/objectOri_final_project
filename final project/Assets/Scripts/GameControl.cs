using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class GameControl : MonoBehaviour
    {
        public GameObject grass;
        public GameObject dirt;
        public GameObject stone;
        public GameObject player;
        public int playgroundWidth = 100;

        void Start()
        {
            InitializeGame();
            
        }

        void InitializeGame()
        {
            int length = 0;
            // Loopthrough playground and randomly generate grass
            while(length < playgroundWidth)
            {    
                int grass_Height = UnityEngine.Random.Range(1, 2);
                int dirt_Height = UnityEngine.Random.Range(1, 5);
                int stone_Height = UnityEngine.Random.Range(3, 7);
                int Width = UnityEngine.Random.Range(2, 5);
                for(int i = 0; i < Width; i++)
                {
                    int h = 0;
                    for (int j = 0; j < stone_Height; j++)
                    {
                        Vector3 Position = new Vector3(length + i - 49.5f, h + 0.5f, 0);
                        GameObject.Instantiate(stone,Position, Quaternion.identity);
                        h++;
                    }

                    for (int j = 0; j < dirt_Height; j++)
                    {
                        Vector3 Position = new Vector3(length + i - 49.5f, h + 0.5f, 0);
                        GameObject.Instantiate(dirt,Position, Quaternion.identity);
                        h++;
                    }

                    for (int j = 0; j < grass_Height; j++)
                    {
                        Vector3 Position = new Vector3(length + i - 49.5f, h + 0.5f, 0);
                        GameObject.Instantiate(grass,Position, Quaternion.identity);
                        h++;
                    }
                }
                if (length == 0)
                {
                    int Height = stone_Height + dirt_Height + grass_Height;
                    player.transform.position = new Vector3(-49.5f, Height + 0.5f, 0);
                }
                length += Width;
            }
        }
    }
}
