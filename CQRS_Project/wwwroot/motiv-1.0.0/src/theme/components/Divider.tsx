import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';

const Divider: Components<Omit<Theme, 'components'>>['MuiDivider'] = {
  styleOverrides: {
    wrapper: ({ theme }) => ({
      fontFamily: theme.typography.h4.fontFamily,
      fontWeight: theme.typography.fontWeightMedium,
      fontSize: theme.typography.fontSize + 4,
      lineHeight: 1.5,
      color: theme.palette.neutral.main,
    }),
  },
};

export default Divider;
