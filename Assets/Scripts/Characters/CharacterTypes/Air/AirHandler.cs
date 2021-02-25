using Characters.Movement;
using Characters.General;

namespace Characters.CharacterTypes.Air
{
    public class AirHandler
    {
        public FlyManager FlyManager { get; private set; }

        public AirHandler(Character character) => FlyManager = character.gameObject.GetComponent<FlyManager>();

        public void Enable() => FlyManager.enabled = true;

        public void Disable() => FlyManager.enabled = false;

        public void SetCommon(Commons commons) => FlyManager.SetSpeed(commons.flySpeed);

        public void SetNormal() => FlyManager.SetDefaultSpeed();
    }
}