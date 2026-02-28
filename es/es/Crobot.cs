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

        public Crobot(SemaphoreSlim sem_robot, SemaphoreSlim sem_dino, Cqueue<string> cqueue)
        {
            this.sem_robot = sem_robot;
            this.sem_dino = sem_dino;
            this.cqueue = cqueue;
        }

        private async Task entra()
        {
            await sem_robot.WaitAsync();
        }

        public async Task fai()
        {
            for (int i = 0; i < 5; i++)
            {
                await entra();
                cqueue.enqueue($"materiale {i}");
                sem_dino.Release();
            }
        }
    }
}
