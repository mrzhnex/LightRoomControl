using Smod2.API;
using UnityEngine;
using ServerMod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System;

namespace LightRoomControl
{
    class ToggleCurrentRoomCommand : ICommandHandler
    {
        public ToggleCurrentRoomCommand(MainSettings plugin)
        {
            Global.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            return "custom toggle light";
        }

        public string GetUsage()
        {
            return "light id/nickname | light <off>/<on>";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (args.Length < 1)
            {
                return new string[] { "out of argument. Usage: " + GetUsage() };
            }

            string name = "";
            int id;
            try
            {
                id = Convert.ToInt16(args[0]);
            }
            catch (Exception)
            {
                id = -1;
                name = args[0];
            }
            if (args[0] == "off")
            {
                LightOff();
                return new string[] { "turn off light" };
            }
            else if (args[0] == "on")
            {
                LightOn();
                return new string[] { "turn on light" };
            }
            else
            {
                if (id == -1)
                {
                    foreach (Player p in Global.plugin.Server.GetPlayers())
                    {
                        if (p.Name.ToLower().Contains(name.ToLower()))
                        {
                            foreach (Room room in Global.rooms)
                            {
                                if (Vector3.Distance((p.GetGameObject() as GameObject).transform.position, room.Position.ToVector3()) < Global.distance)
                                {
                                    if (!Global.roomsOffLight.Contains(room))
                                    {
                                        Global.roomsOffLight.Add(room);
                                        return new string[] { "turn off light from this room" };
                                    }
                                    else
                                    {
                                        Global.roomsOffLight.Remove(room);
                                        return new string[] { "turn on light from this room" };
                                    }
                                }
                            }
                            return new string[] { "current room so long" };
                        }
                    }
                }
                else
                {
                    foreach (Player p in Global.plugin.Server.GetPlayers())
                    {
                        if (p.PlayerId == p.PlayerId)
                        {
                            foreach (Room room in Global.rooms)
                            {
                                if (Vector3.Distance((p.GetGameObject() as GameObject).transform.position, room.Position.ToVector3()) < Global.distance)
                                {
                                    if (!Global.roomsOffLight.Contains(room))
                                    {
                                        Global.roomsOffLight.Add(room);
                                        return new string[] { "turn off light from this room" };
                                    }
                                    else
                                    {
                                        Global.roomsOffLight.Remove(room);
                                        return new string[] { "turn on light from this room" };
                                    }
                                }
                            }
                            return new string[] { "current room so long" };
                        }
                    }
                }
                return new string[] { "player not found" };
            }
        }

        private void LightOff()
        {
            Global.roomsOffLight = new List<Room>();
            List<Room> tempRooms = new List<Room>();
            foreach (Room room in Global.rooms)
            {
                room.FlickerLights();
                tempRooms.Add(room);
            }
            Global.roomsOffLight = tempRooms;
        }

        private void LightOn()
        {
            Global.roomsOffLight = new List<Room>();
        }
    }
}
