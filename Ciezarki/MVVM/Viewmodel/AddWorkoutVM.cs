using Ciezarki.MVVM.Model;

namespace Ciezarki.MVVM.Viewmodel
{
    internal class AddWorkoutVM : BaseVM
    {
        private AppDbContext _dbContext;

        public AddWorkoutVM()
        {
            _dbContext = new AppDbContext();
            _dbContext.Database.EnsureCreated();
            //Workout w = new Workout();
            //Exercise e = new Exercise();
            //AddWorkout(w);
            //AddExercise(e);
        }


        public void AddWorkout(Workout workout)
        {
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();
        }

        public void AddExercise(Exercise exercise)
        {
            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

        }
    }
}


