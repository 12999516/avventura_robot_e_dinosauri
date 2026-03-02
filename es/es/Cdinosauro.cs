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
        int altezza;
        CancellationToken cancellationToken;
        SemaphoreSlim mutex_dino;
        static int id = 0;
        int id_dino;

        public Cdinosauro(SemaphoreSlim sem_robot, SemaphoreSlim sem_dino, Cstack<string> cstack, Cqueue<string> cqueue, CancellationToken cancellationToken, SemaphoreSlim mutex_dino)
        {
            this.sem_robot = sem_robot;
            this.sem_dino = sem_dino;
            this.cstack = cstack;
            this.cqueue = cqueue;
            altezza = 0;
            this.cancellationToken = cancellationToken;
            this.mutex_dino = mutex_dino;
            id++;
            id_dino = id;
        }

        private async Task entra()
        {
            await sem_dino.WaitAsync(cancellationToken);
        }

        public async Task run()
        {
            while (true)
            {
                try
                {
                    await entra();
                    await mutex_dino.WaitAsync(cancellationToken);
                    altezza++;
                    string materiale = cqueue.dequeue();
                    cstack.push(materiale);
                    WriteLine($"il {materiale} viene spostato nello stack dal dinosauro {id_dino}");
                    if (altezza == 2)
                    {
                        WriteLine("viene costruito un pezzo del portale");
                        cstack.clear();
                        altezza = 0;
                    }
                    mutex_dino.Release();
                    sem_robot.Release();
                }catch(Exception ex)
                {

                }
            }
        }
    }
}
