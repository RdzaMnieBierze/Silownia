using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciezarki.MVVM.Model
{

    internal class ExerciseWorkout
    {

        private int _id;
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        private int _id_workout;
        public int Id_workout
        {
            set { _id_workout = value; }
            get { return _id_workout; }
        }
        private int _id_exercise;
        public int Id_exercise
        {
            set { _id_exercise = value; }
            get { return _id_exercise; }
        }
        private int _sets_exercise;
        public int Sets_exercise
        {
            set { _sets_exercise = value; }
            get { return _sets_exercise; }
        }
        private int _reps_exercise;
        public int Reps_exercise
        {
            set { _reps_exercise = value; }
            get { return _reps_exercise; }
        }
        private int _resttime_exercise;
        public int Resttime_exercise
        {
            set { _resttime_exercise = value; }
            get { return _resttime_exercise; }
        }

        public ExerciseWorkout(int id, int id_workout, int id_exercise, int sets_exercise, int reps_exercise, int resttime_exercise)
        {
            Id = id;
            Id_workout = id_workout;
            Id_exercise = id_exercise;
            Sets_exercise = sets_exercise;
            Reps_exercise = reps_exercise;
            Resttime_exercise = resttime_exercise;
        }
    }

}
