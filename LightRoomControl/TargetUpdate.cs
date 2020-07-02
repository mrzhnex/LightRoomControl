using Smod2.API;
using System.Collections.Generic;
using UnityEngine;
using RemoteAdmin;
using System;

namespace LightRoomControl
{
    class TargetUpdate : MonoBehaviour
    {
        private bool prevLightStatus = true;
        private float timer = 0f;

        public void Update()
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                timer = 0f;
                try
                {
                    if (Global.lightPanel.isTabletConnected && prevLightStatus)
                    {
                        foreach (Player p in Global.plugin.Server.GetPlayers())
                        {
                            if (p.PlayerId == Global.lightPanel.NetworkplayerConnected.GetComponent<QueryProcessor>().PlayerId)
                            {
                                p.PersonalBroadcast(10, "Вы выключили свет в комплексе", true);
                                Global.plugin.Server.Map.AnnounceCustomMessage("The lights have been turned off by system", true);
                                break;
                            }
                        }
                        Global.roomsOffLight = new List<Room>();
                        List<Room> tempRooms = new List<Room>();
                        foreach (Room room in Global.rooms)
                        {
                            tempRooms.Add(room);
                        }
                        Global.roomsOffLight = tempRooms;
                        prevLightStatus = false;
                    }
                    else if (!Global.lightPanel.isTabletConnected && !prevLightStatus)
                    {
                        foreach (GameObject gameobj in PlayerManager.players)
                        {
                            if (Vector3.Distance(Global.lightPanel.transform.position, gameobj.transform.position) < 3f)
                            {
                                foreach (Player p in Global.plugin.Server.GetPlayers())
                                {
                                    if (p.PlayerId == gameobj.GetComponent<QueryProcessor>().PlayerId)
                                    {
                                        p.PersonalBroadcast(10, "Вы включили свет в комплексе", true);
                                        Global.plugin.Server.Map.AnnounceCustomMessage("The lights have been turned on by system", true);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        Global.roomsOffLight = new List<Room>();
                        prevLightStatus = true;
                    }
                }
                catch (Exception ex)
                {
                    Global.plugin.Info("Error: " + ex.Message);
                }
            }
            
        }       

    }
}
