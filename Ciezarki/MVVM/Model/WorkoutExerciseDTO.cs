using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciezarki.MVVM.Model
{
    public class WorkoutExerciseDTO
    {
        public string Name { get; set; }
        public string Muscle { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double Load { get; set; }
        public int Resttime { get; set; }

        public override string ToString()
        {
            return $"{Name, -40}Powtórzenia: {Reps, -8}Serie: {Sets, -8}Obciążenie: {Load, 5} kg\tOdpoczynek: {Resttime, 4} s\tMięśnie: {Muscle, -20}";
        }
    }

}
