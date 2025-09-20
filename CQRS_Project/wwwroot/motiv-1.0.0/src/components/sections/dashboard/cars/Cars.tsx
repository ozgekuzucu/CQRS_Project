import { Grid } from '@mui/material';
import Car from './Car';
import { ICar } from 'types/types';

const Cars = ({ cars }: { cars: ICar[] }) => {
  return (
    <Grid container spacing={3.75}>
      {cars.map((car) => (
        <Grid item key={car.id} xs={12} sm={6} lg>
          <Car car={car} />
        </Grid>
      ))}
    </Grid>
  );
};

export default Cars;
