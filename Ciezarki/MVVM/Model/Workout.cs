using System.Text.Json.Serialization;

namespace Ciezarki.MVVM.Model
{
    internal class Workout
    {

        private string _name;
        public string Name { 
            set { _name = value; }
            get { return _name; } }
        private int _id;
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        private DateTime _create_date;
        public DateTime Create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }
        private string _notes;
        public string Notes
        {
            set { _notes = value; }
            get { return _notes; }
        }

        public Workout(int id, DateTime create_date, string notes)
        {
            Id = id;
            Create_date = create_date;
            Notes = notes;
        }

        public Workout()
        {
            Create_date = DateTime.Now;
            Notes = string.Empty;

        }


        public override string ToString()
        {
            return Notes;
        }

        public ICollection<WorkoutExercises> WorkoutExercises { get; set; }
        public ICollection<UserWorkout> UserWorkout { get; set; }
    }
}
