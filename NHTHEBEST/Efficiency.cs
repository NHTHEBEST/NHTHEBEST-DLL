using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NHTHEBEST
{
    namespace Efficiency
    {
        public sealed class CPU
        {
            public static int LogicalProcessors { get;} = Environment.ProcessorCount;
            public static int PhysicalCores { get; } = GetCores();
            public static int PhysicalProcessors { get; } = GetPhysicalProcessors();
            private static int GetCores()
            {
                int coreCount = 0;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
                {
                    coreCount += int.Parse(item["NumberOfCores"].ToString());
                }
                return coreCount;
            }
            private static int GetPhysicalProcessors()
            {
                int pp = 1;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                {
                    pp = int.Parse(item["NumberOfProcessors"].ToString());
                }
                return pp;
            }
        }
        public class SuperThread
        {
            public List<Action> Code { get; set; }
            public int MaxCoresToUse { get; set; } = CPU.LogicalProcessors;
            private Thread master;
            public void Join()
            {
                master.Join();
            }
            public void Start()
            {
                master = new Thread(Run);
                master.Start();
            }
            private void Run()
            {
                Thread[] Athreads;
                int size = Code.Count();
                int cpu = CPU.LogicalProcessors;
                if (MaxCoresToUse <= cpu){
                    cpu = MaxCoresToUse;
                }
                int TaskPerThread = (int)Math.Round((double)size / (double)cpu);
                List<Thread> threads = new List<Thread>();
                List<ThreadStart> tasks = new List<ThreadStart>();
                foreach (Action Task in Code){
                    tasks.Add(new ThreadStart(Task));
                }
                ThreadStart[] starts = tasks.ToArray();
                for (int ii = 1; ii <= TaskPerThread + 1; ii++)
                {
                    for (int i = 1; i <= cpu; i++) { 
                    try { threads.Add(new Thread(starts[i - 1])); }
                    catch { }
                    }
                }
                Athreads = threads.ToArray();
                for (int i = 0; i <= TaskPerThread; i += cpu)
                {
                    for (int ii = 0; ii <= cpu; ii++)
                    {
                        Athreads[i + ii].Start();
                    }
                    for (int ii = 0; ii <= cpu; ii++)
                    {
                        Athreads[i + ii].Join();
                    }
                }
            }
        }
    }
}
