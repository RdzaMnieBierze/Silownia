namespace Ciezarki.MVVM.Model
{
    internal class UserWorkout
    {

        private int _id;
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        private int _id_user;
        public int Id_user
        {
            set { _id_user = value; }
            get { return _id_user; }
        }
        private int _id_workout;
        public int Id_workout
        {
            set { _id_workout = value; }
            get { return _id_workout; }
        }
        private DateTime _create_date;
        public DateTime Create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }
        private DateTime _plan_date;
        public DateTime Plan_date
        {
            set { _plan_date = value; }
            get { return _plan_date; }
        }
        private string _name;
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        public UserWorkout() { }
        public UserWorkout(int id, int id_user, int id_workout, DateTime create_date, DateTime plan_date, string name)
        {
            Id = id;
            Id_user = id_user;
            Id_workout = id_workout;
            Name = name;
            Create_date = create_date;
            Plan_date = plan_date;

        }

        public override string ToString()
        {
            return "Data treningu: " + Plan_date.ToShortDateString() + "\tNazwa: " + Name + "\tID: " + Id + "\tID_workout: " + Id_workout;
        }

        public User User { get; set; }
        public Workout Workout { get; set; }

    }

}
