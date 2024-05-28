using AlphaWorkout.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AlphaWorkout.Data
{
    public static class ExerciseSeed
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any exercises already in the database.
                if (context.Exercises.Any())
                {
                    return;   // Database has been seeded
                }

                context.Exercises.AddRange(
                    // Abdominals
                    new Exercise
                    {
                        Name = "Crunches",
                        Type = "strength",
                        Muscle = "abdominals",
                        Difficulty = "beginner",
                        Instructions = "Lie on your back with your knees bent and feet flat on the floor. Place your hands behind your head without interlocking your fingers. Engage your core and lift your shoulder blades off the ground, then lower back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Leg Raises",
                        Type = "strength",
                        Muscle = "abdominals",
                        Difficulty = "intermediate",
                        Instructions = "Lie on your back with your legs straight. Place your hands under your lower back for support. Lift your legs until they are perpendicular to the floor, then lower them back down without touching the floor. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Plank",
                        Type = "strength",
                        Muscle = "abdominals",
                        Difficulty = "beginner",
                        Instructions = "Start in a push-up position but with your weight on your forearms instead of your hands. Keep your body in a straight line from head to heels. Hold this position for as long as possible."
                    },
                    new Exercise
                    {
                        Name = "Bicycle Crunches",
                        Type = "strength",
                        Muscle = "abdominals",
                        Difficulty = "intermediate",
                        Instructions = "Lie on your back with your hands behind your head and legs raised. Bring your right elbow towards your left knee while straightening your right leg. Switch sides in a pedaling motion. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Hanging Leg Raises",
                        Type = "strength",
                        Muscle = "abdominals",
                        Difficulty = "expert",
                        Instructions = "Hang from a pull-up bar with your legs straight. Engage your core and lift your legs until they are parallel with the floor. Slowly lower them back down. Repeat."
                    },
                    // Abductors
                    new Exercise
                    {
                        Name = "Side-Lying Leg Lifts",
                        Type = "strength",
                        Muscle = "abductors",
                        Difficulty = "beginner",
                        Instructions = "Lie on your side with your legs straight. Lift your top leg as high as possible, then lower it back down. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Clamshells",
                        Type = "strength",
                        Muscle = "abductors",
                        Difficulty = "beginner",
                        Instructions = "Lie on your side with your legs bent at a 90-degree angle. Keeping your feet together, lift your top knee as high as possible without moving your pelvis. Lower it back down. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Cable Hip Abduction",
                        Type = "strength",
                        Muscle = "abductors",
                        Difficulty = "intermediate",
                        Instructions = "Attach an ankle strap to a low pulley and secure it around your ankle. Stand sideways to the machine and lift your leg away from your body. Return to the starting position. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Lateral Band Walks",
                        Type = "strength",
                        Muscle = "abductors",
                        Difficulty = "intermediate",
                        Instructions = "Place a resistance band around your legs just above your knees. Bend your knees slightly and take steps to the side while keeping tension on the band. Repeat in the opposite direction."
                    },
                    new Exercise
                    {
                        Name = "Standing Abduction Machine",
                        Type = "strength",
                        Muscle = "abductors",
                        Difficulty = "intermediate",
                        Instructions = "Stand in the abduction machine and push your legs outward against the resistance. Slowly return to the starting position. Repeat."
                    },
                    // Adductors
                    new Exercise
                    {
                        Name = "Standing Adduction Machine",
                        Type = "strength",
                        Muscle = "adductors",
                        Difficulty = "beginner",
                        Instructions = "Stand in the adduction machine and push your legs inward against the resistance. Slowly return to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Side Lunges",
                        Type = "strength",
                        Muscle = "adductors",
                        Difficulty = "intermediate",
                        Instructions = "Stand with your feet together. Step to the side with one leg and bend that knee while keeping the other leg straight. Push off your bent leg to return to the starting position. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Cable Hip Adduction",
                        Type = "strength",
                        Muscle = "adductors",
                        Difficulty = "intermediate",
                        Instructions = "Attach an ankle strap to a low pulley and secure it around your ankle. Stand sideways to the machine and pull your leg toward your body. Return to the starting position. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Sumo Squats",
                        Type = "strength",
                        Muscle = "adductors",
                        Difficulty = "beginner",
                        Instructions = "Stand with your feet wider than shoulder-width apart and your toes pointed outward. Squat down by bending your knees and pushing your hips back. Return to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Adductor Machine",
                        Type = "strength",
                        Muscle = "adductors",
                        Difficulty = "beginner",
                        Instructions = "Sit in the adductor machine and push your legs inward against the resistance. Slowly return to the starting position. Repeat."
                    },
                    // Biceps
                    new Exercise
                    {
                        Name = "Incline Hammer Curls",
                        Type = "strength",
                        Muscle = "biceps",
                        Difficulty = "beginner",
                        Instructions = "Seat yourself on an incline bench with a dumbbell in each hand. You should be pressed firmly against the back with your feet together. Allow the dumbbells to hang straight down at your side, holding them with a neutral grip. This will be your starting position. Initiate the movement by flexing at the elbow, attempting to keep the upper arm stationary. Continue to the top of the movement and pause, then slowly return to the start position."
                    },
                    new Exercise
                    {
                        Name = "Barbell Curls",
                        Type = "strength",
                        Muscle = "biceps",
                        Difficulty = "beginner",
                        Instructions = "Stand with your feet shoulder-width apart and hold a barbell with an underhand grip. Curl the barbell up towards your chest, keeping your elbows close to your body. Lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Concentration Curls",
                        Type = "strength",
                        Muscle = "biceps",
                        Difficulty = "intermediate",
                        Instructions = "Sit on a bench and hold a dumbbell in one hand. Rest your elbow on the inside of your thigh. Curl the dumbbell up towards your shoulder, then lower it back down. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Cable Curls",
                        Type = "strength",
                        Muscle = "biceps",
                        Difficulty = "intermediate",
                        Instructions = "Stand facing a cable machine with a handle attachment set at the lowest position. Hold the handle with an underhand grip and curl it towards your shoulder. Slowly lower it back down. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Preacher Curls",
                        Type = "strength",
                        Muscle = "biceps",
                        Difficulty = "intermediate",
                        Instructions = "Sit on a preacher bench with your upper arms resting on the pad. Hold a barbell or dumbbells with an underhand grip. Curl the weight towards your shoulders, then lower it back down. Repeat."
                    },
                    // Calves
                    new Exercise
                    {
                        Name = "Standing Calf Raises",
                        Type = "strength",
                        Muscle = "calves",
                        Difficulty = "beginner",
                        Instructions = "Stand with your feet hip-width apart and your toes on the edge of a raised platform. Raise your heels as high as possible, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Seated Calf Raises",
                        Type = "strength",
                        Muscle = "calves",
                        Difficulty = "beginner",
                        Instructions = "Sit on a calf raise machine with your toes on the platform and your knees under the pads. Raise your heels as high as possible, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Donkey Calf Raises",
                        Type = "strength",
                        Muscle = "calves",
                        Difficulty = "intermediate",
                        Instructions = "Bend at the waist and place your hands on a support. With your partner sitting on your lower back, raise your heels as high as possible, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Calf Press on Leg Press Machine",
                        Type = "strength",
                        Muscle = "calves",
                        Difficulty = "intermediate",
                        Instructions = "Sit on a leg press machine and place your toes on the lower part of the platform. Press the platform away by extending your ankles, then slowly lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Jump Rope",
                        Type = "cardio",
                        Muscle = "calves",
                        Difficulty = "beginner",
                        Instructions = "Hold the handles of a jump rope with the rope behind you. Swing the rope over your head and jump as it passes under your feet. Repeat in a continuous motion."
                    },
                    // Chest
                    new Exercise
                    {
                        Name = "Bench Press",
                        Type = "strength",
                        Muscle = "chest",
                        Difficulty = "beginner",
                        Instructions = "Lie on a flat bench with your feet flat on the floor. Hold a barbell with a grip slightly wider than shoulder-width apart. Lower the barbell to your chest, then press it back up. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Push-Ups",
                        Type = "strength",
                        Muscle = "chest",
                        Difficulty = "beginner",
                        Instructions = "Start in a plank position with your hands slightly wider than shoulder-width apart. Lower your body until your chest nearly touches the floor, then push back up. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Incline Dumbbell Press",
                        Type = "strength",
                        Muscle = "chest",
                        Difficulty = "intermediate",
                        Instructions = "Lie on an incline bench with a dumbbell in each hand. Press the dumbbells up until your arms are fully extended, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Cable Crossovers",
                        Type = "strength",
                        Muscle = "chest",
                        Difficulty = "intermediate",
                        Instructions = "Stand between two cable machines with handles attached at the highest position. Hold a handle in each hand and pull them down and together in front of your body. Slowly return to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Chest Dips",
                        Type = "strength",
                        Muscle = "chest",
                        Difficulty = "intermediate",
                        Instructions = "Hold the parallel bars and lift your body. Lean forward slightly and lower your body until your upper arms are parallel with the floor. Push back up to the starting position. Repeat."
                    },
                    // Forearms
                    new Exercise
                    {
                        Name = "Wrist Curls",
                        Type = "strength",
                        Muscle = "forearms",
                        Difficulty = "beginner",
                        Instructions = "Sit on a bench and hold a barbell with an underhand grip. Rest your forearms on your thighs with your wrists hanging over the edge. Curl your wrists up, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Reverse Wrist Curls",
                        Type = "strength",
                        Muscle = "forearms",
                        Difficulty = "beginner",
                        Instructions = "Sit on a bench and hold a barbell with an overhand grip. Rest your forearms on your thighs with your wrists hanging over the edge. Curl your wrists up, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Farmer's Walk",
                        Type = "strength",
                        Muscle = "forearms",
                        Difficulty = "intermediate",
                        Instructions = "Hold a heavy dumbbell in each hand at your sides. Walk forward for a set distance or time while maintaining a strong grip on the dumbbells. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Towel Pull-Ups",
                        Type = "strength",
                        Muscle = "forearms",
                        Difficulty = "expert",
                        Instructions = "Drape two towels over a pull-up bar and hold one in each hand. Perform pull-ups by pulling your chest to the bar. Lower yourself back down and repeat."
                    },
                    new Exercise
                    {
                        Name = "Plate Pinches",
                        Type = "strength",
                        Muscle = "forearms",
                        Difficulty = "intermediate",
                        Instructions = "Hold two weight plates together with your fingers and thumb. Pinch them together for a set time or until failure. Repeat."
                    },
                    // Glutes
                    new Exercise
                    {
                        Name = "Glute Bridge",
                        Type = "strength",
                        Muscle = "glutes",
                        Difficulty = "beginner",
                        Instructions = "Lie on your back with your knees bent and feet flat on the floor. Lift your hips until your body forms a straight line from your shoulders to your knees. Lower your hips back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Hip Thrusts",
                        Type = "strength",
                        Muscle = "glutes",
                        Difficulty = "intermediate",
                        Instructions = "Sit on the ground with your upper back against a bench and a barbell over your hips. Drive through your heels to lift your hips until your body forms a straight line from your shoulders to your knees. Lower your hips back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Bulgarian Split Squats",
                        Type = "strength",
                        Muscle = "glutes",
                        Difficulty = "intermediate",
                        Instructions = "Stand a few feet in front of a bench with one foot resting on the bench behind you. Lower your body until your front thigh is parallel with the floor, then push back up. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Sumo Deadlifts",
                        Type = "strength",
                        Muscle = "glutes",
                        Difficulty = "intermediate",
                        Instructions = "Stand with your feet wider than shoulder-width apart and your toes pointed outward. Hold a barbell with a mixed grip. Lower the bar to the ground by bending your hips and knees, then lift it back up. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Step-Ups",
                        Type = "strength",
                        Muscle = "glutes",
                        Difficulty = "beginner",
                        Instructions = "Stand in front of a bench or step. Step onto the bench with one foot and push through your heel to lift your body up. Step back down and repeat on the other side."
                    },
                    // Hamstrings
                    new Exercise
                    {
                        Name = "Romanian Deadlifts",
                        Type = "strength",
                        Muscle = "hamstrings",
                        Difficulty = "intermediate",
                        Instructions = "Hold a barbell with a shoulder-width grip. Keep your legs slightly bent and your back straight. Hinge at your hips to lower the bar down your legs, then lift it back up. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Leg Curls",
                        Type = "strength",
                        Muscle = "hamstrings",
                        Difficulty = "beginner",
                        Instructions = "Lie face down on a leg curl machine with your ankles under the pads. Curl your legs up towards your glutes, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Glute-Ham Raises",
                        Type = "strength",
                        Muscle = "hamstrings",
                        Difficulty = "expert",
                        Instructions = "Hook your feet under a secure object and kneel on a padded surface. Lower your torso to the floor by extending your knees, then curl back up to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Good Mornings",
                        Type = "strength",
                        Muscle = "hamstrings",
                        Difficulty = "intermediate",
                        Instructions = "Stand with your feet shoulder-width apart and a barbell on your upper back. Bend at your hips to lower your torso forward, then return to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Single-Leg Deadlifts",
                        Type = "strength",
                        Muscle = "hamstrings",
                        Difficulty = "intermediate",
                        Instructions = "Stand on one leg and hold a dumbbell in the opposite hand. Hinge at your hips to lower the dumbbell towards the floor while extending your free leg behind you. Return to the starting position. Repeat on the other side."
                    },
                    // Lats
                    new Exercise
                    {
                        Name = "Pull-Ups",
                        Type = "strength",
                        Muscle = "lats",
                        Difficulty = "intermediate",
                        Instructions = "Hang from a pull-up bar with an overhand grip. Pull your chest to the bar by squeezing your shoulder blades together. Lower yourself back down and repeat."
                    },
                    new Exercise
                    {
                        Name = "Lat Pulldowns",
                        Type = "strength",
                        Muscle = "lats",
                        Difficulty = "beginner",
                        Instructions = "Sit at a lat pulldown machine and hold the bar with an overhand grip. Pull the bar down to your chest, then slowly return it to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Bent Over Rows",
                        Type = "strength",
                        Muscle = "lats",
                        Difficulty = "intermediate",
                        Instructions = "Hold a barbell with a shoulder-width grip. Bend at your hips and knees, keeping your back straight. Pull the barbell to your lower chest, then lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Single-Arm Dumbbell Rows",
                        Type = "strength",
                        Muscle = "lats",
                        Difficulty = "intermediate",
                        Instructions = "Place one knee and hand on a bench for support. Hold a dumbbell in the opposite hand and pull it towards your hip. Lower it back down. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "T-Bar Rows",
                        Type = "strength",
                        Muscle = "lats",
                        Difficulty = "intermediate",
                        Instructions = "Stand over a T-bar row machine with your feet shoulder-width apart. Hold the handles and pull them towards your chest. Lower them back down. Repeat."
                    },
                    // Lower Back
                    new Exercise
                    {
                        Name = "Hyperextensions",
                        Type = "strength",
                        Muscle = "lower_back",
                        Difficulty = "beginner",
                        Instructions = "Lie face down on a hyperextension bench with your ankles secured. Lower your torso towards the floor, then raise it back up until your body forms a straight line. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Deadlifts",
                        Type = "strength",
                        Muscle = "lower_back",
                        Difficulty = "intermediate",
                        Instructions = "Stand with your feet shoulder-width apart and a barbell in front of you. Bend at your hips and knees to grasp the bar. Lift the bar by extending your hips and knees, then lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Good Mornings",
                        Type = "strength",
                        Muscle = "lower_back",
                        Difficulty = "intermediate",
                        Instructions = "Stand with your feet shoulder-width apart and a barbell on your upper back. Bend at your hips to lower your torso forward, then return to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Supermans",
                        Type = "strength",
                        Muscle = "lower_back",
                        Difficulty = "beginner",
                        Instructions = "Lie face down on the floor with your arms extended in front of you. Lift your arms, chest, and legs off the ground simultaneously. Hold briefly, then lower back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Back Extensions",
                        Type = "strength",
                        Muscle = "lower_back",
                        Difficulty = "beginner",
                        Instructions = "Lie face down on a back extension bench with your ankles secured. Lower your torso towards the floor, then raise it back up until your body forms a straight line. Repeat."
                    },
                    // Middle Back
                    new Exercise
                    {
                        Name = "Bent Over Rows",
                        Type = "strength",
                        Muscle = "middle_back",
                        Difficulty = "intermediate",
                        Instructions = "Hold a barbell with a shoulder-width grip. Bend at your hips and knees, keeping your back straight. Pull the barbell to your lower chest, then lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Seated Rows",
                        Type = "strength",
                        Muscle = "middle_back",
                        Difficulty = "beginner",
                        Instructions = "Sit at a rowing machine and hold the handle with both hands. Pull the handle towards your lower chest, then slowly return it to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Single-Arm Dumbbell Rows",
                        Type = "strength",
                        Muscle = "middle_back",
                        Difficulty = "intermediate",
                        Instructions = "Place one knee and hand on a bench for support. Hold a dumbbell in the opposite hand and pull it towards your hip. Lower it back down. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Inverted Rows",
                        Type = "strength",
                        Muscle = "middle_back",
                        Difficulty = "beginner",
                        Instructions = "Lie under a bar set at hip height. Hold the bar with an overhand grip and keep your body straight. Pull your chest to the bar, then lower yourself back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "T-Bar Rows",
                        Type = "strength",
                        Muscle = "middle_back",
                        Difficulty = "intermediate",
                        Instructions = "Stand over a T-bar row machine with your feet shoulder-width apart. Hold the handles and pull them towards your chest. Lower them back down. Repeat."
                    },
                    // Neck
                    new Exercise
                    {
                        Name = "Neck Flexion",
                        Type = "strength",
                        Muscle = "neck",
                        Difficulty = "beginner",
                        Instructions = "Sit on a bench and place a weight plate on your forehead. Slowly nod your head forward and back up. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Neck Extension",
                        Type = "strength",
                        Muscle = "neck",
                        Difficulty = "beginner",
                        Instructions = "Lie face down on a bench with your head hanging off the end. Place a weight plate on the back of your head and lift your head up. Lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Neck Side Flexion",
                        Type = "strength",
                        Muscle = "neck",
                        Difficulty = "beginner",
                        Instructions = "Sit on a bench and place a weight plate on the side of your head. Slowly tilt your head towards your shoulder and back up. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Neck Isometrics",
                        Type = "strength",
                        Muscle = "neck",
                        Difficulty = "beginner",
                        Instructions = "Place your hand on your forehead and push against it without moving your head. Hold for a few seconds, then release. Repeat in all directions."
                    },
                    new Exercise
                    {
                        Name = "Shrugs",
                        Type = "strength",
                        Muscle = "neck",
                        Difficulty = "beginner",
                        Instructions = "Hold a dumbbell in each hand at your sides. Shrug your shoulders up towards your ears, then lower them back down. Repeat."
                    },
                    // Quadriceps
                    new Exercise
                    {
                        Name = "Squats",
                        Type = "strength",
                        Muscle = "quadriceps",
                        Difficulty = "beginner",
                        Instructions = "Stand with your feet shoulder-width apart. Lower your body by bending your hips and knees until your thighs are parallel to the floor. Push back up to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Leg Press",
                        Type = "strength",
                        Muscle = "quadriceps",
                        Difficulty = "beginner",
                        Instructions = "Sit on a leg press machine with your feet on the platform. Push the platform away by extending your hips and knees, then slowly lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Lunges",
                        Type = "strength",
                        Muscle = "quadriceps",
                        Difficulty = "beginner",
                        Instructions = "Stand with your feet together. Step forward with one leg and lower your body until your front thigh is parallel to the floor. Push back up to the starting position. Repeat on the other side."
                    },
                    new Exercise
                    {
                        Name = "Leg Extensions",
                        Type = "strength",
                        Muscle = "quadriceps",
                        Difficulty = "beginner",
                        Instructions = "Sit on a leg extension machine with your ankles under the pads. Extend your legs until they are straight, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Bulgarian Split Squats",
                        Type = "strength",
                        Muscle = "quadriceps",
                        Difficulty = "intermediate",
                        Instructions = "Stand a few feet in front of a bench with one foot resting on the bench behind you. Lower your body until your front thigh is parallel with the floor, then push back up. Repeat on the other side."
                    },
                    // Traps
                    new Exercise
                    {
                        Name = "Barbell Shrugs",
                        Type = "strength",
                        Muscle = "traps",
                        Difficulty = "beginner",
                        Instructions = "Hold a barbell with a shoulder-width grip. Shrug your shoulders up towards your ears, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Dumbbell Shrugs",
                        Type = "strength",
                        Muscle = "traps",
                        Difficulty = "beginner",
                        Instructions = "Hold a dumbbell in each hand at your sides. Shrug your shoulders up towards your ears, then lower them back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Upright Rows",
                        Type = "strength",
                        Muscle = "traps",
                        Difficulty = "beginner",
                        Instructions = "Hold a barbell with a shoulder-width grip. Pull the barbell up to your chest, then lower it back down. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Face Pulls",
                        Type = "strength",
                        Muscle = "traps",
                        Difficulty = "intermediate",
                        Instructions = "Attach a rope to a cable machine at face height. Hold the rope with both hands and pull it towards your face, squeezing your shoulder blades together. Slowly return to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Farmer's Walk",
                        Type = "strength",
                        Muscle = "traps",
                        Difficulty = "intermediate",
                        Instructions = "Hold a heavy dumbbell in each hand at your sides. Walk forward for a set distance or time while maintaining a strong grip on the dumbbells. Repeat."
                    },
                    // Triceps
                    new Exercise
                    {
                        Name = "Triceps Dips",
                        Type = "strength",
                        Muscle = "triceps",
                        Difficulty = "beginner",
                        Instructions = "Hold the parallel bars and lift your body. Lower your body until your upper arms are parallel with the floor, then push back up to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Skull Crushers",
                        Type = "strength",
                        Muscle = "triceps",
                        Difficulty = "intermediate",
                        Instructions = "Lie on a bench with a barbell. Extend your arms and hold the barbell above your chest. Bend your elbows to lower the barbell towards your forehead, then extend your arms to lift it back up. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Overhead Triceps Extension",
                        Type = "strength",
                        Muscle = "triceps",
                        Difficulty = "beginner",
                        Instructions = "Hold a dumbbell with both hands and lift it over your head. Lower the dumbbell behind your head by bending your elbows, then lift it back up. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Triceps Pushdown",
                        Type = "strength",
                        Muscle = "triceps",
                        Difficulty = "beginner",
                        Instructions = "Stand at a cable machine with a handle attached at the highest position. Hold the handle with both hands and push it down until your arms are fully extended. Slowly return to the starting position. Repeat."
                    },
                    new Exercise
                    {
                        Name = "Close-Grip Bench Press",
                        Type = "strength",
                        Muscle = "triceps",
                        Difficulty = "intermediate",
                        Instructions = "Lie on a bench and hold a barbell with a grip narrower than shoulder-width. Lower the barbell to your chest, then press it back up. Repeat."
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
