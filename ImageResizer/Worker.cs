using System;

namespace ImageResizer
{
    /// <summary>
    /// Инкапсулирует поток для выполнения любых задач
    /// </summary>
    class Worker
    {
        private readonly object locker = new object();
        private bool sleeping, running;
        private System.Threading.Thread thread;
        private Action task;

        public Worker()
        {
            sleeping = true;
            running = false;
            thread = new System.Threading.Thread(Run);
            thread.Start();
        }

        public Action Task { set { lock (locker){ this.task = value; } } }
        public bool IsWorking { get { return !sleeping; } }
        public void StartTask() { sleeping = false; }

        protected void Run() {
            running = true;

            while(running)
            {
                sleeping = true;

                while (sleeping) System.Threading.Thread.Sleep(100);

                if (!running) return;

                if (task != null) task();
            }
        }
        
        public void Stop() {
            running = sleeping = false;
            thread.Join();
            thread = null;
        }
    }
}