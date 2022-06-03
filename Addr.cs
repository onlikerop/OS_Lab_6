namespace mainNamespace
{
    public class Addr
    {

        public OS_Task Task { get; set; }
        public int Address { get; set; }
        public Addr(OS_Task task, int address)
        {
            Task = task;
            Address = address;
        }
    }
}