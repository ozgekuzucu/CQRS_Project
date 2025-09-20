import { Grid } from '@mui/material';
import { IFactor } from 'types/types';
import Factor from './Factor';

const Factors = ({ factors }: { factors: IFactor[] }) => {
  return (
    <Grid container spacing={3.75}>
      {factors.map((factor) => (
        <Grid item xs={12} sm={6} lg key={factor?.id}>
          <Factor factor={factor} />
        </Grid>
      ))}
    </Grid>
  );
};

export default Factors;
