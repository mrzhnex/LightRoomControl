using System.Collections.Generic;

namespace LightRoomControl
{
    public static class Global
    {
        public static List<Room> rooms = new List<Room>();
        public static List<Room> roomsOffLight = new List<Room>();
        public static float distance = 10f;
        public static WorkStation lightPanel;
        public static bool running = true;
        public static bool lightsout_gamemode = false;

        //lightsout

        public static List<Generator079> EnabledGenerators = new List<Generator079>();

    }
}