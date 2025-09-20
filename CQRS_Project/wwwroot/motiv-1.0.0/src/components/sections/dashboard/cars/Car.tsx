import { Box, IconButton, Link, Paper, Stack, Typography } from '@mui/material';
import Image from 'components/base/Image';
import CirculationIcon from 'components/icons/car/Icon1';
import LoopIcon from 'components/icons/car/Icon2';
import SettingsIcon from 'components/icons/car/Icon3';
import ThunderIcon from 'components/icons/car/Icon4';
import { ICar } from 'types/types';

const Car = ({ car }: { car: ICar }) => {
  const { imageUrl, recommendation, modelName, mileage, costPerHour, backgroundColor } = car;

  return (
    <Paper sx={{ bgcolor: backgroundColor }}>
      <Stack
        rowGap={1.375}
        component={Link}
        href="#!"
        sx={(theme) => ({
          p: theme.spacing(1.5, 3),
          color: 'common.black',
        })}
      >
        <Stack direction="row" alignItems="center" columnGap={1.25} ml={-0.5}>
          <IconButton aria-label="circulation-icon" sx={{ p: 0.5 }}>
            <CirculationIcon sx={{ color: 'grey.600' }} />
          </IconButton>
          <Typography variant="body1">{recommendation}% Recommend</Typography>
        </Stack>

        <Image
          src={imageUrl}
          alt="car-image"
          sx={{
            width: 1,
            height: { xs: 106 },
            objectFit: { xs: 'contain' },
          }}
        />

        <Box>
          <Typography variant="h3" component="h2">
            {modelName}
          </Typography>

          <Stack
            direction="row"
            justifyContent="space-between"
            alignItems="center"
            color="grey.600"
          >
            <Stack direction="row" alignItems="center" columnGap={1} ml={-0.5}>
              <IconButton aria-label="loop-icon" sx={{ p: 0.5 }}>
                <LoopIcon fontSize="small" />
              </IconButton>
              <Typography variant="body2" component="span">
                {mileage}K
              </Typography>
              <IconButton aria-label="settings-icon" sx={{ p: 1 }}>
                <SettingsIcon sx={{ fontSize: 'fontSize' }} />
              </IconButton>
              <IconButton aria-label="thunder-icon" sx={{ p: 1 }}>
                <ThunderIcon sx={{ fontSize: 'fontSize' }} />
              </IconButton>
            </Stack>

            <Typography variant="body2" component="span">
              ${costPerHour}/h
            </Typography>
          </Stack>
        </Box>
      </Stack>
    </Paper>
  );
};

export default Car;
