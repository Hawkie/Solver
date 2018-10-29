using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using SolverLib.Core;
using SolverLib.Job;
using SolverLib.Puzzle;
using SolverLib.Space;
using SolverLib.Constraints;

namespace SolverLib.Engine
{
    public delegate void SolverFinishedHandler();

    public class PuzzleEngine<TKey> : IPuzzleEngine<TKey>
    {
        
        public event SolverFinishedHandler SolverFinishedEvent;

        public PuzzleEngine(IPuzzle<TKey> puzzle)
        {
            WorkQueue1 = new Queue<IJob<TKey>>();
            WorkQueue2 = new Queue<IJob<TKey>>();
            Puzzle = puzzle;
        }

        public IPuzzle<TKey> Puzzle { get; set; }

        private Queue<IJob<TKey>> WorkQueue1 { get; set;}
        private Queue<IJob<TKey>> WorkQueue2 { get; set; }

       

        /// <summary>
        /// Takes an action from the queue and processes it
        /// </summary>
        /// <param name="space">The solution space to change</param>
        protected void ProcessAction(IJob<TKey> job)
        {
            // Start with the values that have changed
            // After each one it set the keys it has affected in other constraints 
            // groups that are associated with these keys and apply constraints again
            job.Process(this);
        }

        public void SetInitialValues(ISpace<TKey> initialValuesCollection)
        {
            foreach (KeyValuePair<TKey, IPossible> valuePair in initialValuesCollection)
            {
                TKey k = valuePair.Key;
                if (Puzzle.Space[k].Values != valuePair.Value)
                {
                    if (valuePair.Value.SetEquals(Puzzle.Space.DefaultValue))
                    {
                        Puzzle.Space[k].Locked = false;
                    }
                    else
                    {
                        Puzzle.Space[k].Locked = true;
                    }

                    if (Puzzle.Space[k].SetValues(valuePair.Value))
                    {
                        Puzzle.Space[k].AcceptChanges();
                        Keys<TKey> keysChanged = new Keys<TKey>() { k };
                        IJob<TKey> job = new JobSearch<TKey>("Initial", keysChanged);
                        this.Add(job);
                    }
                }
            }
        }

        public void RunActionCount(int actionCount)
        {
            int actionsProcessed = 0;
            bool continueProcessing = WorkQueue1.Count > 0;
            while (continueProcessing)
            {
                IJob<TKey> action = WorkQueue1.Dequeue();
                ProcessAction(action);
                continueProcessing = WorkQueue1.Count > 0
                    || actionsProcessed == actionCount;
            }
        }

        public void Solve(bool async)
        {
            SolveBackground(async);
        }

        private void SolveUI()
        {
            while(Count > 0)
            {
                while (WorkQueue1.Count > 0)
                {
                    IJob<TKey> jobCurrent = WorkQueue1.Dequeue();
                    ProcessAction(jobCurrent);
                }
                while (WorkQueue2.Count > 0)
                {
                    IJob<TKey> jobCurrent = WorkQueue2.Dequeue();
                    ProcessAction(jobCurrent);
                }
            }
        }

        private void SolveBackground(bool async)
        {
            if (async)
            {
                //ThreadPool.QueueUserWorkItem(thread_DoWork);
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
            else
            {
                SolveUI();
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Keys<TKey> keysChanged = e.Result as Keys<TKey>;
            foreach (TKey key in keysChanged)
            {
                Puzzle.Space[key].AcceptChanges();
            }
            if (this.Count > 0)
            {
                SolveBackground(true);
            }
            else
            {
                SolverFinishedHandler handler = SolverFinishedEvent;
                if (handler != null)
                {
                    handler();
                }
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = DoNextJob();
        }

        public Keys<TKey> DoNextJob()
        {
            IJob<TKey> nextJob = null;
            Keys<TKey> keysChanged = new Keys<TKey>();
            if (WorkQueue1.Count > 0)
            {
                nextJob = WorkQueue1.Dequeue();
            }
            else if (WorkQueue2.Count > 0)
            {
                nextJob = WorkQueue2.Dequeue();
            }
            if (nextJob != null)
            {
                keysChanged.UnionWith(nextJob.Process(this));
            }
            return keysChanged;
        }

        

        public void Add(IJob<TKey> job)
        {
            if (job.Priority == 1)
            {
                WorkQueue1.Enqueue(job);
            }
            else
            {
                WorkQueue2.Enqueue(job);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
        public void Clear()
        {
            WorkQueue1.Clear();
            WorkQueue2.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        public bool Contains(IJob<TKey> job)
        {
            if (!WorkQueue1.Contains(job))
            {
                if (!WorkQueue2.Contains(job))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.-or-Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
        public void CopyTo(IJob<TKey>[] array, int arrayIndex)
        {
            WorkQueue1.CopyTo(array,arrayIndex);
            WorkQueue2.CopyTo(array, arrayIndex+array.GetLength(0));
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public bool Remove(IJob<TKey> item)
        {
            return false;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get
            {
                return WorkQueue1.Count + WorkQueue2.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<IJob<TKey>> GetEnumerator()
        {
            IEnumerator<IJob<TKey>> e1 = WorkQueue1.GetEnumerator();
            IEnumerator<IJob<TKey>> e2 = WorkQueue1.GetEnumerator();

            return new PuzzleEnumerator<TKey>(WorkQueue1, WorkQueue2);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IJob<TKey> Peek()
        {
            if (WorkQueue1.Count > 0)
            {
                return WorkQueue1.Peek();
            }
            else if (WorkQueue2.Count > 0)
            {
                return WorkQueue2.Peek();
            }
            return null;
        }
    }
}
