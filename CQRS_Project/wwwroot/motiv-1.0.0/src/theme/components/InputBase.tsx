import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';

const InputBase: Components<Omit<Theme, 'components'>>['MuiInputBase'] = {
  styleOverrides: {
    inputAdornedStart: ({ theme }) => ({
      paddingTop: `${theme.spacing(0.5)} !important`,
    }),

    inputHiddenLabel: ({ theme }) => ({
      paddingTop: `${theme.spacing(1.5)} !important`,
      paddingBottom: `${theme.spacing(1.5)} !important`,
    }),
  },
};

export default InputBase;
