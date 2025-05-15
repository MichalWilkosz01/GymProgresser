using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Domain.Enums
{
    public enum ExerciseCategory
    {
        [Description("Inne")]
        Other,
        [Description("Klatka piersiowa")]
        Chest,
        [Description("Plecy")]
        Back,
        [Description("Biceps")]
        Biceps,
        [Description("Triceps")]
        Triceps,
        [Description("Nogi")]
        Legs,
        [Description("Barki")]
        Shoulders
    }
}
