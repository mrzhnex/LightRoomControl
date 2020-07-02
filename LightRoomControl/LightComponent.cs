using EXILED.ApiObjects;
using System.Collections.Generic;
using UnityEngine;

namespace LightRoomControl
{
    public class LightComponent : MonoBehaviour
    {
        private float Timer = 0.0f;
        private float TimeIsUp = 7.0f;

        public void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > TimeIsUp)
            {
                Timer = 0.0f;

                IReadOnlyList<Room> rooms = Global.roomsOffLight;
                foreach (Room room in rooms)
                {
                    room.FlickerLights();
                }
            }
        }
    }
}