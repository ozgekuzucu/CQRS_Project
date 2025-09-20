import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';

const Paper: Components<Omit<Theme, 'components'>>['MuiPaper'] = {
  defaultProps: {
    elevation: 0,
  },
  styleOverrides: {
    elevation0: ({ theme }) => ({
      borderRadius: theme.shape.borderRadius * 3.5,
    }),
  },
};

export default Paper;
