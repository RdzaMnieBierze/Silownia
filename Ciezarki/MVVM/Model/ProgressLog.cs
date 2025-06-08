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
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public double Chest { get; set; }
        public double Biceps { get; set; }
        public double Height { get; set; }
        public string Notes { get; set; }


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
