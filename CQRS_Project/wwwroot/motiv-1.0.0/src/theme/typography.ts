import { TypographyOptions } from '@mui/material/styles/createTypography';

const typography: TypographyOptions = {
  fontFamily: ['DM Sans', 'Inter', 'sans-serif'].join(','),

  h1: {
    fontWeight: 700,
    fontSize: '1.875rem', // 30px
    lineHeight: 1.302, // 39.06px
  },

  h2: {
    fontWeight: 700,
    fontSize: '1.5rem', // 24px
    lineHeight: 1.302, // 31.25px
  },

  h3: {
    fontWeight: 700,
    fontSize: '1.25rem', // 20px
    lineHeight: 1.302, //26.04px
  },

  h4: {
    fontWeight: 700,
    fontSize: '1.25rem', // 20px
    lineHeight: 1.21, // 24.2px
    fontFamily: 'Inter',
  },

  h5: {
    fontWeight: 500,
    fontSize: '1.125rem', // 18px
    lineHeight: 1.302, // 23.44px
  },

  h6: {
    fontWeight: 500,
    fontSize: '1rem', // 16px
    lineHeight: 1.301, // 20.83px
  },

  subtitle1: {
    fontWeight: 400,
    fontSize: '1.125rem', // 18px
    lineHeight: 1.302, //23.44px
  },

  subtitle2: {
    fontWeight: 700,
    fontSize: '0.875rem', // 14px
    lineHeight: 1.302, // 18.23px
  },

  body1: {
    fontWeight: 700,
    fontSize: '1rem', // 16px
    lineHeight: 1.301, // 20.83px
  },

  body2: {
    fontWeight: 500,
    fontSize: '0.875rem', // 14px
    lineHeight: 1.302, // 18.23px
  },

  button: {
    fontWeight: 500,
    fontSize: '1.125rem', // 18px
    lineHeight: 1.302, // 23.44px
    textTransform: 'none',
  },

  caption: {
    fontFamily: "'DM Sans',sans-serif",
  },
};

export default typography;
