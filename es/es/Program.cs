using static System.Console;
using es;

namespace es
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Cqueue<string> queue = new Cqueue<string>();
            Cstack<string> stack = new Cstack<string>();
            SemaphoreSlim sem_robot = new SemaphoreSlim(1, 1);
            SemaphoreSlim sem_dino = new SemaphoreSlim(0, 1);

            Crobot robot = new Crobot(sem_robot, sem_dino, queue);
            Cdinosauro dino = new Cdinosauro(sem_robot, sem_dino, stack, queue);

            List<Task> tasks = new List<Task>();
            tasks.Add(robot.fai());
            tasks.Add(dino.run());

            await Task.WhenAll(tasks);
        }
    }
}
