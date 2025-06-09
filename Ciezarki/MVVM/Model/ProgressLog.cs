using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciezarki.MVVM.Model
{
    internal class ProgressLog
    {
        private int _id;
        private int _user_id;
        private DateTime _date;
        private double _weight;
        private double _chest;
        private double _biceps;
        private double _height;
        private string _notes;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int UserId {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public DateTime Date {
            get { return _date; }
            set { _date = value; }
        }
        public double Weight {
            get { return _weight; }
            set { _weight = value; }
        }
        public double Chest {
            get { return _chest; }
            set { _chest = value; }
        }
        public double Biceps {
            get { return _biceps; }
            set { _biceps = value; }
        }
        public double Height {
            get { return _height; }
            set { _height = value; }
        }
        public string Notes {
            get { return _notes; }
            set { _notes = value; }
        }


        public ProgressLog(int id, int userId, DateTime date, double weight, double chest, double biceps, double height, string notes)
        {
            Id = id;
            UserId = userId;
            Date = date;
            Weight = weight;
            Chest = chest;
            Biceps = biceps;
            Height = height;
            Notes = notes;
        }
        public override string ToString()
        {
            return $"{Id}: User Id: {UserId} on {Date.ToShortDateString()} - {Weight} kg, Chest: {Chest}, Biceps: {Biceps}, Height: {Height} cm, Notes: {Notes} ";
        }
    }
}
