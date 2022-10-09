# Thread Pool in C# with Examples

## Thread Pool in C#:
The Thread pool in C# is nothing but a collection of threads that can be reused to perform no of tasks in the background. Now when a request comes, then it directly goes to the thread pool and checks whether there are any free threads available or not. If available, then it takes the thread object from the thread pool and executes the task as shown in the below image.

![image](https://user-images.githubusercontent.com/11813841/194757098-e55c04bf-5611-4c3c-a12b-b8a2abd765b1.png)

Once the thread completes its task then it is again sent back to the thread pool so that it can reuse. This reusability avoids an application to create a number of threads and this enables less memory consumption.


# How to use C# Thread Pool?
Let us see a simple example to understand how to use Thread Pooling. Once you understand how to use thread pooling then we will see the performance benchmark between the normal thread object and the thread pool.

## Step1:
In order to implement thread pooling in C#, first, we need to import the Threading namespace as ThreadPool class belongs to this namespace as shown below.

using System.Threading;

## Step2:
Once you import the Threading namespace, then you need to use the ThreadPool class, and using this class you need to call the QueueUserWorkItem static method. If you go to the definition of the QueueUserWorkItem method, then you will see that this method takes one parameter of the type WaitCallback object. While creating the object of the WaitCallback class, you need to pass the method name that you want to execute.

ThreadPool.QueueUserWorkItem(new WaitCallback(MyMethod));

Here, the QueueUserWorkItem method Queues the function for execution and that function executes when a thread becomes available from the thread pool. If no thread is available then it will wait until one thread gets freed. Here MyMethod is the method that we want to execute by a thread pool thread.

# The complete code is given below.
As you can see in the below code, here, we create one method that is MyMethod and as part of that method, we simply print the thread id, whether the thread is a background thread or not, and whether it is from a thread pool or not. And we want to execute this method 10 times using the thread pool threads. So, here we use a simple for each loop and use the ThreadPool class and call that method.

```
using System;
using System.Threading;
namespace ThreadPoolApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(MyMethod));
            }
            Console.Read();
        }
        public static void MyMethod(object obj)
        {
            Thread thread = Thread.CurrentThread;
            string message = $"Background: {thread.IsBackground}, Thread Pool: {thread.IsThreadPoolThread}, Thread ID: {thread.ManagedThreadId}";
            Console.WriteLine(message);
        }
    }
}
```
Once you execute the above code, it will give you the following output. As you can see, it shows that it is a background thread and this thread is from the thread pool and the thread Ids may vary in your output. Here, you can see three threads handle all the 10 method calls.

![image](https://user-images.githubusercontent.com/11813841/194757344-605a1d23-90e6-49b2-8056-7b54bb01decc.png)

# Performance testing using and without using Thread Pool in C# with Example:
Let us see an example to understand the performance benchmark. Here, we will compare how much time the thread object takes and how much time the thread pool thread takes to do the same task i.e. to execute the same methods.

```
static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                MethodWithThread();
                MethodWithThreadPool();
            }
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 10; i++)
            {
                stopwatch.Reset();
                Console.WriteLine("********Case " + (i + 1) + " ********");
                stopwatch.Start();
                MethodWithThread();
                stopwatch.Stop();
                Console.WriteLine("Time consumed by MethodWithThread is : " +
                                     stopwatch.ElapsedTicks.ToString());

                stopwatch.Reset();
                stopwatch.Start();
                MethodWithThreadPool();
                stopwatch.Stop();
                Console.WriteLine("Time consumed by MethodWithThreadPool is : " +
                                     stopwatch.ElapsedTicks.ToString());
                Console.WriteLine("________________________________");
            }


            Console.Read();
        }

        public static void MethodWithThread()
        {
            for (int i = 0; i < 10000; i++)
            {
                Thread thread = new Thread(Test);
            }
        }
        public static void MethodWithThreadPool()
        {
            for (int i = 0; i < 10000; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Test));
            }

        }
        public static void Test(object obj)
        {
        }
        
```
# Output:
![image](https://user-images.githubusercontent.com/11813841/194757504-0215204c-9a67-4dfd-a30a-d51fb30b5243.png)

 If you observe there is a vast time difference between these two.
 
So it proves that the thread pool gives better performance as compared to the thread class object. If there are need to create one or two threads then you need to use the Thread class object while if there is a need to create more than 5 threads then you need to go for the thread pool class in a multithreaded environment.
