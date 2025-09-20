import { Components, Theme } from '@mui/material';

const ListItemButton: Components<Omit<Theme, 'components'>>['MuiListItemButton'] = {
  styleOverrides: {
    gutters: ({ theme }) => ({
      borderRadius: theme.shape.borderRadius * 1.5,
      padding: theme.spacing(0.875, 1),
      '&.Mui-selected': {
        backgroundColor: theme.palette.action.selected,
      },
      '&:hover': {
        backgroundColor: theme.palette.action.hover,
      },
    }),
  },
};
export default ListItemButton;
