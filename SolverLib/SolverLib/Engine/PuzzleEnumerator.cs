using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Job;

namespace SolverLib.Engine
{
    public class PuzzleEnumerator<TKey> : IEnumerator<IJob<TKey>>
    {
        private Queue<IJob<TKey>> queue1 = null;
        private Queue<IJob<TKey>> queue2 = null;

        private IEnumerator<IJob<TKey>> e1;
        private IEnumerator<IJob<TKey>> e2;

        private int currentEnumerator = 0;

        public PuzzleEnumerator(Queue<IJob<TKey>> queue1, Queue<IJob<TKey>> queue2)
        {
            this.queue1 = queue1;
            this.queue2 = queue2;
            this.e1 = queue1.GetEnumerator();
            this.e2 = queue2.GetEnumerator();
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public bool MoveNext()
        {
            currentEnumerator = 1;
            if (!this.e1.MoveNext())
            {
                currentEnumerator = 2;
                if (!this.e2.MoveNext())
                {
                    currentEnumerator = 0;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public void Reset()
        {
            currentEnumerator = 0;
            e1.Reset();
            e2.Reset();
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.-or- The collection was modified after the enumerator was created.</exception><filterpriority>2</filterpriority>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        /// The element in the collection at the current position of the enumerator.
        /// </returns>
        public IJob<TKey> Current
        {
            get
            {
                if (currentEnumerator == 1)
                {
                    return this.e1.Current;
                }
                else if (currentEnumerator == 2)
                {
                    return e2.Current;
                }
                return null;
            }
        }
    }
}
