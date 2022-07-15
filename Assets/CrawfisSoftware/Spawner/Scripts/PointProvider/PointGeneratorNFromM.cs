using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public class PointGeneratorNFromM : PointGeneratorCompleteList
    {
        private int generationSize = 1;
        private readonly System.Random random;

        public int GenerationSize
        {
            get { return generationSize; }
            set
            {
                // Todo: Possibly remove the restriction and allow value > Count to create a wave.
                if ((value <= 0) || (value > positions.Count))
                    throw new ArgumentOutOfRangeException("Generation size must be greater than zero and <= input position size.");
                generationSize = value;

            }
        }

        public PointGeneratorNFromM(Vector3 fixedPosition, System.Random random = null) : base(fixedPosition)
        {
            this.random = random;
            if (this.random == null) this.random = new System.Random();
        }

        public PointGeneratorNFromM(IEnumerable<Vector3> listOfPositions, System.Random random = null) : base(listOfPositions)
        {
            this.random = random;
            if (this.random == null) this.random = new System.Random();
        }

        public override IList<Vector3> GetNextSet()
        {
            // Todo: Create a permutation and use that, either wrapping around or recomputing.
            var outputPositions = new List<Vector3>(generationSize);
            for(int i=0; i < generationSize; i ++)
            {
                outputPositions.Add(positions[random.Next(0, positions.Count)]);
            }
            return outputPositions;
        }
    }
}
