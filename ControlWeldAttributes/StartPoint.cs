using Aveva.ApplicationFramework;


namespace ControlWeldAttributes
{
    public class StartPoint : IAddin
    {
        public string Name => "ControlWeldAttributes";

        public string Description => "Control attributes for Weld when it creating and modificating.";

        
        public void Start(ServiceManager serviceManager)
        {
            Controller.Run();
           
        }

      

        public void Stop() { }


    }
}
