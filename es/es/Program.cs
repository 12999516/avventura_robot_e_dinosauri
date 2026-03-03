using es;
using System.Collections;
using System.Diagnostics;
using static System.Console;

namespace es
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Cqueue<string> queue = new Cqueue<string>();
            Cstack<string> stack = new Cstack<string>();
            SemaphoreSlim sem_robot = new SemaphoreSlim(5, 5);
            SemaphoreSlim sem_dino = new SemaphoreSlim(0);
            SemaphoreSlim mutex_robo = new SemaphoreSlim(1, 1);
            SemaphoreSlim mutex_dino = new SemaphoreSlim(1, 1);
            CancellationTokenSource cancellationToken_robot = new CancellationTokenSource();
            CancellationTokenSource cancellationtoken_dino = new CancellationTokenSource();



            List<Task> tasks = new List<Task>();
            List<Cdinosauro> dinos = new List<Cdinosauro>();
            List<Crobot> robots = new List<Crobot>();

            for (int i = 0; i < 20; i++)
            {
                if (i % 4 == 0)
                {
                    dinos.Add(new Cdinosauro(sem_robot, sem_dino, stack, queue, cancellationtoken_dino.Token, mutex_dino));
                    robots.Add(new Crobot(sem_robot, sem_dino, queue, cancellationToken_robot.Token, mutex_robo));
                }
                else
                {
                    robots.Add(new Crobot(sem_robot, sem_dino, queue, cancellationToken_robot.Token, mutex_robo));
                }

            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < robots.Count; i++)
            {
                tasks.Add(robots[i].fai());
            }

            for (int i = 0; i < dinos.Count; i++)
            {
                tasks.Add(dinos[i].run());
            }

            while (true)
            {
                if(stopwatch.ElapsedMilliseconds == 1000)
                {
                    cancellationToken_robot.Cancel();
                    cancellationtoken_dino.Cancel();
                    await Task.WhenAll(tasks);
                    WriteLine("Fine programma");
                    return;
                }
            }
        }
    }
}
