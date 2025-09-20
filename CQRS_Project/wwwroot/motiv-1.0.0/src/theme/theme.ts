import { createTheme } from '@mui/material';
import palette from './palette';
import typography from './typography';
import CssBaseline from './components/CssBaseline';
import AppBar from './components/AppBar';
import Toolbar from './components/Toolbar';
import Drawer from './components/Drawer';
import Link from './components/Link';
import ListItemButton from './components/ListItemButton';
import ListItemIcon from './components/ListItemIcon';
import ListItemText from './components/ListItemText';
import Paper from './components/Paper';
import Stack from './components/Stack';
import Button from './components/Button';
import InputLabel from './components/InputLabel';
import InputAdornment from './components/InputAdornment';
import FilledInput from './components/FilledInput';
import OutlinedInput from './components/OutlinedInput';
import DataGrid from './components/DataGrid';
import Pagination from './components/Pagination';
import Divider from './components/Divider';
import shadows from './shadows';
import MenuItem from './components/MenuItem';
import Menu from './components/Menu';
import InputBase from './components/InputBase';

export const theme = createTheme({
  palette,
  typography,
  mixins: {
    toolbar: {
      minHeight: 78,
    },
  },
  components: {
    MuiAppBar: AppBar,
    MuiToolbar: Toolbar,
    MuiPaper: Paper,
    MuiDrawer: Drawer,

    MuiButton: Button,

    MuiInputBase: InputBase,
    MuiInputLabel: InputLabel,
    MuiFilledInput: FilledInput,
    MuiOutlinedInput: OutlinedInput,
    MuiInputAdornment: InputAdornment,

    MuiDivider: Divider,
    MuiStack: Stack,

    MuiLink: Link,
    MuiListItemButton: ListItemButton,
    MuiListItemIcon: ListItemIcon,
    MuiListItemText: ListItemText,

    MuiMenu: Menu,
    MuiMenuItem: MenuItem,

    MuiPagination: Pagination,
    MuiDataGrid: DataGrid,
    MuiCssBaseline: CssBaseline,
  },
});

// Add custom shadows to theme
shadows.forEach((shadow, index) => {
  theme.shadows[index + 1] = shadow;
});
