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
        CancellationToken cancellationToken;
        SemaphoreSlim mutex_robot;
        static int id = 0;
        int id_robot;

        public Crobot(SemaphoreSlim sem_robot, SemaphoreSlim sem_dino, Cqueue<string> cqueue, CancellationToken cancellationToken, SemaphoreSlim mutex_robot)
        {
            this.sem_robot = sem_robot;
            this.sem_dino = sem_dino;
            this.cqueue = cqueue;
            this.cancellationToken = cancellationToken;
            this.mutex_robot = mutex_robot;
            id++;
            id_robot = id;
        }

        private async Task entra()
        {
            await sem_robot.WaitAsync(cancellationToken);
        }

        public async Task fai()
        {
            int cont = 0;
            Random rdn = new Random();
            while (true)
            {
                try
                {
                    await entra();
                    await mutex_robot.WaitAsync(cancellationToken);
                    rdn.Next(1, 251);
                    cqueue.enqueue($"materiale {cont} viene messo nella coda dal robot {id_robot}");
                    cont++;
                    mutex_robot.Release();
                    sem_dino.Release();
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}
