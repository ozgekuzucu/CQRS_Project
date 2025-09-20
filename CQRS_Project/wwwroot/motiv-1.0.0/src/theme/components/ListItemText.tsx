import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';

const ListItemText: Components<Omit<Theme, 'components'>>['MuiListItemText'] = {
  defaultProps: { disableTypography: true },
  styleOverrides: {
    root: ({ theme }) => ({
      color: theme.palette.grey[700],
      margin: 0,
      fontSize: theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightMedium,
    }),
  },
};

export default ListItemText;
