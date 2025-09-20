import { Box, Paper, Stack, Typography } from '@mui/material';
import { useRef, useState } from 'react';
import EChartsReactCore from 'echarts-for-react/lib/core';
import CarStatisticsChart from './CarStatisticsChart';
import { areaChart } from 'data/dashboard/chartData';
import ChartLegend from './ChartLegend';

type ChartDataKey = keyof typeof areaChart;

const CarStatistics = () => {
  const areaChartRef = useRef<null | EChartsReactCore>(null);
  const [selectedOption, setSelectedOption] = useState('day');

  let areaChartData: number[] | null = null;
  const handleChartLegend = (value: ChartDataKey) => {
    setSelectedOption(value);
    areaChartData = areaChart[value];

    if (areaChartRef.current) {
      const chartInstance = areaChartRef.current.getEchartsInstance();
      chartInstance.setOption({
        series: [
          {
            data: areaChartData,
          },
        ],
      });
    }
  };

  return (
    <Paper
      sx={(theme) => ({
        p: theme.spacing(1.875, 3, 1.25, 3),
      })}
    >
      <Stack rowGap={3} sx={{ mb: 1.75 }}>
        <Typography variant="h3">
          Car{' '}
          <Box component="span" sx={{ fontWeight: 'fontWeightRegular' }}>
            Statistics
          </Box>
        </Typography>

        <Stack
          sx={{
            flexDirection: { sm: 'row' },
            justifyContent: { sm: 'space-between' },
            alignItems: { sm: 'center' },
            rowGap: { xs: 'inherit' },
          }}
        >
          <Typography variant="subtitle2" component="p" sx={{ color: 'grey.700' }}>
            20 February 2022
          </Typography>

          <Stack
            direction="row"
            columnGap={1.25}
            alignItems="center"
            bgcolor="neutral.lighter"
            borderRadius={6}
            width="max-content"
          >
            <ChartLegend
              active={selectedOption === 'day'}
              label="Day"
              color="warning"
              onHandleClick={handleChartLegend}
            />
            <ChartLegend
              active={selectedOption === 'week'}
              label="Week"
              color="warning"
              onHandleClick={handleChartLegend}
            />
            <ChartLegend
              active={selectedOption === 'month'}
              label="Month"
              color="warning"
              onHandleClick={handleChartLegend}
            />
          </Stack>
        </Stack>
      </Stack>

      <CarStatisticsChart
        areaChartRef={areaChartRef}
        data={areaChartData}
        style={{ height: 223 }}
      />
    </Paper>
  );
};

export default CarStatistics;
