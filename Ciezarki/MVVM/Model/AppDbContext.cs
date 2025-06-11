using Microsoft.EntityFrameworkCore;

namespace Ciezarki.MVVM.Model
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercises> WorkoutExercises { get; set; }
        public DbSet<ProgressLog> ProgressLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(1,"Antek","abc@interia.pl","haslo",DateTime.Now)
                );
            
            modelBuilder.Entity <Exercise>().HasData(
               new Exercise(1, "Pompki klasyczne", "Przyjmij pozycję podporu przodem z rękami na szerokość barków. Utrzymując ciało w jednej linii, zegnij łokcie i opuść klatkę piersiową w kierunku podłogi. Następnie wyprostuj ręce, wracając do pozycji wyjściowej.", "klatka piersiowa"),
               new Exercise(2, "Przysiady z ciężarem ciała", "Stań prosto ze stopami na szerokość bioder. Zegnij kolana i wypchnij biodra do tyłu, opuszczając ciało jakbyś siadał na krześle. Utrzymuj plecy prosto. Wróć do pozycji stojącej, prostując nogi.", "uda"),
               new Exercise(3, "Deska", "Połóż się na brzuchu, podeprzyj się na przedramionach i palcach stóp. Unieś ciało, tworząc prostą linię od głowy do pięt. Napnij mięśnie brzucha i utrzymaj pozycję przez określony czas.", "brzuch"),
               new Exercise(4, "Mountain climbers", "Przyjmij pozycję pompki. Naprzemiennie przyciągaj kolana do klatki piersiowej w szybkim tempie, utrzymując napięcie mięśni brzucha i stabilną sylwetkę.", "brzuch"),
               new Exercise(5, "Wykroki", "Stań prosto, wykonaj duży krok do przodu jedną nogą. Zegnij oba kolana, aż tylne kolano prawie dotknie podłoża. Wróć do pozycji wyjściowej i powtórz na drugą nogę.", "pośladki"),
               new Exercise(6, "Most biodrowy", "Połóż się na plecach, ugnij kolana, stopy oprzyj o podłoże. Unieś biodra w górę, napinając pośladki, aż ciało utworzy linię prostą od barków do kolan. Powoli opuść biodra.", "pośladki"),
               new Exercise(7, "Burpees", "Stań prosto, wykonaj przysiad i oprzyj ręce na podłodze. Wyskocz nogami do tyłu do pozycji pompki. Zrób pompkę, wróć nogami do przysiadu i wyskocz w górę z uniesionymi rękami.", "całe ciało"),
               new Exercise(8, "Rozpiętki z hantlami", "Połóż się na plecach na ławce, trzymaj hantle nad klatką piersiową z lekko ugiętymi łokciami. Powoli rozkładaj ręce na boki, aż poczujesz rozciągnięcie w klatce, następnie wróć do pozycji wyjściowej.", "klatka piersiowa"),
               new Exercise(9, "Unoszenie nóg leżąc", "Połóż się na plecach z rękami wzdłuż ciała. Unieś proste nogi ku górze do kąta prostego, następnie opuszczaj je powoli, nie dotykając podłoża. Napnij brzuch przez cały ruch.", "brzuch"),
               new Exercise(10, "Pajacyki", "Stań prosto z rękami wzdłuż ciała. Wyskocz, jednocześnie rozkładając nogi na boki i unosząc ręce nad głowę. Wróć do pozycji wyjściowej i powtarzaj ruch w szybkim tempie.", "nogi"));

            modelBuilder.Entity<ProgressLog>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.ProgressLog)
                .HasForeignKey(pl => pl.UserId);

            modelBuilder.Entity<UserWorkout>()
                .HasOne(uw => uw.User)
                .WithMany(u => u.UserWorkout)
                .HasForeignKey(uw => uw.Id_user);
            modelBuilder.Entity<UserWorkout>()
                .HasOne(uw => uw.Workout)
                .WithMany(w => w.UserWorkout)
                .HasForeignKey(uw => uw.Id_workout);

            modelBuilder.Entity<WorkoutExercises>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.Id_workout);

            modelBuilder.Entity<WorkoutExercises>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.Id_exercise);
        }
    }
}
