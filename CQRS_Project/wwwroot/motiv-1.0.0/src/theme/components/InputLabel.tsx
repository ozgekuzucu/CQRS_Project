import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';

const InputLabel: Components<Omit<Theme, 'components'>>['MuiInputLabel'] = {
  styleOverrides: {
    animated: ({ theme }) => ({
      color: theme.palette.text.primary,
      marginBottom: theme.spacing(1.5),
      fontWeight: theme.typography.fontWeightMedium,
      lineHeight: 1.301,
    }),
  },
};
export default InputLabel;
