import { Paper, Typography, Stack, alpha, useTheme } from '@mui/material';
import { useState } from 'react';
import { IFactor } from 'types/types';
import FactorChart from './FactorChart';

const Factor = ({ factor }: { factor: IFactor }) => {
  const theme = useTheme();

  const [isHovered, setIsHovered] = useState(false);

  const { icon: Icon, color, title, value, max = 100 } = factor;

  const [paletteOption, simplePaletteColorOption] = color.split('.') as [
    keyof typeof theme.palette,
    keyof (typeof theme.palette)[keyof typeof theme.palette],
  ];

  const factorColor = theme.palette[paletteOption][simplePaletteColorOption];
  const doughnutChartData = { color: factorColor, value, max };

  return (
    <Paper
      sx={{
        transition: 'background-color 0.8s',
        '&:hover': {
          bgcolor: 'secondary.main',
          color: 'grey.100',
          cursor: 'pointer',
          '& .iconWrapper': {
            bgcolor: 'secondary.light',
          },
        },
      }}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <Stack alignItems="center" rowGap={3.75} sx={{ py: 2.5, height: 1 }}>
        <Stack alignItems="center" rowGap={1.25}>
          <Stack
            justifyContent="center"
            alignItems="center"
            className="iconWrapper"
            sx={{
              height: 38,
              width: 38,
              bgcolor: alpha(factorColor, 0.1),
              borderRadius: '50%',
            }}
          >
            <Icon sx={[isHovered ? { color: 'grey.100' } : { color: factorColor }]} />
          </Stack>
          <Typography variant="h2">{title}</Typography>
        </Stack>

        <FactorChart
          data={doughnutChartData}
          isHovered={isHovered}
          style={{ height: 112, width: 112 }}
        />
      </Stack>
    </Paper>
  );
};

export default Factor;
