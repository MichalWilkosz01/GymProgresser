export interface Exercise {
    id: number;
    name: string;
    category: string;
    label: string;
    isVerified: boolean;
}

export interface ExerciseDetails extends Exercise {
    description: string;
}