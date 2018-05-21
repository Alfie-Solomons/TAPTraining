using System;
using System.Threading;
using System.Threading.Tasks;

namespace TAP_Example___Basic
{
	/// <summary>
	/// The Master version
	/// </summary>
	public class Program
	{
		static void Main()
		{
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} Main(): Calling TestAsync() asynchronously...");
			var task = new Program().TestAsync();
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} Main(): back from TestAsync(), calling task.Wait()...");
			task.Wait();
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} Main(): task finished, press Enter to exit.");
			Console.ReadLine();
		}

		private async Task TestAsync()
		{
			// Running on the calling thread...
			var delayTime = 4000;

			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} Calling WaitABitAsync synchronously");
			var result = WaitABitAsync(delayTime).Result;
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} back from WaitABitAsync, result = {result}");
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} press Enter to continue...");
			Console.ReadLine();

			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} Calling WaitABitAsync asynchronously...");
			var task = WaitABitAsync(delayTime);
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} back from WaitABitAsync, awaiting result...");
			result = await task;
			// At this spot, the task will be returned to the caller immediately

			// Following continuation code #1 will be executed after the task completes
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} got result: {result}");

			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} Calling WaitABitAsync asynchronously...");
			result = await WaitABitAsync(delayTime);
			// Following continuation code #2 will be executed after the task 
			// returned from the second call to WaitABitAsync completes
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} got result: {result}");

			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} Calling WaitABitAsync asynchronously 2...");
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} got result 2: {await WaitABitAsync(delayTime)}");
		}

		private async Task<int> WaitABitAsync(int millisec)
		{
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} WaitABitAsync: Calling Delay()");
			await Task.Delay(millisec);
			Console.WriteLine($"{DateTime.Now} ID: {Thread.CurrentThread.ManagedThreadId} WaitABitAsync: returning result");
			return millisec * 2;
		}
	}
}
