using GymProgresser.Domain.Entities;
using GymProgresser.Domain.Enums;
using GymProgresser.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure
{
    public class ExerciseSeeder
    {
        public static void SeedExercises(GymProgressDbContext context)
        {
            if (context.Exercises.Any()) return;

            var exercises = new List<Exercise>
        {
            // Klatka piersiowa
            new() { Name = "Wyciskanie sztangi leżąc", Category = ExerciseCategory.Chest, IsVerified = true, CreatedById = 1 },
            new() { Name = "Wyciskanie hantli na ławce skośnej", Category = ExerciseCategory.Chest, IsVerified = true, CreatedById = 1 },
            new() { Name = "Rozpiętki na ławce płaskiej", Category = ExerciseCategory.Chest, IsVerified = true, CreatedById = 1 },
            new() { Name = "Pompki klasyczne", Category = ExerciseCategory.Chest, IsVerified = true, CreatedById = 1 },
            new() { Name = "Wyciskanie na maszynie", Category = ExerciseCategory.Chest, IsVerified = true, CreatedById = 1 },

            // Plecy
            new() { Name = "Martwy ciąg", Category = ExerciseCategory.Back, IsVerified = true, CreatedById = 1 },
            new() { Name = "Podciąganie nachwytem", Category = ExerciseCategory.Back, IsVerified = true, CreatedById = 1 },
            new() { Name = "Wiosłowanie hantlą", Category = ExerciseCategory.Back, IsVerified = true, CreatedById = 1 },
            new() { Name = "Ściąganie drążka do klatki", Category = ExerciseCategory.Back, IsVerified = true, CreatedById = 1 },
            new() { Name = "Face pull", Category = ExerciseCategory.Back, IsVerified = true, CreatedById = 1 },

            // Biceps
            new() { Name = "Uginanie ramion ze sztangą", Category = ExerciseCategory.Biceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Uginanie ramion z hantlami", Category = ExerciseCategory.Biceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Modlitewnik", Category = ExerciseCategory.Biceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Uginanie ramion z linką dolnego wyciągu", Category = ExerciseCategory.Biceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Uginanie młotkowe", Category = ExerciseCategory.Biceps, IsVerified = true, CreatedById = 1 },

            // Triceps
            new() { Name = "Prostowanie ramion na wyciągu", Category = ExerciseCategory.Triceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Wyciskanie francuskie", Category = ExerciseCategory.Triceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Pompki na poręczach", Category = ExerciseCategory.Triceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Kickback tricepsa", Category = ExerciseCategory.Triceps, IsVerified = true, CreatedById = 1 },
            new() { Name = "Prostowanie ramienia zza głowy", Category = ExerciseCategory.Triceps, IsVerified = true, CreatedById = 1 },

            // Nogi
            new() { Name = "Przysiady ze sztangą", Category = ExerciseCategory.Legs, IsVerified = true, CreatedById = 1 },
            new() { Name = "Wykroki z hantlami", Category = ExerciseCategory.Legs, IsVerified = true, CreatedById = 1 },
            new() { Name = "Martwy ciąg na prostych nogach", Category = ExerciseCategory.Legs, IsVerified = true, CreatedById = 1 },
            new() { Name = "Wspięcia na palce", Category = ExerciseCategory.Legs, IsVerified = true, CreatedById = 1 },
            new() { Name = "Przysiady bułgarskie", Category = ExerciseCategory.Legs, IsVerified = true, CreatedById = 1 },

            // Barki
            new() { Name = "Wyciskanie żołnierskie", Category = ExerciseCategory.Shoulders, IsVerified = true, CreatedById = 1 },
            new() { Name = "Unoszenie ramion bokiem", Category = ExerciseCategory.Shoulders, IsVerified = true, CreatedById = 1 },
            new() { Name = "Arnold press", Category = ExerciseCategory.Shoulders, IsVerified = true, CreatedById = 1 },
            new() { Name = "Unoszenie ramion w opadzie", Category = ExerciseCategory.Shoulders, IsVerified = true, CreatedById = 1 },
            new() { Name = "Face pulls", Category = ExerciseCategory.Shoulders, IsVerified = true, CreatedById = 1 },

            // Inne
            new() { Name = "Plank", Category = ExerciseCategory.Other, IsVerified = true, CreatedById = 1 },
        };

            context.Exercises.AddRange(exercises);
            context.SaveChanges();
        }
    }
}
