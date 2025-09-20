import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';

const Menu: Components<Omit<Theme, 'components'>>['MuiMenu'] = {
  styleOverrides: {
    list: { padding: 0 },
  },
};

export default Menu;
