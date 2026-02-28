using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using es;

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
            sem_dino.WaitAsync();
        }

        public async Task run()
        {
            entra();
            string materiale = cqueue.dequeue();
            cstack.push(materiale);
            sem_robot.Release();
        }
    }
}
