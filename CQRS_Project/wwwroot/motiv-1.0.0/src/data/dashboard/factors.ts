import Blood from 'components/icons/factor/Blood';
import Lightning from 'components/icons/factor/Lightning';
import Range from 'components/icons/factor/Range';
import Tier from 'components/icons/factor/Tier';
import { IFactor } from 'types/types';

export const factors: IFactor[] = [
  {
    id: 1,
    icon: Lightning,
    title: 'Energy',
    color: 'primary.main',
    value: 45,
  },
  {
    id: 2,
    icon: Range,
    title: 'Range',
    color: 'error.light',
    value: 157,
    max: 300,
  },
  {
    id: 3,
    icon: Blood,
    title: 'Break Fluid',
    color: 'secondary.main',
    value: 9,
  },
  {
    id: 4,
    icon: Tier,
    title: 'Tire Wear',
    color: 'warning.darker',
    value: 25,
  },
];
