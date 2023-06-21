using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    internal class Monster
    {
        private Random generator;
        private Texture2D _texture;
        private Rectangle _rectangle;
        private int _numerizedRoomLocation;
        private Point _mapLocation;
        private Room _room;
        

        public Monster(Texture2D texture, int x, int y)
        {
            x = 200 ; y = 100;
            _texture = texture;
            _rectangle = new Rectangle(x, y, 875, 800);
        }

        public Monster(int RoomLocationNum)
        {
            _numerizedRoomLocation = RoomLocationNum;
        }

        public Monster(Room RoomLocationEnum)
        {
            _room = RoomLocationEnum;
        }

        public Monster(Point mapLocation, int x, int y)
        {
            _mapLocation = mapLocation;
        }

        public int RoomLocationNum
        {
            get { return _numerizedRoomLocation; }
            set { _numerizedRoomLocation = value; }
        }

        public Room RoomLocationEnum
        {
            get { return _room; }
            set { _room = value; }
        }

        public Point MonsterMapLocation
        {
            get { return _mapLocation; }
            set { _mapLocation = value; }
        }

        private void InitializeLocation()
        {
            RoomLocationNum = generator.Next(0, 18);
            
            for (int i = 1; i < 18; i++)
            {
                if (RoomLocationNum == i)
                {
                    RoomLocationEnum = Rooms[i];
                }
            }
        }

        public void UpdateMovement()
        {
            // logic for if in this room, (1 / # of rooms available to move to + currentRoom) chance of moving to other room
        }

    }
}
