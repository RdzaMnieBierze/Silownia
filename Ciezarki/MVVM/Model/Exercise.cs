namespace Ciezarki.MVVM.Model
{
    internal class Exercise
    {
        public Exercise(int id, string name, string description, string muscle)
        {
            Id = id;
            Name = name;
            Description = description;
            Muscle = muscle;
        }
        private int _id;
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        private string _name;
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        private string _description;
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        private string _muscle;
        public string Muscle
        {
            set { _muscle = value; }
            get { return _muscle; }
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
