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
        private int _nextRoom;
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
        List<Rectangle> roomRects = new List<Rectangle>();
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


        public int nextRoom
        {
            get { return _nextRoom; }
        }

        public void UpdateMovement()
        {
            if (RoomLocationEnum == Room.dockingBayRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 3);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.dockingBayRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.escapePodBayRoom;

            }

            else if (RoomLocationEnum == Room.escapePodBayRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 5);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.dockingBayRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.escapePodBayRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.hallwayARoom;

                else if (_nextRoom == 4)
                    RoomLocationEnum = Room.hallwayBRoom;
            }

            else if (RoomLocationEnum == Room.hallwayARoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.escapePodBayRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.hallwayARoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.residenceRoom1;

            }

            else if (RoomLocationEnum == Room.hallwayBRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.escapePodBayRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.hallwayBRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.cargoBayRoom;

            }

            else if (RoomLocationEnum == Room.cargoBayRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.hallwayBRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.cargoBayRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.engineRoom;
            }

            else if (RoomLocationEnum == Room.engineRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.cargoBayRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.engineRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.reactorRoom;
            }

            else if (RoomLocationEnum == Room.reactorRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.engineRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.reactorRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.commRoom;
            }

            else if (RoomLocationEnum == Room.residenceRoom1)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.hallwayARoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.residenceRoom1;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.messHallRoom;
            }

            else if (RoomLocationEnum == Room.messHallRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 5);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.residenceRoom1;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.messHallRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.residenceRoom2;

                else if (_nextRoom == 4)
                    RoomLocationEnum = Room.securityRoom;
            }

            else if (RoomLocationEnum == Room.residenceRoom2)
            {
                _nextRoom = _monstGenerator.Next(1, 3);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.messHallRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.residenceRoom2;

            }

            else if (RoomLocationEnum == Room.securityRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 6);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.messHallRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.securityRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.hallwayERoom;

                else if (_nextRoom == 4)
                    RoomLocationEnum = Room.hallwayDRoom;

                else if (_nextRoom == 5)
                    RoomLocationEnum = Room.logRoom;

            }

            else if (RoomLocationEnum == Room.hallwayERoom)
            {
                _nextRoom = _monstGenerator.Next(1, 3);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.securityRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.hallwayERoom;
            }

            else if (RoomLocationEnum == Room.hallwayDRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.securityRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.hallwayDRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.medBayRoom;
            }

            else if (RoomLocationEnum == Room.medBayRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.hallwayDRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.medBayRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.commRoom;
            }

            else if (RoomLocationEnum == Room.hallwayCRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.commRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.hallwayCRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.logRoom;
            }

            else if (RoomLocationEnum == Room.commRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 5);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.medBayRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.commRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.hallwayCRoom;

                else if (_nextRoom == 4)
                    RoomLocationEnum = Room.reactorRoom;
            }

            else if (RoomLocationEnum == Room.logRoom)
            {
                _nextRoom = _monstGenerator.Next(1, 4);

                if (_nextRoom == 1)
                    RoomLocationEnum = Room.hallwayCRoom;

                else if (_nextRoom == 2)
                    RoomLocationEnum = Room.logRoom;

                else if (_nextRoom == 3)
                    RoomLocationEnum = Room.securityRoom;
            }
        }


        public void MapTracker()
        {
            if (RoomLocationEnum == Room.dockingBayRoom)
            {
                _mapLocation = roomRects[0].Center;
            }

            else if (RoomLocationEnum == Room.escapePodBayRoom)
            {
                _mapLocation = roomRects[1].Center;
            }

            else if (RoomLocationEnum == Room.residenceRoom1)
            {
                _mapLocation = roomRects[2].Center;
            }

            else if (RoomLocationEnum == Room.residenceRoom2)
            {
                _mapLocation = roomRects[3].Center;
            }

            else if (RoomLocationEnum == Room.messHallRoom)
            {
                _mapLocation = roomRects[4].Center;
            }

            else if (RoomLocationEnum == Room.securityRoom)
            {
                _mapLocation = roomRects[5].Center;
            }

            else if (RoomLocationEnum == Room.cargoBayRoom)
            {
                _mapLocation = roomRects[6].Center;
            }

            else if (RoomLocationEnum == Room.hallwayARoom)
            {
                _mapLocation = roomRects[7].Center;
            }

            else if (RoomLocationEnum == Room.hallwayBRoom)
            {
                _mapLocation = roomRects[8].Center;
            }

            else if (RoomLocationEnum == Room.hallwayCRoom)
            {
                _mapLocation = roomRects[9].Center;
            }

            else if (RoomLocationEnum == Room.hallwayDRoom)
            {
                _mapLocation = roomRects[10].Center;
            }

            else if (RoomLocationEnum == Room.hallwayERoom)
            {
                _mapLocation = roomRects[11].Center;
            }

            else if (RoomLocationEnum == Room.medBayRoom)
            {
                _mapLocation = roomRects[12].Center;
            }

            else if (RoomLocationEnum == Room.engineRoom)
            {
                _mapLocation = roomRects[13].Center;
            }

            else if (RoomLocationEnum == Room.reactorRoom)
            {
                _mapLocation = roomRects[14].Center;
            }

            else if (RoomLocationEnum == Room.logRoom)
            {
                _mapLocation = roomRects[15].Center;
            }

            else if (RoomLocationEnum == Room.commRoom)
            {
                _mapLocation = roomRects[16].Center;
            }
        }
    }
}
