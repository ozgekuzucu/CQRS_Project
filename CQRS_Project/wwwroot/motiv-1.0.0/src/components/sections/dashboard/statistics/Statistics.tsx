import { Grid } from '@mui/material';
import MilesStatistics from './MilesStatistics';
import CarStatistics from './CarStatistics';

const Statistics = () => {
  return (
    <Grid container spacing={3.75}>
      <Grid item xs={12} lg={6}>
        <MilesStatistics />
      </Grid>
      <Grid item xs={12} lg={6}>
        <CarStatistics />
      </Grid>
    </Grid>
  );
};

export default Statistics;
