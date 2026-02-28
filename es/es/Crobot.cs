using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace es
{
    internal class Crobot
    {
        SemaphoreSlim sem_robot;
        SemaphoreSlim sem_dino;
        Cqueue<string> cqueue;
        int nmat;

        public Crobot(SemaphoreSlim sem_robot, SemaphoreSlim sem_dino, Cqueue<string> cqueue)
        {
            this.sem_robot = sem_robot;
            this.sem_dino = sem_dino;
            this.cqueue = cqueue;
            int nmat = 0;
        }

        private async Task entra()
        {
            sem_robot.WaitAsync();
        }

        public async Task fai()
        {
            entra();
            cqueue.enqueue($"materiale {nmat}");
            sem_dino.Release();
        }
    }
}
