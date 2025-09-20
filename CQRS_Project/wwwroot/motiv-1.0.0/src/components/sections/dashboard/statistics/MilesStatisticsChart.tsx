import * as echarts from 'echarts/core';
import { TooltipComponent, GridComponent } from 'echarts/components';
import { BarChart } from 'echarts/charts';
import { CanvasRenderer } from 'echarts/renderers';
import { MutableRefObject, useMemo } from 'react';
import EChartsReactCore from 'echarts-for-react/lib/core';
import { useTheme } from '@mui/material';
import ReactEchart from 'components/base/ReactEchart';
import { TooltipFormatter } from 'helpers/TooltipFormatter';
import { EChartsOption } from 'echarts-for-react';

echarts.use([TooltipComponent, GridComponent, BarChart, CanvasRenderer]);

interface ChartProps {
  barChartRef: MutableRefObject<EChartsReactCore | null>;
  data: number[] | null;
  style?: {
    height?: number;
    width?: number;
  };
}

const MilesStatisticsChart = ({ barChartRef, data, style }: ChartProps) => {
  const theme = useTheme();

  const getBarChartOption = useMemo(() => {
    const barChartOption: EChartsOption = {
      color: theme.palette.primary.main,
      tooltip: {
        trigger: 'axis',
        confine: true,
        axisPointer: {
          type: 'line',
        },
        backgroundColor: theme.palette.neutral.dark,
        textStyle: {
          color: theme.palette.common.white,
          fontSize: theme.typography.fontSize / 1.4,
        },

        formatter: TooltipFormatter,
        padding: [2, 9, 2, 8],
        // position: function (pos: Array<number>) {
        //   return [pos[0], '30%'];
        // },
        extraCssText: 'border-radius: 0.3125rem;',
      },

      grid: {
        left: '0',
        right: '1',
        // left: '-4%',
        // right: '-4%',
        top: '0',
        bottom: '12%',
      },

      textStyle: {
        fontFamily: 'DM Sans',
        fontWeight: theme.typography.fontWeightRegular,
      },

      xAxis: [
        {
          type: 'category',
          data: ['1 PM', '2 PM', '3 PM', '4 PM', '5 PM', '6 PM', '7 PM'],
          axisTick: {
            show: false,
          },
          splitLine: {
            show: true,
            lineStyle: {
              color: theme.palette.neutral.lighter,
            },
          },
          axisLine: {
            show: false,
          },
          axisLabel: { fontSize: theme.typography.fontSize },
        },
      ],

      yAxis: [
        {
          type: 'value',
          splitLine: {
            show: false,
          },
          axisLabel: {
            show: false,
          },
        },
      ],

      series: [
        {
          type: 'bar',
          barWidth: 28,
          data: [120, 80, 157, 60, 130, 40, 90],
          // data: data,
          cursor: 'none',
          emphasis: {
            focus: 'series',
            itemStyle: {
              color: theme.palette.primary.light,
            },
          },
        },
      ],
    };

    return barChartOption;
  }, [data]);

  return (
    <ReactEchart echarts={echarts} option={getBarChartOption} ref={barChartRef} style={style} />
  );
};

export default MilesStatisticsChart;
