import { SvgIconProps } from '@mui/material';

interface Item {
  id: number;
  icon: (props: SvgIconProps) => JSX.Element;
  title: string;
  path?: string;
  active?: boolean;
}

export interface SubItem {
  id: number;
  title: string;
  path?: string;
  active?: boolean;
}

export interface DrawerItem extends Item {
  collapsible: boolean;
  subList?: SubItem[];
}

export interface LanguageItem {
  id: number;
  value: string;
  label: string;
  icon: string;
}

export interface MenuItem {
  id: number;
  label: string;
  icon: string;
}

export interface IFactor {
  id: number;
  icon: (props: SvgIconProps) => JSX.Element;
  title: string;
  color: string;
  value: number;
  max?: number;
}

export interface ICar {
  id: number;
  recommendation: number;
  imageUrl: string;
  modelName: string;
  mileage: number;
  costPerHour: number;
  backgroundColor: string;
}
