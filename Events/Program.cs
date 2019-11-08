
namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var er = new EventRaiser();

            // er.DelegateTypeSelected += new System.EventHandler<DelegateTypeEventArgs>(DelegateTypes);
            // er.ExecutionCompleted += new System.EventHandler(Completed);

            // Delegate Inference
            er.DelegateTypeSelected += DelegateTypes;
            er.ExecutionCompleted += Completed;

            er.Start();
        }

        public static void DelegateTypes(object sender, DelegateTypeEventArgs args)
        {
            System.Console.WriteLine(args.Type);
        }

        public static void Completed(object sender, System.EventArgs args)
        {
            System.Console.WriteLine("That's all");
        }
    }
}
