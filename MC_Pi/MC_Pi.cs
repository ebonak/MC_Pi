using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* 
 * compute the value of Pi using a Monte Carlo Simulation
 * based on https://www.youtube.com/watch?v=VJTFfIqO4TU
 * 
 * e.bonakdarian Dec 2016
 */

namespace MC_Pi
{
    class MC_Pi
    {
        // num_trials was a constant initially .. now I will prompt for values.
        private const int K = 1000;
        private static long num_trials = 5000 * K; // number of trials

        static void Main(string[] args)
        {
            double radius = 1.0;

            Console.Write("Enter number of trials: ");
            if (!long.TryParse(Console.ReadLine(), out num_trials))
            {
                Console.Error.WriteLine("*** ERROR: invalid entry - exiting program.");
                Environment.Exit(-1);
            }

            if (num_trials < 1)
            {
                Console.Error.WriteLine("*** ERROR: negative value entered - exiting program.");
                Environment.Exit(-2);
            }

            Console.WriteLine("\nEstimating Pi ({0:f5}) using Monte Carlo Simulation", Math.PI);
            Console.WriteLine("Number of trials is: {0:0,0}\n", num_trials);

            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            double pi_est = computePi(num_trials, radius);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            writeResults(elapsedTime, pi_est);
            banner();
            // Console.WriteLine("\nGood-bye.");
            // Console.ReadLine();
        }


        static void banner()
        {
            Console.WriteLine("my banner...");
        }


        static void writeResults(string elapsedTime, double pi_est)
        {
            Console.WriteLine("\n\nElapsed Time " + elapsedTime);
            Console.WriteLine("\npi estimate: {0,9:N6}", pi_est);
            Console.WriteLine("pi         : {0,9:F6}", Math.PI);
            Console.WriteLine("Difference : {0,9:F6}", (pi_est - Math.PI));
        }



        static double computePi(long num_trial, double radius)
        {
            int in_count = 0;  // in circle
            Random rand = new Random();
            double x = 0.0;
            double y = 0.0;
            int update_interval = (int)(num_trial / 10);
            double pi_est = 0.0;

            Console.WriteLine("Updated inteval at {0:n0} iterations", update_interval);
            for (long i = 0; i < num_trial; i++)
            {
                x = rand.NextDouble();
                y = rand.NextDouble();

                if (inCircle(x, y, radius))
                    in_count++;

                pi_est = (4.0 * in_count) / num_trial;

                if ((i + 1) % update_interval == 0)
                    Console.WriteLine("pi est: {0:f6}  diff: {1,9:f6}  [{2,9:N0}]",
                                       pi_est, Math.PI - pi_est, i + 1);
            }

            return pi_est;
        }


        static bool inCircle(double x, double y, double r)
        // is the point x, y inside the circle with radius r
        {
            return (x * x) + (y * y) <= (r * r);
        }
    }
}
