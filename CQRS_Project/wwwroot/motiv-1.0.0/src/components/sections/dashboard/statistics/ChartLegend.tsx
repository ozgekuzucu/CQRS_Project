import { Button } from '@mui/material';

type ChartLegendValue = 'day' | 'week' | 'month';

interface ChartLegendProps {
  active: boolean;
  label: string;
  color?: 'primary' | 'secondary' | 'warning';
  onHandleClick: (value: ChartLegendValue) => void;
}
const ChartLegend = ({ active, label, color, onHandleClick }: ChartLegendProps) => {
  return (
    <Button
      variant={active ? 'contained' : 'text'}
      size="small"
      onClick={() => onHandleClick(label.toLowerCase() as ChartLegendValue)}
      color={color}
      sx={{ minWidth: 'auto' }}
    >
      {label}
    </Button>
  );
};

export default ChartLegend;
