using UnityEngine;

namespace LightRoomControl
{
    public class SetEvents
    {
        public void OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            if (Global.lightsout_gamemode)
            {
                Global.EnabledGenerators.Add(ev.Generator);
                CheckGeneratorsFinish();
            }
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            try
            {
                Global.lightsout_gamemode = System.Convert.ToBoolean(Global.plugin.GetConfigString("lightsout_gamemode"));
                Global.plugin.Info("<lightsout_gamemode> set from config file: " + Global.lightsout_gamemode.ToString());
            }
            catch (System.FormatException)
            {
                Global.lightsout_gamemode = false;
                Global.plugin.Info("Failed convert <lightsout_gamemode from config file. <lightsout_gamemode> set to default value: " + Global.lightsout_gamemode.ToString());
            }
            Global.running = false;
            Room[] rooms = Global.plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA);
            
            Global.rooms.Clear();
            for (int i = 0; i < rooms.Length; i++)
            {
                Global.plugin.Debug("room: " + rooms[i].RoomType + " type: " + rooms[i].ZoneType);
                if (rooms[i].ZoneType != ZoneType.ENTRANCE)
                    Global.rooms.Add(rooms[i]);
            }
            

            foreach (Smod2.API.Door door in Global.plugin.Server.Map.GetDoors())
            {
                if (door.Name.ToLower().Contains("nuke_surface"))
                {
                    WorkStation[] workS = Object.FindObjectsOfType<WorkStation>();
                    foreach (WorkStation work in workS)
                    {
                        if (Vector3.Distance(work.transform.position, new Vector3(door.Position.x, door.Position.y, door.Position.z)) < Global.distance)
                        {
                            Global.lightPanel = work;
                            break;
                        }
                    }
                    break;
                }
            }
            Global.roomsOffLight.Clear();
            Global.running = true;
            if (!Global.lightsout_gamemode)
                GameObject.FindWithTag("FemurBreaker").AddComponent<TargetUpdate>();
            else
                Global.LightOff();
        }

        private void CheckGeneratorsFinish()
        {
            if (Global.EnabledGenerators.Count == 5)
            {
                Global.LightOn();
                GameObject.FindWithTag("FemurBreaker").AddComponent<TargetUpdate>();
                Global.plugin.Info("Light has been turned on");
            }
        }
    }
}

