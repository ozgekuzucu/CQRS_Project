import { Theme } from '@mui/material';
import { Components } from '@mui/material/styles/components';
import type {} from '@mui/x-data-grid/themeAugmentation';

const DataGrid: Components<Omit<Theme, 'components'>>['MuiDataGrid'] = {
  defaultProps: {
    disableColumnMenu: true,
    disableRowSelectionOnClick: true,
    hideFooter: true,
  },
  styleOverrides: {
    root: ({ theme }) => ({
      borderRadius: 0,
      borderLeft: 0,
      borderRight: 0,
      borderBottom: 0,
      borderColor: theme.palette.grey.A700,
      '--DataGrid-rowBorderColor': theme.palette.grey.A700,
    }),

    columnHeader: {
      paddingLeft: 0,
      '&:focus-within': { outline: 'none' },
    },

    columnHeaderTitle: ({ theme }) => ({
      color: theme.palette.grey[600],
    }),

    cell: ({ theme }) => ({
      paddingLeft: 0,
      paddingRight: 0,
      fontSize: theme.typography.fontSize - 1,
      '&:focus': {
        outline: 'none',
      },
    }),

    columnSeparator: {
      display: 'none',
    },

    sortIcon: ({ theme }) => ({
      color: theme.palette.primary.main,
    }),

    withBorderColor: ({ theme }) => ({ borderColor: theme.palette.grey.A700 }),
  },
};

export default DataGrid;
