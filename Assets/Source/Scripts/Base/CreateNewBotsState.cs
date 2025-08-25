using Source.Scripts.Bots;

namespace Source.Scripts.Base
{
    public class CreateNewBotsState : BaseState
    {
        private BaseBotsHandler _botsHandler;
        private ResourcesCounter _counter;
        
        public void Initialize(BaseBotsHandler botsHandler, ResourcesCounter counter)
        {
            _botsHandler = botsHandler;
            _counter = counter;
        }

        public override void Enter()
        {
            base.Enter();
            _botsHandler.SetTask(Tasks.CollectResources);
        }

        public override void ProcessResource()
        {
            if (_counter.ResourcesCount >= _botsHandler.BotCreationCost)
            {
                _botsHandler.SpawnNewBots();
            }
        }
    }
}