using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using es;
using static System.Console;

namespace es
{
    internal class Cdinosauro
    {
        SemaphoreSlim sem_robot;
        SemaphoreSlim sem_dino;
        Cstack<string> cstack;
        Cqueue<string> cqueue;

        public Cdinosauro(SemaphoreSlim sem_robot, SemaphoreSlim sem_dino, Cstack<string> cstack, Cqueue<string> cqueue)
        {
            this.sem_robot = sem_robot;
            this.sem_dino = sem_dino;
            this.cstack = cstack;
            this.cqueue = cqueue;
        }

        private async Task entra()
        {
            await sem_dino.WaitAsync();
        }

        public async Task run()
        {
            for (int i = 0; i < 5; i++)
            {
                await entra();
                string materiale = cqueue.dequeue();
                cstack.push(materiale);
                WriteLine($"il {materiale} viene spostato nello stack");
                sem_robot.Release();
            }
        }
    }
}
