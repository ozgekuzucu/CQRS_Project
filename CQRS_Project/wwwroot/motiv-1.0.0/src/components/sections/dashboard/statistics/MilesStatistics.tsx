import { Box, Paper, Stack, Typography } from '@mui/material';
import { useRef, useState } from 'react';
import EChartsReactCore from 'echarts-for-react/lib/core';
import { barChart } from 'data/dashboard/chartData';
import MilesStatisticsChart from './MilesStatisticsChart';
import ChartLegend from './ChartLegend';

type ChartDataKey = keyof typeof barChart;

const MilesStatistics = () => {
  const barChartRef = useRef<null | EChartsReactCore>(null);
  const [selectedOption, setSelectedOption] = useState('day');

  let barChartData: number[] | null = null;
  const handleChartLegend = (value: ChartDataKey) => {
    setSelectedOption(value);
    barChartData = barChart[value];

    if (barChartRef.current) {
      const chartInstance = barChartRef.current.getEchartsInstance();
      chartInstance.setOption({
        series: [
          {
            data: barChartData,
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
          Miles{' '}
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
          <Stack direction="row" columnGap={1.25} alignItems={'center'}>
            <ChartLegend
              active={selectedOption === 'day'}
              label="Day"
              onHandleClick={handleChartLegend}
            />
            <ChartLegend
              active={selectedOption === 'week'}
              label="Week"
              onHandleClick={handleChartLegend}
            />
            <ChartLegend
              active={selectedOption === 'month'}
              label="Month"
              onHandleClick={handleChartLegend}
            />
          </Stack>

          <Typography variant="subtitle2" component="p" sx={{ color: 'grey.700' }}>
            256 Miles
          </Typography>
        </Stack>
      </Stack>

      <MilesStatisticsChart barChartRef={barChartRef} data={barChartData} style={{ height: 223 }} />
    </Paper>
  );
};

export default MilesStatistics;
