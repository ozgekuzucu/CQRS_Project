import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';

const Pagination: Components<Omit<Theme, 'components'>>['MuiPagination'] = {
  styleOverrides: {
    ul: { flexWrap: 'nowrap' },
  },
};

export default Pagination;
