import * as echarts from 'echarts/core';
import { TooltipComponent, TooltipComponentOption } from 'echarts/components';
import { GaugeChart, GaugeSeriesOption } from 'echarts/charts';
import { SVGRenderer } from 'echarts/renderers';
import ReactEchart from 'components/base/ReactEchart';
import { useTheme } from '@mui/material';
import { useMemo } from 'react';

echarts.use([TooltipComponent, GaugeChart, SVGRenderer]);

type EChartsOption = echarts.ComposeOption<TooltipComponentOption | GaugeSeriesOption>;

interface ChartProps {
  data: { color: string; value: number; max: number };
  isHovered: boolean;
  style?: {
    height?: number;
    width?: number;
  };
}

const FactorChart = ({ data: doughnutChartData, isHovered, style }: ChartProps) => {
  const { color, value, max } = doughnutChartData;

  const theme = useTheme();

  const getDoughnutChartOption = useMemo(() => {
    const doughnutChartOption: EChartsOption = {
      tooltip: { show: false },
      series: [
        {
          type: 'gauge',
          min: 0,
          max,
          startAngle: 220,
          endAngle: -35,
          radius: '100%',
          detail: {
            formatter: (value: number) => (value < 100 ? `${value}%` : `${value}k`),
            offsetCenter: [0, 0],
            fontSize: theme.typography.fontSize + 10,
            fontFamily: theme.typography.caption.fontFamily,
            color: isHovered ? theme.palette.common.white : theme.palette.neutral.dark,
          },
          itemStyle: {
            color: isHovered ? theme.palette.common.white : color,
          },
          progress: { roundCap: true, show: true, width: 10 },
          axisLine: {
            roundCap: true,
            lineStyle: {
              width: 10,
              color: isHovered
                ? [[1, theme.palette.secondary.lighter]]
                : [[1, theme.palette.grey.A100]],
            },
          },
          axisTick: { show: false },
          splitLine: { show: false },
          axisLabel: { show: false },
          pointer: { show: false },
          data: [{ value }],
        },
      ],
    };

    return doughnutChartOption;
  }, [theme, isHovered]);

  return <ReactEchart echarts={echarts} option={getDoughnutChartOption} style={style} />;
};

export default FactorChart;
