using EXILED;

namespace LightRoomControl
{
    public class MainSettings : Plugin
    {
        public override string getName => "LightRoomControl";
        private SetEvents SetEvents;

        public override void OnEnable()
        {
            SetEvents = new SetEvents();

            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}