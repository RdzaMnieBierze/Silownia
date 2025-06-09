using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciezarki.MVVM.Model
{
    internal class Workout
    {
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
    }
}
