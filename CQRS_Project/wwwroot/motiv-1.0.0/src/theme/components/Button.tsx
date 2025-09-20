import { Components, Theme } from '@mui/material';

const Button: Components<Omit<Theme, 'components'>>['MuiButton'] = {
  defaultProps: {
    disableElevation: true,
  },
  styleOverrides: {
    root: ({ theme }) => ({
      textTransform: 'none',
      borderRadius: theme.shape.borderRadius * 2,
      fontSize: theme.typography.fontSize, // 14px
      padding: theme.spacing(1, 2),
      lineHeight: 1.302,
    }),

    text: ({ theme }) => ({
      color: theme.palette.grey[700],
    }),

    sizeSmall: ({ theme }) => ({
      borderRadius: theme.shape.borderRadius * 6, // 24px
      fontSize: theme.typography.fontSize / 1.4, // 10px
      padding: theme.spacing(0.5, 1.5), // 4px, 12px
      fontWeight: theme.typography.fontWeightBold,
    }),

    sizeLarge: ({ theme }) => ({
      fontSize: theme.typography.htmlFontSize, // 16px
      padding: theme.spacing(1.5, 2.5), // 12px, 20px
    }),

    outlinedPrimary: ({ theme }) => ({
      color: theme.palette.neutral.dark,
      borderColor: theme.palette.grey.A400,
    }),

    startIcon: {
      '& > *:first-of-type': {
        fontSize: 20,
      },
    },

    endIcon: {
      '& > *:first-of-type': {
        fontSize: 14,
      },
    },
  },
};

export default Button;
