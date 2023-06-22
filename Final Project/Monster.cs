using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Random _monstGenerator;
        private Texture2D _texture;
        private Rectangle _rectangle;
        private int _numerizedRoomLocation;
        private Point _mapLocation;
        private Room _room;

        


        public Monster()
        {
            _monstGenerator = new Random();
            _mapLocation = new Point();
            _rectangle = new Rectangle();
            _room = new Room();
            _numerizedRoomLocation = new int();
        }

        private Monster(int RoomLocationNum)
        {
            _numerizedRoomLocation = RoomLocationNum;
        }
        public int RoomLocationNum
        {
            get { return _numerizedRoomLocation; }
            set { _numerizedRoomLocation = value; }
        }


        public Monster(Room RoomLocationEnum)
        {
            _room = RoomLocationEnum;
        }
        public Room RoomLocationEnum
        {
            get { return _room; }
            set { _room = value; }
        }


        public Monster(Point mapLocation, int x, int y)
        {
            _mapLocation = mapLocation;
        }
        public Point MonsterMapLocation
        {
            get { return _mapLocation; }
        }


        List<Room> rooms = new List<Room>();
        public void InitializeLocation()
        {
            _numerizedRoomLocation = _monstGenerator.Next(1, 17);

            rooms.Add(Room.dockingBayRoom);
            rooms.Add(Room.escapePodBayRoom);
            rooms.Add(Room.residenceRoom1);
            rooms.Add(Room.residenceRoom2);
            rooms.Add(Room.messHallRoom);
            rooms.Add(Room.securityRoom);
            rooms.Add(Room.cargoBayRoom);
            rooms.Add(Room.hallwayARoom);
            rooms.Add(Room.hallwayBRoom);
            rooms.Add(Room.hallwayCRoom);
            rooms.Add(Room.hallwayDRoom);
            rooms.Add(Room.hallwayERoom);
            rooms.Add(Room.medBayRoom);
            rooms.Add(Room.engineRoom);
            rooms.Add(Room.reactorRoom);
            rooms.Add(Room.logRoom);
            rooms.Add(Room.commRoom);

            for (int i = 1; i < 17; i++)
            {
                if (_numerizedRoomLocation == i)
                {
                    _room = rooms[i];
                }
            }
        }

        public void UpdateMovement()
        {
            // logic for if in this room, (1 / # of rooms available to move to + currentRoom) chance of moving to other room
        }

    }
}
