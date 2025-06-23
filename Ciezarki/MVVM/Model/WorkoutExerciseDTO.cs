using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciezarki.MVVM.Model
{
    public class WorkoutExerciseDTO
    {
        public string? Name { get; set; }
        public string? Muscle { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double Load { get; set; }
        public int Resttime { get; set; }

        public override string ToString()
        {
            return string.Format("{0,-30} Powtórzenia: {1,-3} Serie: {2,-3} Obciążenie: {3,4} kg  Odpoczynek: {4,4} s  Mięśnie: {5,-20}",
                                 Name, Reps, Sets, Load, Resttime, Muscle);
        }

    }

}
