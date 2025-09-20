import { ICar } from 'types/types';
import car1 from 'assets/car-1.webp';
import car2 from 'assets/car-2.webp';
import car3 from 'assets/car-3.webp';

const carImages: Record<number, string> = { 1: car1, 2: car2, 3: car3 };

export const cars: ICar[] = [
  {
    id: 1,
    imageUrl: carImages[1],
    recommendation: 64,
    modelName: 'Mini Cooper',
    mileage: 132,
    costPerHour: 32,
    backgroundColor: 'warning.lighter',
  },
  {
    id: 2,
    recommendation: 74,
    imageUrl: carImages[2],
    modelName: 'Porsche 911 Carrera',
    mileage: 130,
    costPerHour: 28,
    backgroundColor: 'primary.lighter',
  },
  {
    id: 3,
    recommendation: 74,
    imageUrl: carImages[3],
    modelName: 'Porsche 911 Carrera',
    mileage: 130,
    costPerHour: 28,
    backgroundColor: 'error.lighter',
  },
];
